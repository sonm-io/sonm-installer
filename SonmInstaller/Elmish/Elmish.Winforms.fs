module Elmish.Winforms

open System.Windows.Forms

    let viewInvoker view (form: #Form) (prev: 's option) (next: 's) = 
        let action = System.Action<unit>(fun _ -> view form prev next)
        if form.InvokeRequired then
            form.Invoke(action, ()) |> ignore
        else
            view form prev next