[<AutoOpen>]
module SonmInstaller.Components.MainUpdate

open Elmish
open SonmInstaller
open SonmInstaller.Components
open SonmInstaller.Components.Main

module Main = 
    
    type IService = 
        inherit NewKeyPage.IService
        abstract member StartDownload: 
            progressCb: (int64 -> int64 -> unit) ->
            completeCb: (Result<unit, exn> -> unit) ->  // ToDo: make it just exn option
            unit
        abstract member OpenKeyFolder: path: string -> unit
        abstract member OpenKeyFile  : path: string -> unit

    let init (srv: IService) () =
        {
            CurrentStep = Screen.S0Welcome, 0
            StepsHistory = []
            InstallationProgress = InstallationProgress.WaitForStart
            HasWallet = false
            BackButton = button.btnHidden
            NextButton = button.btnBegin
            NewKeyState = NewKeyPage.init srv
        }, []

    module private Impl = 
    
        let startDownload (service: IService) dispatch =
            let progressCb bytesDownloaded total = 
                DownloadProgress (bytesDownloaded, total) |> dispatch
                System.Console.WriteLine ("progressCb: {0}", bytesDownloaded)
            let completeCb = DownloadComplete >> dispatch 
            service.StartDownload
                progressCb
                completeCb

        let next (service: IService) (state: Main.State) = 

            let getNextState (service: IService) (s: Main.State) =
                let goTo (screen: Screen) = s, Cmd.none, Some screen
                
                match state.CurrentScreen() with
                | Screen.S0Welcome         -> 
                    let cmd = 
                        match s.InstallationProgress with
                        | WaitForStart -> Cmd.ofSub (fun d -> d StartDownload)
                        | _            -> Cmd.none
                    let ns = { s with InstallationProgress = Downloading }
                    ns, cmd, Some Screen.S1DoYouHaveWallet
                | Screen.S1DoYouHaveWallet -> 
                    match state.HasWallet with 
                    | false                -> goTo Screen.S2a1KeyGen
                    | true                 -> goTo Screen.S2b1SelectJson
                | Screen.S2a1KeyGen        -> 
                    match s.NewKeyState.KeyJustCreated with
                    | false -> 
                        let tryCreateKey = Cmd.ofSub (fun d -> NewKeyPage.Msg.TryCreateKey |> NewKeyAction |> d)
                        s, tryCreateKey, None
                    | true  -> 
                        match s.NewKeyState.NextAllowed() with
                        | true  -> 
                            let resetNewKeyPage = Cmd.ofSub (fun d -> NewKeyPage.Msg.ResetResult |> NewKeyAction |> d)
                            s, resetNewKeyPage, Some Screen.S2a2KeyGenSuccess
                        | false -> s, Cmd.none, None
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
                { button.btnBack with 
                    Visible = getVisibility <| state.CurrentScreen () 
                    Enabled = not (state.IsPending())
                }

            let getNextBtn (state: State) = 
                let isNextAllowed = function
                    | Screen.S2a1KeyGen -> state.NewKeyState.NextAllowed()
                    | _ -> true
                let b = match state.CurrentScreen () with
                        | Screen.S0Welcome  -> button.btnBegin
                        | Screen.S6Finish   -> button.btnClose
                        | Screen.S5Progress -> button.btnHidden
                        | _                 -> button.btnNext
                { b with 
                    Enabled = isNextAllowed (state.CurrentScreen()) && not (state.IsPending())
                }
            { state with
                BackButton = getBackBtn state
                NextButton = getNextBtn state
            }, cmd

        let computeNewState (service: IService) (s: State) = function
            | Back -> 
                { s with 
                    CurrentStep = s.PrevStep ()
                    StepsHistory = s.HistoryTail()
                }, []
            | Next -> next service s
            | StartDownload -> 
                let ns = { s with InstallationProgress = Downloading }
                ns, Cmd.ofSub (startDownload service)
            | HasWallet hasWallet -> { s with HasWallet = hasWallet }, []
            | NewKeyAction action -> 
                let res, cmd = NewKeyPage.update service s.NewKeyState action
                let ns = { s with NewKeyState = res }
                if res.KeyJustCreated then 
                    next service ns 
                else 
                    ns, Cmd.map NewKeyAction cmd
            | OpenKeyDir -> 
                service.OpenKeyFolder s.NewKeyState.KeyPath
                s, []
            | OpenKeyFile -> 
                service.OpenKeyFile s.NewKeyState.KeyPath
                s, []
            | DownloadProgress (_, _) -> s, []
            | DownloadComplete res -> { s with InstallationProgress = InstallationProgress.DownloadComplete res }, []
        
    open Impl

    let update (service: IService) (state: Main.State) =
        computeNewState service state
        >> withButtons 
