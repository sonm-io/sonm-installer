namespace SonmInstaller.Components.NewKeyPage

type State = {
    password: string
    passwordRepeat: string
    errorMessage: string option
    keyPath: string
}
with
    member this.NextAllowed () = 
        (this.password <> "" || this.passwordRepeat <> "")
        && Option.isNone this.errorMessage

type Msg =
    | PasswordUpdate of string
    | PasswordRepeatUpdate of string
    | ChangeKeyPath of string
    | Validate
