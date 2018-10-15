module Elmish.Winforms

open System.Windows.Forms

    let viewInvoker view (form: #Form) (prev: 's option) (next: 's) (msg: 'm) = 
        let action = System.Action<unit>(fun _ -> view form prev next msg)
        if form.InvokeRequired then
            form.Invoke(action, ()) |> ignore
        else
            action.Invoke()