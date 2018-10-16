[<AutoOpen>]
module SonmInstaller.Components.MainView

open SonmInstaller
open SonmInstaller.Components
open SonmInstaller.Components.Main
open Elmish.Tools

open System
open System.IO

module Main = 

    module private Impl =
        let totalSteps = 6
        
        let updateStepNum (form: WizardForm) current total =
            let label = form.HeaderLabels.[form.tabs.SelectedIndex]
            label.Text <- String.Format(form.HeaderTpls.[form.tabs.SelectedIndex], current, total)

        let downloadProgress (form: WizardForm) downloaded total = 
            form.progressBarBottom.ProgressCurrent <- downloaded
            form.progressBar.ProgressCurrent <- downloaded
            form.progressBarBottom.ProgressTotal <- total
            form.progressBar.ProgressTotal <- total

        let commonView (form: WizardForm) (prev: Main.State option) (next: Main.State) = 
            let doIf = doIfChanged prev next
            let inline doPropIf p a = doPropIfChanged prev next p a
    
            doIf (fun s -> s.BackButton) (Button.view form.btnBack)
            doIf (fun s -> s.NextButton) (Button.view form.btnNext)
    
            doPropIf (fun s -> s.NewKeyState.KeyPath) (fun keyPath -> 
                form.lblNewKeyPath.Text          <- keyPath
                form.saveNewKey.InitialDirectory <- Path.GetDirectoryName keyPath
                form.saveNewKey.FileName         <- Path.GetFileName      keyPath
            )

            form.tabs.SelectedIndex <- LanguagePrimitives.EnumToValue (next.CurrentScreen ())
            updateStepNum form (next.CurrentStepNum()) totalSteps
            NewKeyPage.view form next.NewKeyState
            form.progressBarBottom.Visible <- 
                let cs = next.CurrentScreen ()
                cs > Screen.S0Welcome 
                && cs < Screen.S5Progress 
                && [ InstallationStatus.Downloading ] |> List.contains next.InstallationStatus

        let messageView (form: WizardForm) (prev: Main.State option) (next: Main.State) msg = 
            match msg with
            | DownloadProgress (downloaded, total) -> downloadProgress form downloaded total
            | _ -> commonView form prev next           

    open Impl

    let view (form: WizardForm) (prev: Main.State option) (next: Main.State) = function
        | Some msg -> messageView form prev next msg
        | _        -> commonView  form prev next
        
        
