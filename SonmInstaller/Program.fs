﻿module SonmInstaller.Program

open Elmish
open Elmish.Winforms
open SonmInstaller.EventHandlers
open SonmInstaller.Components
open SonmInstaller.EventHandlers
open System.Linq.Expressions

let private subscription form _ = [fun dispatch -> subscribeToEvents form dispatch]

let getProgram (form: WizardForm) = 
    Program.mkProgram
        main.init
        Main.update
        (viewInvoker Main.view form)
    |> Program.withSubscription (subscription form)
    |> Program.withErrorHandler (fun (_, e) -> raise e)