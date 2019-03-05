[<AutoOpen>]
module SonmInstaller.Components.ProgressView

module Progress = 
    open SonmInstaller
    open SonmInstaller.Components.Progress
    open SonmInstaller.Tools
    open Elmish.Tools

    module private Impl = 
        open System.Windows.Forms

        let resetValue (pb: UI.ProgressBar) = 
            pb.LabelTpl <- ""
            pb.ProgressCurrent <- 0.
            pb.ProgressTotal <- 100.
        
        let updateLabel label (bp: UI.ProgressBar) = bp.LabelTpl <- label

        let updateStyle style (bp: UI.ProgressBar) = 
            bp.ProgressBarInner.Style <- 
                match style with
                | Continuous -> ProgressBarStyle.Continuous
                | Marquee    -> ProgressBarStyle.Marquee
        
        let updateCurrent current (bp: UI.ProgressBar) = bp.ProgressCurrent <- current

        let updateTotal total (bp: UI.ProgressBar) = bp.ProgressTotal <- total

        let getBars (form: WizardForm) = 
            [   
                form.progressBar
                form.progressBarBottom
            ]

    let reset (form: WizardForm) = Impl.getBars form |> List.iter Impl.resetValue

    let view (form: WizardForm) (prev: State option) (next: State) = 
        let inline doPropIf p a = doPropIfChanged prev next p a
        let bars = Impl.getBars form
        doPropIf (fun s -> s.captionTpl) (fun v -> bars |> List.iter (Impl.updateLabel v))
        doPropIf (fun s -> s.style) (fun v -> bars |> List.iter (Impl.updateStyle v))
        doPropIf (fun s -> s.current) (fun v -> bars |> List.iter (Impl.updateCurrent v))
        doPropIf (fun s -> s.total) (fun v -> bars |> List.iter (Impl.updateTotal v))
