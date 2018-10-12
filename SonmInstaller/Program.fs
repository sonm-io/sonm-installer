module SonmInstaller.Program

open Elmish
open Elmish.Winforms
open SonmInstaller.EventHandlers
open SonmInstaller.Components.Main
open SonmInstaller.EventHandlers
open System.Linq.Expressions

let private view form prev next _ = 
    viewInvoker 
        View.view
        form
        prev
        next

let private subscription form _ = [fun dispatch -> subscribeToEvents form dispatch]

let getProgram (form: WizardForm) = 
    Program.mkProgram
        Init.init
        Update.update
        (view form)
    |> Program.withSubscription (subscription form)
    |> Program.withErrorHandler (fun (_, e) -> raise e)