[<AutoOpen>]
module SonmInstaller.Components.MainUpdate
open System
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
        abstract member CloseApp: unit -> unit

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
                address = ""
                thresholdPayout = "1000"
            }
            selectedDrive = None
        }, []

    module private Impl = 
    
        let goToStep (s: Main.State) step =
            { s with 
                    currentStep = step 
                    stepsHistory = s.currentStep::s.stepsHistory
            }


        // Exceptions Helpers (MessagePage helpers)
        module ExnHlp = 

            let getMessagePage header message tryAgainVisible = 
                {
                    header = header
                    message = message
                    tryAgainVisible = tryAgainVisible
                }
        
            let showExn (tryAgainVisible: bool) header (s: State) (e: exn) =
                { s with show = ShowMessagePage <| getMessagePage header (Exn.getMessagesStack e) tryAgainVisible }
        
        // Async Commands Helpers
        module AsyncHlp = 

            let mapStateOk map (state, res) = 
                match res with
                | Ok value -> map state value
                | Error _ -> state
                , res

            let startAsync (s: Main.State) (msg: AsyncTask<'res> -> Msg) =
                let cmd = Cmd.ofSub (fun d -> AsyncTask.Start |> msg |> d)
                s, cmd, None

            let getAsyncCmd (serviceMethod: unit -> Async<'res>) mapMsg = 
                Cmd.ofAsync
                    serviceMethod 
                    ()
                    (fun res -> res |> Ok |> mapMsg)
                    (fun e -> e |> Error  |> mapMsg)

            let getPending (s: State) (serviceMethod: unit -> Async<'res>) mapMsg = 
                let cmd = getAsyncCmd serviceMethod mapMsg
                { s with isPending = true }, cmd

                
            let toScreen (scr: Screen) errorHeader (s, res) = 
                match res with
                | Ok _ -> goToStep s (scr, s.CurrentStepNum () + 1)
                | Error e -> ExnHlp.showExn false errorHeader s e
                , res

            let processTask 
                (s: State) 
                (serviceMethod: unit -> Async<'res>) 
                mapMsg 
                (mapRes: State * Result<'res,exn> -> State * Cmd<Msg>) 
                = function
                | Start -> 
                    getPending s serviceMethod (AsyncTask.Complete >> mapMsg)
                | Complete res -> 
                    mapRes ({ s with isPending = false }, res)

        let startDownload (service: IService) dispatch =
            let progressCb bytesDownloaded total = 
                Download.Progress (bytesDownloaded, total) |> dispatch
                System.Console.WriteLine ("progressCb: {0}", bytesDownloaded)
            let completeCb = Download.Complete >> dispatch 
            service.StartDownload
                progressCb
                completeCb

        let nextBtn (srv: IService) (state: Main.State) = 

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
                    | true  -> AsyncHlp.startAsync ns GenerateKey
                    | false -> ns, Cmd.none, None
                | Screen.S2a2KeyGenSuccess -> goTo Screen.S3MoneyOut
                | Screen.S2b1SelectJson    -> goTo Screen.S2b2JsonPassword
                | Screen.S2b2JsonPassword  -> AsyncHlp.startAsync s (ImportKey.Import >> Msg.ImportKey)
                | Screen.S3MoneyOut        -> AsyncHlp.startAsync s CallSmartContract
                | Screen.S4SelectDisk      -> AsyncHlp.startAsync s MakeUsbStick
                | Screen.S5Progress
                | Screen.S6Finish          -> 
                    let closeApp = Cmd.ofSub (fun _ -> srv.CloseApp())
                    s, closeApp, None
                | _                        -> failwith "Unknown case"
    
            userTriesToGetNextState state
            |> fun (s, cmd, scr) -> 
                scr 
                |> Option.map (fun scr -> scr, s.CurrentStepNum()+1)
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
                    | Screen.S2a1KeyGen       -> s.newKeyState.NextAllowed()
                    | Screen.S2b2JsonPassword -> not <| String.IsNullOrEmpty(s.existingKeystore.password)
                    | Screen.S4SelectDisk     -> s.selectedDrive.IsSome
                    | _ -> true
                let b = match s.CurrentScreen () with
                        | Screen.S0Welcome    -> button.btnBegin
                        | Screen.S6Finish     -> button.btnClose
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
            | GenerateKey task -> 
                task 
                |> AsyncHlp.processTask s
                    (fun () -> srv.GenerateKeyStore s.newKeyState.password)
                    GenerateKey
                    (AsyncHlp.mapStateOk (fun s addr -> { s with etherAddress = Some addr }) 
                    >> AsyncHlp.toScreen Screen.S2a2KeyGenSuccess "Key Store Generation Error:" 
                    >> fun (s, _) -> s, Cmd.none)
            | OpenKeyDir -> 
                srv.OpenKeyFolder s.newKeyState.keyPath
                s, Cmd.none
            | OpenKeyFile -> 
                srv.OpenKeyFile s.newKeyState.keyPath
                s, Cmd.none
            | ImportKey act -> 
                match act with
                | ImportKey.ChoosePath path -> 
                    let keyStore = { s.existingKeystore with path = Some path }
                    { s with existingKeystore = keyStore }, Cmd.none
                | ImportKey.ChangePassword pass -> 
                    let keyStore = { s.existingKeystore with password = pass }
                    { s with existingKeystore = keyStore }, Cmd.none
                | ImportKey.Import task -> 
                    task 
                    |> AsyncHlp.processTask s
                        (fun () -> srv.ImportKeyStore s.existingKeystore.password)
                        (ImportKey.Import >> Msg.ImportKey)
                        (AsyncHlp.mapStateOk (fun s addr -> { s with etherAddress = Some addr })
                        >> AsyncHlp.toScreen Screen.S3MoneyOut "Key Store Import Error:"
                        >> fun (s, _) -> s, Cmd.none)
            | Withdraw msg -> 
                match msg with
                | Address addr -> { s with withdraw = { s.withdraw with address = addr }}, Cmd.none
                | Threshold value -> { s with withdraw = { s.withdraw with thresholdPayout = value }}, Cmd.none
            | CallSmartContract task ->
                task
                |> AsyncHlp.processTask s
                    (fun () -> srv.CallSmartContract s.withdraw.address (float s.withdraw.thresholdPayout))
                    (CallSmartContract)
                    (AsyncHlp.toScreen Screen.S4SelectDisk "Call Smart Contract Failed"
                    >> fun (s, _) -> s, Cmd.none)
            | SelectDrive drive -> { s with selectedDrive = Some drive }, Cmd.none 
            | MakeUsbStick task -> 
                task
                |> AsyncHlp.processTask s
                    (fun () -> srv.MakeUsbStick s.selectedDrive.Value.Value)
                    MakeUsbStick
                    (AsyncHlp.toScreen Screen.S6Finish "Error" 
                    >> fun (s, _) -> s, Cmd.none)
        
    open Impl

    let update (service: IService) (state: Main.State) =
        computeNewState service state
        >> withButtons 
