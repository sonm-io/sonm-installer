module Elmish.Winforms

open System.Windows.Forms

    let crossThreadControlInvoke (form: #Control) (action: unit -> unit) = 
        let a = System.Action<unit>(action)
        if not form.IsDisposed then
            if form.InvokeRequired then
                form.Invoke(a, ()) |> ignore
            else
                a.Invoke()        

