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

module NewKeyPage = 
    type State = {
        Password: string;
        PasswordRepeat: string;
        ErrorMessage: string option;
    }
    with
        member this.NextAllowed () = 
            (this.Password <> "" || this.PasswordRepeat <> "")
            && Option.isNone this.ErrorMessage

    type Action =
        | PasswordUpdate of string
        | PasswordRepeatUpdate of string
        | Validate

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
        | Validate -> { state with ErrorMessage = Private.validateState state }

    let initial = { Password = ""; PasswordRepeat = ""; ErrorMessage = None }

[<AutoOpen>]
module Main = 
    
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
        NewKeyState: NewKeyPage.State
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
        | NewKeyAction of NewKeyPage.Action
        | ChangeInstallationStatus of InstallationStatus
    
    module private Private = 
    
        let next (state: UiState) = 
            let getNextState (s: UiState) =
                match state.CurrentScreen() with
                | Screen.S0Welcome         -> s, Some Screen.S1DoYouHaveWallet
                | Screen.S1DoYouHaveWallet -> 
                    match state.HasWallet with 
                    | false                -> s, Some Screen.S2a1KeyGen
                    | true                 -> s, Some Screen.S2b1SelectJson
                | Screen.S2a1KeyGen        -> 
                    let ns = { s with NewKeyState = NewKeyPage.update s.NewKeyState NewKeyPage.Validate }
                    match ns.NewKeyState.NextAllowed() with
                    | true                 -> ns, Some Screen.S2a2KeyGenSuccess
                    | false                -> ns, None
                | Screen.S2a2KeyGenSuccess -> s, Some Screen.S3MoneyOut
                | Screen.S2b1SelectJson    -> s, Some Screen.S2b2JsonPassword
                | Screen.S2b2JsonPassword  -> s, Some Screen.S3MoneyOut
                | Screen.S3MoneyOut        -> s, Some Screen.S4SelectDisk
                | Screen.S4SelectDisk      -> s, Some Screen.S5Progress
                | Screen.S5Progress
                | Screen.S6Finish          -> failwith "Can't move next from here"
                | _                        -> failwith "Unknown case"
    
            let updateStepsHistory (state: UiState, screen) = 
                match screen with
                | Some screen -> 
                    { state with 
                        CurrentStep = (screen, state.CurrentStepNum() + 1) 
                        StepsHistory = state.CurrentStep::state.StepsHistory
                    }
                | _ -> state

            state
            |> getNextState
            |> updateStepsHistory

        let updateButtons (state: UiState) =
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
            { state with
                BackButton = getBackBtn state
                NextButton = getNextBtn state
            }

        let computeNewState (state: UiState) = function
            | Back -> 
                { state with 
                    CurrentStep = state.PrevStep ()
                    StepsHistory = state.HistoryTail()
                }
            | Next -> next state
            | HasWallet hasWallet -> { state with HasWallet = hasWallet }
            | NewKeyAction action -> 
                let s = NewKeyPage.update state.NewKeyState action
                { state with 
                    NewKeyState = s
                }
            | ChangeInstallationStatus status -> { state with InstallationStatus = status }
        
    let update (state: UiState) =
        (state |> Private.computeNewState) 
        >> Private.updateButtons 
       
    let initial = {
        CurrentStep = Screen.S0Welcome, 0
        StepsHistory = []
        InstallationStatus = InstallationStatus.WaitForStart
        HasWallet = false
        BackButton = btnHidden
        NextButton = btnBegin
        NewKeyState = NewKeyPage.initial
    }