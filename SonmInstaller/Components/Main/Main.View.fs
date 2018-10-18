[<AutoOpen>]
module SonmInstaller.Components.MainView

open SonmInstaller
open SonmInstaller.Components
open SonmInstaller.Components.Main
open Elmish.Tools

open System
open System.IO

module Main = 

    module private Shared = 
        
        module DownloadProgressBar = 
            let update (form: WizardForm) tpl = 
                form.progressBar.LabelTpl       <- tpl
                form.progressBarBottom.LabelTpl <- tpl
        
            let reset (form: WizardForm) = 
                update form "Download in progress: {0:0.#} of {1:0.#} ({2:0}%)"
        

        let switchLoader (form: WizardForm) (show: bool) = 
            form.loader.Visible <- show

    open Shared

    let private initialView (form: WizardForm) (state: Main.State) = 
        DownloadProgressBar.reset form

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
                let msg = 
                    sprintf "%s. %s"
                        e.Message
                        (if e.InnerException <> null then e.InnerException.Message else "")
                form.lblDownloadError.Text <- msg
                form.lblDownloadError.Visible <- true

        let updateStepNum (form: WizardForm) current total =
            let label = form.HeaderLabels.[form.tabs.SelectedIndex]
            label.Text <- String.Format(form.HeaderTpls.[form.tabs.SelectedIndex], current, total)

        let view (form: WizardForm) (prev: Main.State option) (next: Main.State) = 
            let doIf = doIfChanged prev next
            let inline doPropIf p a = doPropIfChanged prev next p a
    
            doIf (fun s -> s.BackButton) (Button.view form.btnBack)
            doIf (fun s -> s.NextButton) (Button.view form.btnNext)
    
            doPropIf (fun s -> s.NewKeyState.KeyPath) (fun keyPath -> 
                form.lblNewKeyPath.Text          <- keyPath
                form.saveNewKey.InitialDirectory <- Path.GetDirectoryName keyPath
                form.saveNewKey.FileName         <- Path.GetFileName      keyPath
            )

            doPropIf (fun s -> s.InstallationProgress) (function 
                | InstallationProgress.DownloadComplete result -> downloadComplete form result
                | Downloading -> 
                    DownloadProgressBar.reset form
                    form.pnlErrorDownload.Visible <- false
                | _ -> ()
            )

            doPropIf (fun s -> s.IsPending()) (fun isPending -> form.loader.Visible <- isPending) 

            form.tabs.SelectedIndex <- LanguagePrimitives.EnumToValue (next.CurrentScreen ())
            
            updateStepNum form (next.CurrentStepNum()) totalSteps
            
            NewKeyPage.view form next.NewKeyState
            
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
        | DownloadProgress (downloaded, total) -> Messaged.downloadProgress form downloaded total
        | _ -> Common.view form prev next 

    let view (form: WizardForm) (prev: Main.State option) (next: Main.State) msg = 
        if Option.isNone prev then
            initialView form next
        match msg with
            | Some msg -> messagedView form prev next msg
            | _        -> Common.view  form prev next
