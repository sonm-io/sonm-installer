[<AutoOpen>]
module SonmInstaller.Components.MainUpdate

open SonmInstaller.Components
open SonmInstaller.Components.Main

module Main = 
    
    module private Impl = 
    
        let next (state: Main.State) = 

            let getNextState (s: Main.State) =
                match state.CurrentScreen() with
                | Screen.S0Welcome         -> s, Some Screen.S1DoYouHaveWallet
                | Screen.S1DoYouHaveWallet -> 
                    match state.HasWallet with 
                    | false                -> s, Some Screen.S2a1KeyGen
                    | true                 -> s, Some Screen.S2b1SelectJson
                | Screen.S2a1KeyGen        -> 
                    let ns = { s with NewKeyState = NewKeyPage.update s.NewKeyState NewKeyPage.TryCreateKey }
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
    
            let updateStepsHistory (state: State, screen) = 
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

        let updateButtons (state: State) =
            let getBackBtn (state: State) = 
                let getVisibility = function
                    | Screen.S0Welcome
                    | Screen.S5Progress
                    | Screen.S6Finish -> false
                    | _ -> true
                { button.btnBack with Visible = getVisibility <| state.CurrentScreen () }        

            let getNextBtn (state: State) = 
                let isNextAllowed = function
                    | Screen.S2a1KeyGen -> state.NewKeyState.NextAllowed()
                    | _ -> true
                let b = match state.CurrentScreen () with
                        | Screen.S0Welcome  -> button.btnBegin
                        | Screen.S6Finish   -> button.btnClose
                        | Screen.S5Progress -> button.btnHidden
                        | _                 -> button.btnNext
                { b with Enabled = isNextAllowed (state.CurrentScreen()) }            
            { state with
                BackButton = getBackBtn state
                NextButton = getNextBtn state
            }

        let computeNewState (state: State) = function
            | Back -> 
                { state with 
                    CurrentStep = state.PrevStep ()
                    StepsHistory = state.HistoryTail()
                }
            | Next -> next state
            | HasWallet hasWallet -> { state with HasWallet = hasWallet }
            | NewKeyAction action -> 
                { state with 
                    NewKeyState = NewKeyPage.update state.NewKeyState action
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
        
    let update (state: Main.State) =
        state |> Impl.computeNewState
        >> Impl.updateButtons 
        >> fun s -> s, []