namespace SonmInstaller.Components.Button

open Elmish.Tools
open System.Windows.Forms
type private ButtonSt = SonmInstaller.Components.Button.ButtonState // Todo: namings
 
[<AutoOpen>]
module View =

    let view (button: Button) (prev: ButtonSt option) (next: ButtonSt) =
        let inline doPropIf p a = doPropIfChanged prev next p a
        doPropIf (fun s -> s.Visible) (fun v -> button.Visible <- v)
        doPropIf (fun s -> s.Text) (fun v -> button.Text <- v)
        doPropIf (fun s -> s.Enabled) (fun v -> button.Enabled <- v)
