module SonmInstaller.Program

open Elmish
open Elmish.Winforms
open SonmInstaller.EventHandlers
open SonmInstaller.Components
open SonmInstaller.Domain

module private Impl =  

    let subscription form _ = [fun dispatch -> subscribeToEvents form dispatch]

    type ServiceType = Mock | Real

    let realService = (new DomainService()).GetService()
    let mockService = (Mock.createEmptyService 0)

    let customizeMock srv = 
        { srv with
            startDownload = Mock.download 15000 (600L * 1024L * 1024L)
            getUsbDrives = realService.getUsbDrives
            makeUsbStick = Mock.makeUsbStick 10000 32
        }

    let customizeReal srv = 
        { srv with
            startDownload = Mock.download 5000 (600L * 1024L * 1024L)
            callSmartContract = (fun _ _ -> Mock.waitReturn 100 "callSmartContract" ())
            //makeUsbStick = Mock.makeUsbStick 10000 32
        }

    let withCloseApp (form: WizardForm) srv = 
        let action = fun () -> form.Close()
        { srv with closeApp = fun () -> crossThreadControlInvoke form action }

    let rec getService = function
    | Mock   -> Mock.createEmptyService 0 |> customizeMock
    | Real   -> realService |> customizeReal

open Impl

let getProgram (form: WizardForm) = 
    let srv = 
        Real 
        |> getService 
        |> withCloseApp form
    Program.mkProgram
        (Main.init srv)
        (Main.update srv)
        (fun prev next dispatch msg -> 
            let action = fun () -> Main.view form prev next dispatch msg
            crossThreadControlInvoke form action)
    |> Program.withSubscription (subscription form)
    |> Program.withErrorHandler (fun (_, e) -> raise e)

