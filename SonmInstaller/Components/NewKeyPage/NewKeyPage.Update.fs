[<AutoOpen>]
module SonmInstaller.Components.NewKeyPageUpdate

open Elmish
open SonmInstaller.Components
open SonmInstaller.Components.NewKeyPage

module NewKeyPage = 

    type IService = 
        abstract member DefaultNewKeyPath: string

    let init (srv: IService) = 
        { 
            Password = ""
            PasswordRepeat = ""
            ErrorMessage = None 
            KeyPath = srv.DefaultNewKeyPath
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

    let update (state: NewKeyPage.State) = function
        | PasswordUpdate p ->
            { state with Password = p; ErrorMessage = None }
        | PasswordRepeatUpdate p -> 
            { state with PasswordRepeat = p; ErrorMessage = None }
        | ChangeKeyPath path -> { state with KeyPath = path }
        | Validate -> 
            let errorMsg = Private.validateState state
            match errorMsg with
            | Some _ -> { state with ErrorMessage = errorMsg }
            | None -> state
