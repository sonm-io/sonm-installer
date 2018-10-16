module SonmInstaller.EventHandlers

open Elmish
open System.Windows.Forms
open SonmInstaller.Components
open SonmInstaller.Components.Main


let subscribeToEvents (this: WizardForm) (dispatch: Dispatch<Main.Msg>) = 
    this.Load.Add <| fun _ ->
        Tools.ensureSonmPathExists ()

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

    this.linkNewKeyPath.LinkClicked.Add <| fun _ ->
        if this.saveNewKey.ShowDialog() = DialogResult.OK then
            dispatch <| NewKeyAction (NewKeyPage.ChangeKeyPath this.saveNewKey.FileName)
            
    //#endregion

    //#region step2a2 Save your key
        
    this.linkOpenKeyDir.LinkClicked.Add <| fun _ -> dispatch OpenKeyDir

    this.linkOpenKeyFile.LinkClicked.Add <| fun _ -> dispatch OpenKeyFile

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