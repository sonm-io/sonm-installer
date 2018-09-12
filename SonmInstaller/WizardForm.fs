namespace SonmInstaller

open System
open System.Windows.Forms
open SonmInstaller.ViewModel

//let updateView (form: WizardForm) (state: UiState) (changes: UiStateChanges) =
//    ()

type WizardForm() as this =
    inherit WizardFormDesign()
    do
        this.btnBack.Click.Add <| fun _ -> ()

        this.btnNext.Click.Add <| fun _ -> ()

        //#region step2a1 Key Generation
        this.tbPassword.Leave.Add <| fun _ -> ()

        this.tbPassword.TextChanged.Add <| fun _ -> ()

        this.tbPasswordRepeat.Leave.Add <| fun _ -> ()

        this.tbPasswordRepeat.TextChanged.Add <| fun _ -> ()

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


    member this.updateView (state: UiState) (changes: UiStateChanges) =
        changes |> List.iter (fun change -> ())



    