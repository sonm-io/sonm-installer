namespace SonmInstaller.Components.NewKeyPage



type State = {
    Password: string
    PasswordRepeat: string
    ErrorMessage: string option
    KeyPath: string
}
with
    member this.NextAllowed () = 
        (this.Password <> "" || this.PasswordRepeat <> "")
        && Option.isNone this.ErrorMessage

type Msg =
    | PasswordUpdate of string
    | PasswordRepeatUpdate of string
    | ChangeKeyPath of string
    | Validate
