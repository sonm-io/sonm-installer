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

    let withCloseApp (form: WizardForm) srv = 
        let action = fun () -> form.Close()
        { srv with closeApp = fun () -> crossThreadControlInvoke form action }

open Impl

let getProgram (form: WizardForm) = 
    let srv = Mock |> getService |> withCloseApp form
    Program.mkProgram
        (Main.init srv)
        (Main.update srv)
        (fun prev next msg -> 
            let action = fun () -> Main.view form prev next msg
            crossThreadControlInvoke form action)
    |> Program.withSubscription (subscription form)
    |> Program.withErrorHandler (fun (_, e) -> raise e)
