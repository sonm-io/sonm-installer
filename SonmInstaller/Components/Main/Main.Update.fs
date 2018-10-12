module SonmInstaller.Components.Main.Update

open SonmInstaller.Components
open Button.Init  // ToDo: need refactoring - Button.Init.btnBegin - thats bad name, NewKeyPage.Update.update - bad name too.

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
                let ns = { s with NewKeyState = NewKeyPage.Update.update s.NewKeyState NewKeyPage.TryCreateKey }
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
            { state with 
                NewKeyState = NewKeyPage.Update.update state.NewKeyState action
            }
        | OpenKeyDir -> 
            // ToDo use dependency service here
            // let path = Path.GetDirectoryName(state.NewKeyState.KeyPath)
            // Process.Start path |> ignore
            state
        | OpenKeyFile -> 
            // Process.Start (state.NewKeyState.KeyPath) |> ignore
            state
        | ChangeInstallationStatus status -> { state with InstallationStatus = status }
        
let update (state: UiState) =
    state |> Private.computeNewState
    >> Private.updateButtons 
    >> fun s -> s, []