module SonmInstaller.Program

open Elmish
open Elmish.Winforms
open SonmInstaller.EventHandlers
open SonmInstaller.Components
open SonmInstaller.EventHandlers
open System.Linq.Expressions
open SonmInstaller.Domain

module private Impl =  

    let subscription form _ = [fun dispatch -> subscribeToEvents form dispatch]

open Impl

let getProgram (form: WizardForm) = 
    Program.mkProgram
        main.init
        (Main.update Mock.service)
        (viewInvoker Main.view form)
    |> Program.withSubscription (subscription form)
    |> Program.withErrorHandler (fun (_, e) -> raise e)