﻿[<AutoOpen>]
module SonmInstaller.Components.MainView

open System.Collections.Generic
open SonmInstaller
open SonmInstaller.Tools
open SonmInstaller.Components
open SonmInstaller.Components.Main
open SonmInstaller.Components.Main.Msg
open Elmish.Tools

open System
open System.IO
open System.Windows.Forms

module Main = 

    module private Shared = 
        
        let addDrives (form: WizardForm) (drivesList: (int * string) list) = 
            drivesList
            |> List.map (fun (i, text) -> new ListItem(i, text))
            |> Array.ofList
            |> (fun dataSource -> form.cmbDisk.DataSource <- dataSource)

    open Shared

    module private Initial =

        let view (form: WizardForm) (state: Main.State) = 
            addDrives form state.usbDrives.list
            form.tbThresholdAmount.Text <- state.withdraw.thresholdPayout
        

    module private Common =
        open Main

        let totalSteps = 6

        let updateStepNum (form: WizardForm) current total =
            let label = form.HeaderLabels.[form.tabs.SelectedIndex]
            label.Text <- String.Format(form.HeaderTpls.[form.tabs.SelectedIndex], current, total)

        let show (form: WizardForm) (s: Main.State) d = 
            match s.show with
            | ShowStep -> 
                form.tabs.SelectedIndex <- LanguagePrimitives.EnumToValue (s.CurrentScreen ())
                updateStepNum form (s.CurrentStepNum()) totalSteps
            | ShowMessagePage page -> 
                form.tabs.SelectedIndex <- 10
                form.lblMessagePageHeader.Text <- page.header
                form.lblMessagePageText.Text <- page.message
                form.btnMessagePageTryAgain.Visible <- page.tryAgainVisible
            | ShowMessageBox box -> 
                let result = MessageBox.Show (box.text, box.caption, MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2)
                match result with
                | DialogResult.Yes -> DlgRes.Ok
                | DialogResult.No  -> DlgRes.Cancel
                | _ -> failwith "Unhandled result type"
                |> Msg.DialogResult
                |> d // ToDo: wait until download complete and then press any key. Expected result: dialog closed, Actual: dialog appears again.

        let view (form: WizardForm) (prev: Main.State option) (next: Main.State) d = 
            let doIf = doIfChanged prev next
            let inline doPropIf p a = doPropIfChanged prev next p a
    
            doIf (fun s -> s.backButton) (Button.view form.btnBack)
            doIf (fun s -> s.nextButton) (Button.view form.btnNext)
    
            doPropIf (fun s -> s.newKeyState.keyPath) (fun keyPath -> 
                form.lblNewKeyPath.Text          <- keyPath
                form.saveNewKey.InitialDirectory <- Path.GetDirectoryName keyPath
                form.saveNewKey.FileName         <- Path.GetFileName      keyPath
            )

            //doPropIf (fun s -> s.installationProgress) (function 
            //    | InstallationProgress.Downloading -> 
            //        form.progressBarBottom.Visible <- true
            //    | _ -> ()
            //)

            doPropIf (fun s -> s.etherAddress) (function 
                | Some addr -> addr
                | None      -> String.Empty
            >> fun addr  -> form.tbAddressToSend.Text <- addr)

            doPropIf (fun s -> s.isPending) (fun isPending -> form.loader.Visible <- isPending) 

            doPropIf (fun s -> s.existingKeystore.path) (
                Option.defaultValue "" 
                >> fun path -> form.lblLoadedKeyPath.Text <- path
            )

            doPropIf (fun s -> s.usbDrives.list) (fun usbDrives -> 
                addDrives form usbDrives
                match next.usbDrives.selectedDrive with
                | Some (i, _) -> form.cmbDisk.SelectedValue <- i
                | None -> form.cmbDisk.SelectedIndex <- -1
            )

            // ToDo: think about function like doPropIf
            if next.progress.IsSome then
                Progress.view 
                    form
                    (if prev.IsNone then None else prev.Value.progress)
                    next.progress.Value

            show form next d

            NewKeyPage.view form next.newKeyState
            
            form.progressBarBottom.Visible <- 
                let cs = next.CurrentScreen ()
                cs > Screen.S0Welcome 
                && cs < Screen.S5Progress 
                //&& [ InstallationProgress.Downloading ] |> List.contains next.InstallationProgress

    let private messagedView (form: WizardForm) (prev: Main.State option) (next: Main.State) d msg = 
        let defaultRender () = Common.view form prev next d
        match msg with
        | Msg.Download p
        | Msg.MakeUsbStick p -> 
            match p with
            | Progress.Msg.Start -> 
                Progress.reset form
                defaultRender()
            | Progress.Msg.Progress (current, total) -> Progress.progress form (current, total)
            | _ -> defaultRender()
        | _ -> defaultRender()
            

    let view (form: WizardForm) (prev: Main.State option) (next: Main.State) dispatch msg = 
        if Option.isNone prev then
            Initial.view form next
        match msg with
            | Some msg -> messagedView form prev next dispatch msg
            | _        -> Common.view  form prev next dispatch
