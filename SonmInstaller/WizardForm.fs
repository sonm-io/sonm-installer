[<AutoOpen>]
module SonmInstaller.Form

open System
open System.Linq;
open System.Windows.Forms
open SonmInstaller.ViewModel

let totalSteps = 6

let mutable headerLabels: Label list = []
let mutable headerTpls: string list = []

let updateButton (button: Button) (prev: ButtonState) (next: ButtonState) force =
    if force || prev.Visible <> next.Visible then
        button.Visible <- next.Visible
    if force || prev.Text <> next.Text then
        button.Text <- next.Text
    if force || prev.Enabled <> next.Enabled then
        button.Enabled <- next.Enabled

let updateProgress (form: WizardFormDesign) bytes = 
    form.progressBarBottom.ProgressCurrent <- bytes
    form.progressBar.ProgressCurrent <- bytes

let updateStepNum (form: WizardFormDesign) current total =
    let label = headerLabels.[form.tabs.SelectedIndex]
    label.Text <- String.Format(headerTpls.[form.tabs.SelectedIndex], current, total)

let updateNewKey (form: WizardFormDesign) (next: UiState) =
    form.lblNewKeyErrorMessage.Text <- 
        match next.NewKeyState.ErrorMessage with
        | Some err -> err
        | None -> ""
    form.lblNewKeyErrorMessage.Visible <- Option.isSome next.NewKeyState.ErrorMessage

let updateView (form: WizardFormDesign) (prev: UiState) (next: UiState) force =
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

type WizardForm() as this =
    inherit WizardFormDesign()
    
    let mutable state: UiState = ViewModel.Main.initial
    
    let getHeaders () = 
        let getHeaderFromTab (tab: TabPage) = 
            tab.Controls.Cast<Control>()
            |> Seq.tryFind (fun ctl -> ctl.Tag <> null && ctl.Tag.ToString() = "header")
        
        this.tabs.TabPages.Cast<TabPage>()
        |> Seq.map (fun tab -> tab |> getHeaderFromTab)
        |> Seq.filter (function Some _ -> true | _ -> false)
        |> Seq.map (function Some v -> v | _ -> failwith "imposible")
        |> Seq.map (fun i -> i :?> Label)
        |> List.ofSeq
 
    let dispatch action =
        let next = update state action
        let current = state
        state <- next
        updateView this current next false
        

    let setEventHandlers () = 
        this.btnBack.Click.Add <| fun _ -> dispatch Back

        this.btnNext.Click.Add <| fun _ -> dispatch Next

        //#region step1choose Do you have etherium wallet?
        this.radioNoWallet.CheckedChanged.Add <| fun _ ->
            dispatch <| HasWallet this.radioHasWallet.Checked
        //#endregion

        //#region step2a1 Generating keystore

        this.tbPassword.TextChanged.Add <| fun _ -> 
            dispatch <| NewKeyAction (NewKeyPage.PasswordUpdate this.tbPassword.Text)

        this.tbPasswordRepeat.TextChanged.Add <| fun _ ->
            dispatch <| NewKeyAction (NewKeyPage.PasswordRepeatUpdate this.tbPasswordRepeat.Text)

        this.linkChooseDir.LinkClicked.Add <| fun _ -> ()
        //#endregion

        //#region step2a2 Save your key
        this.linkOpenKeyDir.LinkClicked.Add <| fun _ -> ()

        this.linkOpenKeyFile.LinkClicked.Add <| fun _ -> ()
        //#endregion

        //#region step2b Select json-file with key
        this.linkSelectKeyFile.LinkClicked.Add <| fun _ -> ()

        this.btnSelectKeyFile.Click.Add <| fun _ -> ()
        //#endregion

        //#region step3 Where to send money?
        //#endregion

        //#region step4 Select disk to write to
        this.cmbDisk.SelectedValueChanged.Add <| fun _ -> ()
        //#endregion

        //#region step6 Finish
        this.linkFaq.LinkClicked.Add <| fun _ -> ()
        //#endregion

    do
        headerLabels <- getHeaders ()
        headerTpls <- headerLabels |> Seq.map (fun i -> i.Text) |> List.ofSeq
        setEventHandlers()
        updateView this state state true

    






    

    