[<AutoOpen>]
module SonmInstaller.Components.MainUpdate

open Elmish
open SonmInstaller
open SonmInstaller.Components
open SonmInstaller.Components.Main

module Main = 
    
    module private Impl = 
        open SonmInstaller.Domain
    
        let next (service: Service) (state: Main.State) = 

            let getNextState (service: Service) (s: Main.State) =
                let goTo (screen: Screen) = 
                    s, [], Some screen
                
                match state.CurrentScreen() with
                | Screen.S0Welcome         -> 
                    let sub dispatch =
                        let progressCb bytesDownloaded total = 
                            DownloadProgress (bytesDownloaded, total) |> dispatch
                            System.Console.WriteLine ("progressCb: {0}", bytesDownloaded)
                        let completeCb = fun () -> dispatch DownloadComplete
                        
                        service.startDownload
                            progressCb
                            completeCb
                    let ns = { s with InstallationStatus = Downloading }
                    ns, [sub], Some Screen.S1DoYouHaveWallet
                | Screen.S1DoYouHaveWallet -> 
                    match state.HasWallet with 
                    | false                -> goTo Screen.S2a1KeyGen
                    | true                 -> goTo Screen.S2b1SelectJson
                | Screen.S2a1KeyGen        -> 
                    let ns = { s with NewKeyState = NewKeyPage.update s.NewKeyState NewKeyPage.TryCreateKey }
                    match ns.NewKeyState.NextAllowed() with
                    | true                 -> ns, [], Some Screen.S2a2KeyGenSuccess
                    | false                -> ns, [], None
                | Screen.S2a2KeyGenSuccess -> goTo Screen.S3MoneyOut
                | Screen.S2b1SelectJson    -> goTo Screen.S2b2JsonPassword
                | Screen.S2b2JsonPassword  -> goTo Screen.S3MoneyOut
                | Screen.S3MoneyOut        -> goTo Screen.S4SelectDisk
                | Screen.S4SelectDisk      -> goTo Screen.S5Progress
                | Screen.S5Progress
                | Screen.S6Finish          -> failwith "Can't move next from here"
                | _                        -> failwith "Unknown case"
    
            let withHistory (state: State, cmd, screen) = 
                let newState = 
                    match screen with
                    | Some screen -> 
                        { state with 
                            CurrentStep = (screen, state.CurrentStepNum() + 1) 
                            StepsHistory = state.CurrentStep::state.StepsHistory
                        }
                    | _ -> state
                newState, cmd

            getNextState service state
            |> withHistory

        let withButtons (state: State, cmd: Cmd<Main.Msg>) =
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
            }, cmd

        let computeNewState (service: Service) (state: State) = function
            | Back -> 
                { state with 
                    CurrentStep = state.PrevStep ()
                    StepsHistory = state.HistoryTail()
                }, []
            | Next -> next service state
            | HasWallet hasWallet -> { state with HasWallet = hasWallet }, []
            | NewKeyAction action -> 
                { state with 
                    NewKeyState = NewKeyPage.update state.NewKeyState action
                }, []
            | OpenKeyDir -> 
                service.openKeyFolder state.NewKeyState.KeyPath
                state, []
            | OpenKeyFile -> 
                service.openKeyFile state.NewKeyState.KeyPath
                state, []
            | DownloadProgress (_, _) -> state, []
            | DownloadComplete -> { state with InstallationStatus = InstallationStatus.DownloadComplete }, []
        
    open Impl

    let update (service: Domain.Service) (state: Main.State) =
        computeNewState service state
        >> withButtons 
