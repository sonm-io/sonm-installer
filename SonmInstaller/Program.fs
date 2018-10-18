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

    type ServiceType = Real | Mock | Custom

    let getRealService () = (new DomainService()).GetService()

    let getService = function
        | Real -> getRealService()
        | Mock -> Mock.service
        | Custom -> 
            {
                getRealService () with
                    startDownload = Mock.service.startDownload
            }

open Impl

let getProgram (form: WizardForm) = 
    let srv = Custom |> getService
    Program.mkProgram
        (Main.init srv)
        (Main.update srv)
        (viewInvoker Main.view form)
    |> Program.withSubscription (subscription form)
    |> Program.withErrorHandler (fun (_, e) -> raise e)
