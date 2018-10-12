namespace SonmInstaller.Components.NewKeyPage

type State = {
    Password: string;
    PasswordRepeat: string;
    ErrorMessage: string option;
    KeyPath: string;
    KeyContent: string option;
}
with
    member this.NextAllowed () = 
        (this.Password <> "" || this.PasswordRepeat <> "")
        && Option.isNone this.ErrorMessage

type Action =
    | PasswordUpdate of string
    | PasswordRepeatUpdate of string
    | ChangeKeyPath of string
    | TryCreateKey

[<AutoOpen>]
module Init = 
    open SonmInstaller
    let init () = { 
        Password = ""
        PasswordRepeat = ""
        ErrorMessage = None 
        KeyPath = Tools.defaultNewKeyPath // ToDo: Move to dependency
        KeyContent = None
    }