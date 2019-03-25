[<AutoOpen>]
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
        
        let addDrives (form: WizardForm) (drivesList: UsbDrive list) = 
            drivesList
            |> List.map (fun d -> new ListItem(d.index, d.caption))
            |> Array.ofList
            |> (fun dataSource -> form.cmbDisk.DataSource <- dataSource)

        let updateWelcomeScreen (form: WizardForm) isOk = 
            form.linkWelcome.Visible     <- isOk
            form.lblWelcomeError.Visible <- not isOk

    open Shared

    module private Initial =

        let view (form: WizardForm) (state: Main.State) = 
            addDrives form state.usbDrives.list
            form.tbThresholdAmount.Text <- state.withdraw.thresholdPayout
        

    module private Common =
        open Main
        open System.Configuration

        let totalSteps = 5

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
                | Some d -> form.cmbDisk.SelectedValue <- d.index
                | None -> form.cmbDisk.SelectedIndex <- -1
            )

            doPropIf (fun s -> s.usbDrives.erasePreviousData) (fun erase ->
                form.checkUpdateDist.Checked <- erase
            )

            doPropIf (fun s -> s.usbDrives.selectedDrive) (fun selected ->
                let enabled, chkd = match selected with
                                    | Some d -> d.containsSonm, next.usbDrives.erasePreviousData
                                    | None -> false, false
                form.checkUpdateDist.Checked <- chkd
                form.checkUpdateDist.Enabled <- enabled
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

            if next.CurrentScreen () = Screen.S0Welcome then
                updateWelcomeScreen form next.isProcessElevated

    let private messagedView (form: WizardForm) (prev: Main.State option) (next: Main.State) d msg = 
        let defaultRender () = Common.view form prev next d
        match msg with
        | Msg.MakeUsbStick p -> 
            match p with
            | Progress.Msg.Start _ -> 
                Progress.reset form
                defaultRender()
            | Progress.Msg.Progress state -> 
                //Progress.view form (Option.bind (fun s -> s.progress) prev) state
                {next with
                    progress = Some state
                } |> ignore
                defaultRender()
            | _ -> defaultRender()
        | _ -> defaultRender()
            

    let view (form: WizardForm) (prev: Main.State option) (next: Main.State) dispatch msg = 
        if Option.isNone prev then
            Initial.view form next
        match msg with
            | Some msg -> messagedView form prev next dispatch msg
            | _        -> Common.view  form prev next dispatch
