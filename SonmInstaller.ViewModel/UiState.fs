namespace SonmInstaller.ViewModel

type Screen = 
    | S0Welcome
    | S1DoYouHaveWallet
    | S2a1KeyGen
    | S2a2KeyGenSuccess
    | S2bSelectJson
    | S3MoneyOut
    | S4SelectDisk
    | S5Progress
    | S6Finish

type ActionButtonState = {
    Caption: string;
    Disabled: bool;
    Visible: bool;
}


module KeyGenPage = 
    type KeyGenState = {
        Password: string;
        PasswordRepeat: string;
        ErrorMessage: string option;
    }

    type KeyGenAction =
        | Password of string
        | PasswordRepeat of string

    let private validate p1 p2 =
        if p1 <> p2 then
            Some "Passwords didn't match"
        else
            None

    let update (state: KeyGenState) (action: KeyGenAction) : KeyGenState =
        match action with
            | Password p ->
                let msg = validate p state.PasswordRepeat
                { state with Password = p; ErrorMessage = msg }
            | PasswordRepeat p -> 
                let msg = validate state.Password p
                { state with PasswordRepeat = p; ErrorMessage = msg }

module SelectJsonPage =
    type SelectJsonState = {
        PathVisible: bool
        LinkVisible: bool
        ButtonVisible: bool
    }

    type SelectJsonAction =
        | SelectJson of string

    let update = function
        | SelectJson "" -> {
                PathVisible = false
                LinkVisible = false
                ButtonVisible = true
            }
        | _ -> {
                PathVisible = true
                LinkVisible = true
                ButtonVisible = false
            }

type UiState = {
    BackButton: ActionButtonState;
    NextButton: ActionButtonState;
    BytesDownloaded: int;
    BytesTotal: int;
    Screen: Screen;
    StepNum: byte;
    StepsTotal: byte;
    KeyGenPage: KeyGenPage.KeyGenState;
}

type ButtonChange = Caption | Disabled | Visible

type UiStateChange =
    | BackButton of ButtonChange list
    | NextButton of ButtonChange list
    | BytesDownloaded
    | Screen
    | Step
    
type UiStateChanges = UiStateChange list

module X = 

    let x = [
        BackButton([Caption; Visible])
        Screen
    ]