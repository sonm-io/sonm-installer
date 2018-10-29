namespace SonmInstaller.Components.Progress

type ProgressStyle = Continuous | Marquee

type State = {
    captionTpl: string // Template can have up to 3 placeholders: current value, total, percent
    style: ProgressStyle
}

type Msg<'res> = 
    | Start
    | Progress of float * float // current * total
    | Complete of Result<'res, exn>

namespace SonmInstaller.Components
open SonmInstaller.Components.Progress

[<AutoOpen>]
module ProgressHelpers = 
    open Elmish


    module Progress = 
        let defaultValue =
            {
                captionTpl = "Progress: {0:0.0} of {1:0.0} ({2:0}%)"
                style = ProgressStyle.Marquee
            }

        let start (msg: Msg<'res> -> 'msg) = 
            Cmd.ofSub (fun d -> Msg.Start |> msg |> d)