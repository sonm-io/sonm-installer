module SonmInstaller.Components.Main.View

open SonmInstaller
open SonmInstaller.Components
open Elmish.Tools

open System
open System.IO

module private Impl =
    let updateStepNum (form: WizardForm) current total =
        let label = form.HeaderLabels.[form.tabs.SelectedIndex]
        label.Text <- String.Format(form.HeaderTpls.[form.tabs.SelectedIndex], current, total)

    let updateProgress (form: WizardForm) bytes = 
        form.progressBarBottom.ProgressCurrent <- bytes
        form.progressBar.ProgressCurrent <- bytes

    let totalSteps = 6 // ToDo: move it from here

open Impl

let view (form: WizardForm) (prev: UiState option) (next: UiState) _ =
    let doIf = doIfChanged prev next
    let inline doPropIf p a = doPropIfChanged prev next p a
    
    doIf (fun s -> s.BackButton) (Button.View.view form.btnBack) //ToDo: names
    doIf (fun s -> s.NextButton) (Button.View.view form.btnNext)
    
    doPropIf (fun s -> s.NewKeyState.KeyPath) (fun keyPath -> 
        form.lblNewKeyPath.Text          <- keyPath
        form.saveNewKey.InitialDirectory <- Path.GetDirectoryName keyPath   // ToDo: remove System.IO dependency
        form.saveNewKey.FileName         <- Path.GetFileName      keyPath   // 
    )

    form.tabs.SelectedIndex <- LanguagePrimitives.EnumToValue (next.CurrentScreen ())
    updateStepNum form (next.CurrentStepNum()) totalSteps
    NewKeyPage.View.view form next.NewKeyState // ToDo: namings
    form.progressBarBottom.Visible <- 
        let cs = next.CurrentScreen ()
        cs > Screen.S0Welcome && cs < Screen.S5Progress