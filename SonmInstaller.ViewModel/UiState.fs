namespace SonmInstaller.ViewModel

[<AutoOpen>]
module Button = 

    type ButtonState = {
        Text: string;
        Enabled: bool;
        Visible: bool;
    }

    let private btnEmpty = { Text = ""; Enabled = true; Visible = true }
    let private createBtn text = { btnEmpty with Text = text }
    let btnHidden = { Text = ""; Enabled = true; Visible = false }
    let btnBegin  = createBtn "Begin"
    let btnBack   = createBtn "Back"
    let btnNext   = createBtn "Next"
    let btnClose  = createBtn "Close"

[<AutoOpen>]
module NewKeyPage = 
    type NewKeyState = {
        Password: string;
        PasswordRepeat: string;
        ErrorMessage: string option;
    }
    with
        member this.NextAllowed () = 
            (this.Password <> "" || this.PasswordRepeat <> "") 
            && Option.isNone this.ErrorMessage

    type NewKeyAction =
        | PasswordUpdate of string
        | PasswordRepeatUpdate of string
        | ResetErrorMessage

    let private validate p1 p2 =
        if p1 <> p2 then
            Some "Passwords didn't match"
        else
            None

    let update (state: NewKeyState) = function
        | PasswordUpdate p ->
            let msg = validate p state.PasswordRepeat
            { state with Password = p; ErrorMessage = msg }
        | PasswordRepeatUpdate p -> 
            let msg = validate state.Password p
            { state with PasswordRepeat = p; ErrorMessage = msg }
        | ResetErrorMessage -> { state with ErrorMessage = None }

    let initial = { Password = ""; PasswordRepeat = ""; ErrorMessage = None }

[<AutoOpen>]
module Main = 
    
    open Button
    open NewKeyPage

    type Screen = 
        | S0Welcome = 0
        | S1DoYouHaveWallet = 1
        | S2a1KeyGen = 2
        | S2a2KeyGenSuccess = 3
        | S2b1SelectJson = 4
        | S2b2JsonPassword = 5
        | S3MoneyOut = 6
        | S4SelectDisk = 7
        | S5Progress = 8
        | S6Finish = 9
    
    type Step = Screen * int // screen * stepNum

    type InstallationStatus =
        | WaitForStart
        | Downloading
        | Writing
        | Finish

    type UiState = {
        CurrentStep: Step
        StepsHistory: Step list
        InstallationStatus: InstallationStatus
        HasWallet: bool
        BackButton: ButtonState
        NextButton: ButtonState
        NewKeyState: NewKeyState
    }
    with
        member this.IsAtBeginning () = this.StepsHistory = []
        member this.HistoryTail () =
            match this.StepsHistory with
            | _::tail -> tail
            | _ -> []
        member this.PrevStep () = 
            match this.StepsHistory with
            | head::_ -> head
            | _ -> this.CurrentStep
        member this.CurrentScreen () = this.CurrentStep |> fst
        member this.CurrentStepNum () = this.CurrentStep |> snd

    type UiStateAction =
        | Back
        | Next
        | HasWallet of bool
        | NewKeyAction of NewKeyAction
        | ChangeInstallationStatus of InstallationStatus
    
    let private getNextStep (state: UiState) =
        let (screen, step) = state.CurrentStep
        let newScreen = 
            match screen with
            | Screen.S0Welcome         -> Screen.S1DoYouHaveWallet
            | Screen.S1DoYouHaveWallet -> 
                match state.HasWallet with 
                | false                -> Screen.S2a1KeyGen
                | true                 -> Screen.S2b1SelectJson
            | Screen.S2a1KeyGen        -> Screen.S2a2KeyGenSuccess
            | Screen.S2a2KeyGenSuccess -> Screen.S3MoneyOut
            | Screen.S2b1SelectJson    -> Screen.S2b2JsonPassword
            | Screen.S2b2JsonPassword  -> Screen.S3MoneyOut
            | Screen.S3MoneyOut        -> Screen.S4SelectDisk
            | Screen.S4SelectDisk      -> Screen.S5Progress
            | Screen.S5Progress
            | Screen.S6Finish          -> failwith "Can't move next from here"
            | _                        -> failwith "Unknown case"
        (newScreen, step+1)
    
   
    let getBackBtn (state: UiState) = 
        let getVisibility = function
            | Screen.S0Welcome
            | Screen.S5Progress
            | Screen.S6Finish -> false
            | _ -> true
        { btnBack with Visible = getVisibility <| state.CurrentScreen () }        

    let getNextBtn (state: UiState) = 
        let isNextAllowed = function
            | Screen.S2a1KeyGen -> state.NewKeyState.NextAllowed()
            | _ -> true
        let b = match state.CurrentScreen () with
                | Screen.S0Welcome  -> btnBegin
                | Screen.S6Finish   -> btnClose
                | Screen.S5Progress -> btnHidden
                | _                 -> btnNext
        { b with Enabled = isNextAllowed (state.CurrentScreen()) }

    let updateButtons (state: UiState) =
        { state with
            BackButton = getBackBtn state
            NextButton = getNextBtn state
        }

    let update (state: UiState) = function
        | Back -> 
            { state with 
                CurrentStep = state.PrevStep ()
                StepsHistory = state.HistoryTail()
            } |> updateButtons
        | Next -> 
            { state with
                CurrentStep = getNextStep state
                StepsHistory = state.CurrentStep::state.StepsHistory
            } |> updateButtons
        | HasWallet hasWallet -> { state with HasWallet = hasWallet }
        | NewKeyAction action -> 
            let s = NewKeyPage.update state.NewKeyState action
            { state with 
                NewKeyState = s
            } |> updateButtons
        | ChangeInstallationStatus status -> { state with InstallationStatus = status }
       
    let initial = {
        CurrentStep = Screen.S0Welcome, 0
        StepsHistory = []
        InstallationStatus = InstallationStatus.WaitForStart
        HasWallet = false
        BackButton = btnHidden
        NextButton = btnBegin
        NewKeyState = NewKeyPage.initial
    }