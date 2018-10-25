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

    let customizeMock srv = 
        { srv with
            startDownload = Mock.download 4000 (600L * 1024L * 1024L)
            getUsbDrives = realService.getUsbDrives
        }

    let customizeReal srv = 
        { srv with
            startDownload = Mock.download 4000 (600L * 1024L * 1024L)
        }

    let withCloseApp (form: WizardForm) srv = 
        let action = fun () -> form.Close()
        { srv with closeApp = fun () -> crossThreadControlInvoke form action }

    let rec getService = function
    | Mock   -> Mock.createEmptyService 1000 |> customizeMock
    | Real   -> realService |> customizeReal

open Impl

let getProgram (form: WizardForm) = 
    let srv = 
        Mock 
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

let x = Ok 1
let y = Result.Error "wef"