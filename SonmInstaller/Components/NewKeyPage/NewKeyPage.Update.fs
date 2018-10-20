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
            password = ""
            passwordRepeat = ""
            errorMessage = None 
            keyPath = srv.DefaultNewKeyPath
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
            validate state.password state.passwordRepeat

    let update (state: NewKeyPage.State) = function
        | PasswordUpdate p ->
            { state with password = p; errorMessage = None }
        | PasswordRepeatUpdate p -> 
            { state with passwordRepeat = p; errorMessage = None }
        | ChangeKeyPath path -> { state with keyPath = path }
        | Validate -> 
            let errorMsg = Private.validateState state
            match errorMsg with
            | Some _ -> { state with errorMessage = errorMsg }
            | None -> state
