[<AutoOpen>]
module SonmInstaller.Components.ButtonView

open Elmish.Tools
open System.Windows.Forms
open SonmInstaller.Components.Button
 
module Button =

    let view (button: Button) (prev: State option) (next: State) =
        let inline doPropIf p a = doPropIfChanged prev next p a
        doPropIf (fun s -> s.Visible) (fun v -> button.Visible <- v)
        doPropIf (fun s -> s.Text) (fun v -> button.Text <- v)
        doPropIf (fun s -> s.Enabled) (fun v -> button.Enabled <- v)
