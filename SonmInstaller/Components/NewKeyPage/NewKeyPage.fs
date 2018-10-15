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

type Msg =
    | PasswordUpdate of string
    | PasswordRepeatUpdate of string
    | ChangeKeyPath of string
    | TryCreateKey


namespace SonmInstaller.Components
open SonmInstaller.Components.NewKeyPage

module newKeyPage = 
    open SonmInstaller
    let init = { 
        Password = ""
        PasswordRepeat = ""
        ErrorMessage = None 
        KeyPath = Tools.defaultNewKeyPath // ToDo: Move to dependency
        KeyContent = None
    }