[<AutoOpen>]
module SonmInstaller.Components.MainView

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
        
        module DownloadProgressBar = 
            let update (form: WizardForm) tpl = 
                form.progressBar.LabelTpl       <- tpl
                form.progressBarBottom.LabelTpl <- tpl
        
            let reset (form: WizardForm) = 
                update form "Download in progress: {0:0.#} of {1:0.#} ({2:0}%)"
        

    open Shared

    module private Initial =
        
        let addDrives (form: WizardForm) = 
            Tools.getDrives ()
            |> List.iter (fun i -> form.cmbDisk.Items.Add i |> ignore)

        let view (form: WizardForm) (state: Main.State) = 
            DownloadProgressBar.reset form
            addDrives form
        

    module private Common =
        let totalSteps = 6

        let downloadComplete (form: WizardForm) = function
            | Ok () -> 
                form.pnlErrorDownload.Visible <- false
                let text = "Download Complete"
                form.progressBar.LabelTpl       <- text
                form.progressBarBottom.LabelTpl <- text
            | Error (e: exn) -> 
                form.pnlErrorDownload.Visible <- true
                let text = "Download Error"
                form.progressBar.LabelTpl       <- text
                form.progressBarBottom.LabelTpl <- text
                form.lblDownloadError.Text <- Exn.getMessagesStack e
                form.lblDownloadError.Visible <- true

        let updateStepNum (form: WizardForm) current total =
            let label = form.HeaderLabels.[form.tabs.SelectedIndex]
            label.Text <- String.Format(form.HeaderTpls.[form.tabs.SelectedIndex], current, total)

        let view (form: WizardForm) (prev: Main.State option) (next: Main.State) = 
            let doIf = doIfChanged prev next
            let inline doPropIf p a = doPropIfChanged prev next p a
    
            doIf (fun s -> s.backButton) (Button.view form.btnBack)
            doIf (fun s -> s.nextButton) (Button.view form.btnNext)
    
            doPropIf (fun s -> s.newKeyState.keyPath) (fun keyPath -> 
                form.lblNewKeyPath.Text          <- keyPath
                form.saveNewKey.InitialDirectory <- Path.GetDirectoryName keyPath
                form.saveNewKey.FileName         <- Path.GetFileName      keyPath
            )

            doPropIf (fun s -> s.installationProgress) (function 
                | InstallationProgress.DownloadComplete result -> downloadComplete form result
                | Downloading -> 
                    DownloadProgressBar.reset form
                    form.pnlErrorDownload.Visible <- false
                | _ -> ()
            )

            doPropIf (fun s -> s.etherAddress) (function 
                | Some addr -> addr
                | None      -> String.Empty
            >> fun addr  -> form.tbAddressToSend.Text <- addr)

            doPropIf (fun s -> s.isPending) (fun isPending -> form.loader.Visible <- isPending) 

            form.tabs.SelectedIndex <- LanguagePrimitives.EnumToValue (next.CurrentScreen ())
            
            updateStepNum form (next.CurrentStepNum()) totalSteps
            
            NewKeyPage.view form next.newKeyState
            
            form.progressBarBottom.Visible <- 
                let cs = next.CurrentScreen ()
                cs > Screen.S0Welcome 
                && cs < Screen.S5Progress 
                //&& [ InstallationProgress.Downloading ] |> List.contains next.InstallationProgress

    module private Messaged = 
        
        let downloadProgress (form: WizardForm) downloaded total = 
            let d = (double downloaded) / 1024. / 1024.
            let t = (double total) / 1024. / 1024.
            form.progressBarBottom.ProgressTotal <- t
            form.progressBar.ProgressTotal <- t
            form.progressBarBottom.ProgressCurrent <- d
            form.progressBar.ProgressCurrent <- d

    let private messagedView (form: WizardForm) (prev: Main.State option) (next: Main.State) msg = 
        match msg with
        | Msg.Download (Download.Progress (downloaded, total)) -> 
            Messaged.downloadProgress form downloaded total
        | _ -> 
            Common.view form prev next 

    let view (form: WizardForm) (prev: Main.State option) (next: Main.State) msg = 
        if Option.isNone prev then
            Initial.view form next
        match msg with
            | Some msg -> messagedView form prev next msg
            | _        -> Common.view  form prev next
