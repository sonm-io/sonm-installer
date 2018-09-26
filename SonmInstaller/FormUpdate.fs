module SonmInstaller.FormUpdate

open System
open System.Linq;
open System.Windows.Forms
open SonmInstaller.ViewModel

let totalSteps = 6

let updateButton (button: Button) (prev: ButtonState) (next: ButtonState) force =
    if force || prev.Visible <> next.Visible then
        button.Visible <- next.Visible
    if force || prev.Text <> next.Text then
        button.Text <- next.Text
    if force || prev.Enabled <> next.Enabled then
        button.Enabled <- next.Enabled

let updateProgress (form: WizardFormBase) bytes = 
    form.progressBarBottom.ProgressCurrent <- bytes
    form.progressBar.ProgressCurrent <- bytes

let updateStepNum (form: WizardFormBase) current total =
    let label = form.HeaderLabels.[form.tabs.SelectedIndex]
    label.Text <- String.Format(form.HeaderTpls.[form.tabs.SelectedIndex], current, total)

let updateNewKey (form: WizardFormBase) (next: UiState) =
    form.lblNewKeyErrorMessage.Text <- 
        match next.NewKeyState.ErrorMessage with
        | Some err -> err
        | None -> ""
    form.lblNewKeyErrorMessage.Visible <- Option.isSome next.NewKeyState.ErrorMessage

let updateView (form: WizardFormBase) (prev: UiState) (next: UiState) force =
    if force || prev.BackButton <> next.BackButton then
        updateButton form.btnBack prev.BackButton next.BackButton force
    if force || prev.NextButton <> next.NextButton then
        updateButton form.btnNext prev.NextButton next.NextButton force
    form.tabs.SelectedIndex <- LanguagePrimitives.EnumToValue (next.CurrentScreen ())
    updateStepNum form (next.CurrentStepNum()) totalSteps
    updateNewKey form next
    form.progressBarBottom.Visible <- 
        let cs = next.CurrentScreen ()
        cs > Screen.S0Welcome && cs < Screen.S5Progress