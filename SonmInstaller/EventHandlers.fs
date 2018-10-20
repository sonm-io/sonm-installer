module SonmInstaller.EventHandlers

open Elmish
open System.Windows.Forms
open SonmInstaller.Components
open SonmInstaller.Components.Main
open SonmInstaller.Components.Main.Msg

let subscribeToEvents (this: WizardForm) (d: Dispatch<Msg>) = 
    this.Load.Add <| fun _ ->
        Tools.ensureAppPathExists ()

    this.btnBack.Click.Add <| fun _ -> d BackBtn

    this.btnNext.Click.Add <| fun _ -> d NextBtn

    //#region step1choose Do you have etherium wallet?
    this.radioNoWallet.CheckedChanged.Add <| fun _ ->
        d <| HasWallet this.radioHasWallet.Checked
    //#endregion

    //#region step2a1 Generating keystore

    this.tbPassword.TextChanged.Add <| fun _ -> 
        this.tbPassword.Text
        |> NewKeyPage.PasswordUpdate
        |> NewKeyMsg
        |> d

    this.tbPasswordRepeat.TextChanged.Add <| fun _ ->
        this.tbPasswordRepeat.Text
        |> NewKeyPage.PasswordRepeatUpdate
        |> NewKeyMsg
        |> d

    this.linkNewKeyPath.LinkClicked.Add <| fun _ ->
        if this.saveNewKey.ShowDialog() = DialogResult.OK then
            this.saveNewKey.FileName
            |> NewKeyPage.ChangeKeyPath 
            |> NewKeyMsg
            |> d
            
    //#endregion

    //#region step2a2 Save your key
        
    this.linkOpenKeyDir.LinkClicked.Add <| fun _ -> d OpenKeyDir

    this.linkOpenKeyFile.LinkClicked.Add <| fun _ -> d OpenKeyFile

    //#endregion

    //#region step2b Select json-file with key, Enter keystore password
    let openKeystoreHandler _ =
         if this.openKeystore.ShowDialog() = DialogResult.OK then
            this.openKeystore.FileName
            |> ImportKey.ChoosePath
            |> Msg.ImportKey
            |> d
    
    this.linkSelectKeyFile.LinkClicked.Add <| openKeystoreHandler

    this.btnSelectKeyFile.Click.Add <| openKeystoreHandler

    this.tbSelectedKeyPassword.TextChanged.Add <| fun _ ->
        this.tbSelectedKeyPassword.Text
        |> ImportKey.ChangePassword
        |> Msg.ImportKey
        |> d
    //#endregion

    //#region step3 Where to send money?
    //
    //#endregion

    //#region step4 Select disk to write to
    this.cmbDisk.SelectedValueChanged.Add <| fun _ -> ()
    //#endregion

    //#region step5progress Preparing installation image
    this.btnTryAgainProgress.Click.Add <| fun _ -> Download.Start |> Download |> d
    //#endregion

    //#region step6 Finish
    this.linkFaq.LinkClicked.Add <| fun _ -> ()
    //#endregion