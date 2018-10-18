namespace SonmInstaller.Components.NewKeyPage



type State = {
    Password: string
    PasswordRepeat: string
    ErrorMessage: string option
    KeyPath: string
    IsPending: bool
    KeyJustCreated: bool
}
with
    member this.NextAllowed () = 
        (this.Password <> "" || this.PasswordRepeat <> "")
        && Option.isNone this.ErrorMessage
        && not this.IsPending

type Msg =
    | PasswordUpdate of string
    | PasswordRepeatUpdate of string
    | ChangeKeyPath of string
    | TryCreateKey
    | FinishCreateKey of exn option // if success then None, else Some exn.
    | ResetResult
