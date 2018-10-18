[<AutoOpen>]
module SonmInstaller.Components.NewKeyPageUpdate

open Elmish
open SonmInstaller.Components
open SonmInstaller.Components.NewKeyPage

module NewKeyPage = 

    type IService = 
        abstract member GenerateKeyStore: password: string -> Async<unit>
        abstract member DefaultNewKeyPath: string

    let init (srv: IService) = 
        { 
            Password = ""
            PasswordRepeat = ""
            ErrorMessage = None 
            KeyPath = srv.DefaultNewKeyPath
            IsPending = false
            KeyJustCreated = false
        }

    module private Private =

        let validateState (state: NewKeyPage.State) = 
            let validate p1 p2 =
                if p1 = "" then
                    Some "Please choose a password"
                elif p2 = "" then
                    Some "Please confirm your password"
                elif p1 <> p2 then
                    Some "Passwords didn't match"
                else
                    None            
            validate state.Password state.PasswordRepeat

    let update (service: IService) (state: NewKeyPage.State) = function
        | PasswordUpdate p ->
            { state with Password = p; ErrorMessage = None }, Cmd.none
        | PasswordRepeatUpdate p -> 
            { state with PasswordRepeat = p; ErrorMessage = None }, Cmd.none
        | ChangeKeyPath path -> { state with KeyPath = path }, Cmd.none
        | TryCreateKey -> 
            let errorMsg = Private.validateState state
            match errorMsg with
            | Some _ -> { state with ErrorMessage = errorMsg }, Cmd.none
            | None -> 
                let cmd = Cmd.ofAsync
                            service.GenerateKeyStore 
                            state.Password
                            (fun () -> FinishCreateKey None)
                            (fun e -> e |> Some |> FinishCreateKey)
                { state with ErrorMessage = None; IsPending = true }, cmd
        | FinishCreateKey _ -> { state with IsPending = false; KeyJustCreated = true }, Cmd.none
        | ResetResult -> { state with KeyJustCreated = false }, Cmd.none
