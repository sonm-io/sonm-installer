namespace SonmInstaller.Components.NewKeyPage

open SonmInstaller.Tools

module Update = 
    module private Private =

        let validateState (state: State) = 
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

    let update (state: State) = function
        | PasswordUpdate p ->
            { state with Password = p; ErrorMessage = None }
        | PasswordRepeatUpdate p -> 
            { state with PasswordRepeat = p; ErrorMessage = None }
        | ChangeKeyPath path -> { state with KeyPath = path }
        | TryCreateKey -> 
            let errorMsg = Private.validateState state
            match errorMsg with
            | Some _ -> { state with ErrorMessage = errorMsg; KeyContent = None }
            | None -> 
                let key = generateNewKey state.Password
                saveTextFile state.KeyPath key
                { state with ErrorMessage = None; KeyContent = Some key }