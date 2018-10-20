[<AutoOpen>]
module SonmInstaller.Components.MainUpdate

open Elmish
open SonmInstaller.Tools
open SonmInstaller.Components
open SonmInstaller.Components.Main
open SonmInstaller.Components.Main.Msg

module Main = 
    
    type IService = 
        inherit NewKeyPage.IService
        abstract member StartDownload: 
            progressCb: (int64 -> int64 -> unit) ->
            completeCb: (Result<unit, exn> -> unit) ->  // ToDo: make it just exn option
            unit
        abstract member GenerateKeyStore : password: string -> Async<string>
        abstract member ImportKeyStore   : password: string -> Async<string>
        abstract member OpenKeyFolder : path: string -> unit
        abstract member OpenKeyFile   : path: string -> unit
        abstract member CallSmartContract: withdrawTo: string -> minPayout: float -> Async<unit>
        abstract member MakeUsbStick: drive: int -> Async<unit>

    let init (srv: IService) () =
        {
            currentStep = Screen.S0Welcome, 0
            stepsHistory = []
            show = ShowStep
            installationProgress = InstallationProgress.WaitForStart
            hasWallet = false
            backButton = button.btnHidden
            nextButton = button.btnBegin
            newKeyState = NewKeyPage.init srv
            existingKeystore = {
                path = None
                password = ""
            }
            isPending = false
            etherAddress = None
            withdraw = {
                withdrawAddress = ""
                thresholdPayout = ""
            }
            selectedDrive = None
        }, []

    module private Impl = 
        open System.IO
    
        let startDownload (service: IService) dispatch =
            let progressCb bytesDownloaded total = 
                Download.Progress (bytesDownloaded, total) |> dispatch
                System.Console.WriteLine ("progressCb: {0}", bytesDownloaded)
            let completeCb = Download.Complete >> dispatch 
            service.StartDownload
                progressCb
                completeCb
        
        let goToStep (s: Main.State) step =
            { s with 
                    currentStep = step 
                    stepsHistory = s.currentStep::s.stepsHistory
            }

        let nextBtn (service: IService) (state: Main.State) = 

            let userTriesToGetNextState (s: Main.State) =
                let stay = s, Cmd.none, None
                let goTo (screen: Screen) = s, Cmd.none, Some screen
                
                match s.CurrentScreen() with
                | Screen.S0Welcome         -> 
                    let ns, cmd = 
                        match s.installationProgress with
                        | WaitForStart -> 
                            let ns = { s with installationProgress = Downloading }
                            let cmd = Cmd.ofSub (fun d -> Download.Start |> Msg.Download |> d)
                            ns, cmd
                        | _            -> s, Cmd.none
                    ns, cmd, Some Screen.S1DoYouHaveWallet
                | Screen.S1DoYouHaveWallet -> 
                    match s.hasWallet with 
                    | false                -> goTo Screen.S2a1KeyGen
                    | true                 -> goTo Screen.S2b1SelectJson
                | Screen.S2a1KeyGen        -> 
                    let keyGenState = NewKeyPage.update s.newKeyState NewKeyPage.Msg.Validate
                    let ns = { s with newKeyState = keyGenState }
                    match keyGenState.NextAllowed() with
                    | true  -> 
                        let tryCreateKey = Cmd.ofSub (fun d -> AsyncTask.Start |> GenerateKey |> d)
                        ns, tryCreateKey, None
                    | false -> ns, Cmd.none, None
                | Screen.S2a2KeyGenSuccess -> goTo Screen.S3MoneyOut
                | Screen.S2b1SelectJson    -> goTo Screen.S2b2JsonPassword
                | Screen.S2b2JsonPassword  -> goTo Screen.S3MoneyOut
                | Screen.S3MoneyOut        -> 
                    let callSC = Cmd.ofSub (fun d -> AsyncTask.Start |> CallSmartContract |> d)
                    s, callSC, None
                | Screen.S4SelectDisk      -> 
                    goTo Screen.S5Progress
                | Screen.S5Progress
                | Screen.S6Finish          -> failwith "Can't move next from here"
                | _                        -> failwith "Unknown case"
    
            userTriesToGetNextState state
            |> fun (s, cmd, scr) -> 
                scr 
                |> Option.map (fun scr -> scr, (s.CurrentStepNum() + 1))
                |> function
                    | Some step -> goToStep s step
                    | _ -> s
                , cmd
            
        let withButtons (state: State, cmd: Cmd<Msg>) =
            let getBackBtn (state: State) = 
                let getVisibility = function
                    | Screen.S0Welcome
                    | Screen.S5Progress
                    | Screen.S6Finish -> false
                    | _ -> true
                { button.btnBack with 
                    Visible = getVisibility <| state.CurrentScreen () 
                    Enabled = not state.isPending
                }

            let getNextBtn (s: State) = 
                let isNextAllowedOnScreen = function
                    | Screen.S2a1KeyGen -> s.newKeyState.NextAllowed()
                    | _ -> true
                let b = match s.CurrentScreen () with
                        | Screen.S0Welcome    -> button.btnBegin
                        | Screen.S6Finish     -> button.btnClose
                        | Screen.S4SelectDisk -> 
                            match s.selectedDrive with
                            | Some _ -> button.btnNext
                            | None   -> button.btnHidden
                        | Screen.S5Progress   -> button.btnHidden
                        | _                   -> button.btnNext
                { b with 
                    Enabled = 
                        s.show |> function 
                            | ShowStep -> isNextAllowedOnScreen (s.CurrentScreen()) 
                            | ShowMessagePage _ -> false
                        && not s.isPending
                }
            
            { state with
                backButton = getBackBtn state
                nextButton = getNextBtn state
            }, cmd

        let getMessagePage header message tryAgainVisible = 
            {
                header = header
                message = message
                tryAgainVisible = tryAgainVisible
            }
        
        let getAsyncCmd (serviceMethod: 'arg -> Async<'res>) arg mapResult = 
            Cmd.ofAsync
                serviceMethod 
                arg
                (fun res -> res |> Ok |> mapResult)
                (fun e -> e |> Error  |> mapResult)

        let keyCreationComplete (s: Main.State) successScreen errorHeader res =
            let keyCreationSuccess (s: Main.State) address nextScreen = 
                let step = nextScreen, (s.CurrentStepNum() + 1)
                { goToStep s step with etherAddress = Some address; isPending = false }, Cmd.none
            let keyCreationFail (s: Main.State) (e: exn) header = 
                let showMessage = ShowMessagePage <| getMessagePage header e.Message false
                { s with show = showMessage; isPending = false }, Cmd.none
            match res with
            | Ok address -> keyCreationSuccess s address successScreen
            | Error e -> keyCreationFail s e errorHeader            

        let computeNewState (srv: IService) (s: State) = function
            | BackBtn -> 
                match s.show with
                | ShowStep -> 
                    { s with 
                        currentStep = s.PrevStep ()
                        stepsHistory = s.HistoryTail()
                    }, Cmd.none
                | ShowMessagePage _ -> { s with show = ShowStep }, Cmd.none
            | NextBtn -> nextBtn srv s
            | Download act -> 
                match act with
                | Download.Start -> 
                    let ns = { s with installationProgress = Downloading }
                    ns, Cmd.map Msg.Download (Cmd.ofSub (startDownload srv))
                | Download.Progress (_, _) -> s, Cmd.none
                | Download.Complete res -> { s with installationProgress = InstallationProgress.DownloadComplete res }, Cmd.none
            | HasWallet hasWallet -> { s with hasWallet = hasWallet }, Cmd.none
            | NewKeyMsg action -> 
                let res = NewKeyPage.update s.newKeyState action
                let ns = { s with newKeyState = res }
                ns, Cmd.none
            | GenerateKey action ->
                match action with
                | AsyncTask.Start -> 
                    let mapResult = AsyncTask.Complete >> GenerateKey
                    let cmd = getAsyncCmd srv.GenerateKeyStore s.newKeyState.Password mapResult
                    { s with isPending = true }, cmd
                | AsyncTask.Complete res -> keyCreationComplete s Screen.S2a2KeyGenSuccess "Key Store Generation Error:" res
            | OpenKeyDir -> 
                srv.OpenKeyFolder s.newKeyState.KeyPath
                s, Cmd.none
            | OpenKeyFile -> 
                srv.OpenKeyFile s.newKeyState.KeyPath
                s, Cmd.none
            | ImportKey act -> 
                match act with
                | ImportKey.ChoosePath path -> 
                    let keyStore = { s.existingKeystore with path = Some path }
                    { s with existingKeystore = keyStore }, Cmd.none
                | ImportKey.ChangePassword pass -> 
                    let keyStore = { s.existingKeystore with password = pass }
                    { s with existingKeystore = keyStore }, Cmd.none
                | ImportKey.Import act -> 
                    match act with
                    | AsyncTask.Start -> 
                        let mapResult = AsyncTask.Complete >> ImportKey.Import >> Msg.ImportKey
                        let cmd = getAsyncCmd srv.ImportKeyStore s.existingKeystore.password mapResult
                        { s with isPending = true }, cmd
                    | AsyncTask.Complete res -> keyCreationComplete s Screen.S3MoneyOut "Key Store Import Error:" res
            | Withdraw msg -> 
                match msg with
                | Address addr -> { s with withdraw = { s.withdraw with withdrawAddress = addr }}, Cmd.none
                | Threshold value -> { s with withdraw = { s.withdraw with thresholdPayout = value }}, Cmd.none
            | CallSmartContract task ->
                match task with
                | Start -> 
                    let cmd = 
                        getAsyncCmd
                            (srv.CallSmartContract s.withdraw.withdrawAddress)
                            (float s.withdraw.thresholdPayout)
                            (AsyncTask.Complete >> CallSmartContract)
                    s, cmd
                | Complete res -> 
                    match res with
                    | Ok () -> goToStep s (Screen.S4SelectDisk, (s.CurrentStepNum() + 1)), Cmd.none
                    | Error e -> 
                        let msgPage = getMessagePage "Call Smart Contract Failed" (Exn.getMessagesStack e) false
                        { s with show = ShowMessagePage msgPage }, Cmd.none
            | SelectDrive drive -> { s with selectedDrive = Some drive }, Cmd.none 
            | MakeUsbStick task -> 
                match task with
                | Start -> 
                    let cmd = 
                        getAsyncCmd
                            srv.MakeUsbStick
                            (s.selectedDrive.Value.Value)
                            (AsyncTask.Complete >> MakeUsbStick)
                    s, cmd
                | Complete res -> 
                    match res with
                    | Ok () -> goToStep s (Screen.S6Finish, (s.CurrentStepNum() + 1)), Cmd.none
                    | Error e -> 
                        let msgPage = getMessagePage "Error" (Exn.getMessagesStack e) false
                        { s with show = ShowMessagePage msgPage }, Cmd.none
        
    open Impl

    let update (service: IService) (state: Main.State) =
        computeNewState service state
        >> withButtons 
