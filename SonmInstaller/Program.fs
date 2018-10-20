module SonmInstaller.Program

open Elmish
open Elmish.Winforms
open SonmInstaller.EventHandlers
open SonmInstaller.Components
open SonmInstaller.Domain

module private Impl =  

    let subscription form _ = [fun dispatch -> subscribeToEvents form dispatch]

    type ServiceType = Real | Empty | Mock | Custom

    let getRealService () = (new DomainService()).GetService()

    let getService = function
        | Real   -> getRealService()
        | Empty  -> Mock.createEmptyService 4000
        | Mock   -> Mock.createService 1000 3000L
        | Custom -> 
            let srv = Mock.createService 100 100L
            {
                getRealService () with
                    startDownload = srv.startDownload
            }

open Impl

let getProgram (form: WizardForm) = 
    let srv = Mock |> getService
    Program.mkProgram
        (Main.init srv)
        (Main.update srv)
        (viewInvoker Main.view form)
    |> Program.withSubscription (subscription form)
    |> Program.withErrorHandler (fun (_, e) -> raise e)
