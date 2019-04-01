[<AutoOpen>]
module SonmInstaller.Components.MainUpdate
open System
open Elmish
open SonmInstaller.Tools
open SonmInstaller.Components
open SonmInstaller.Components.Main
open SonmInstaller.Components.Main.Msg

module Main = 
    open SonmInstaller.ReleaseMetadata
    
    type IService = 
        inherit NewKeyPage.IService
        abstract member IsProcessElevated: unit -> bool
        abstract member GetUsbDrives: unit -> UsbDrive list
        abstract member DownloadMetadata : progress: (Progress.State -> unit) -> Async<ChannelMetadata>
        abstract member DownloadRelease : arg: Release -> progress: (Progress.State -> unit) -> Async<Release>
        abstract member GenerateKeyStore : path: string -> password: string -> Async<string>
        abstract member ImportKeyStore   : path: string -> password: string -> Async<string>
        abstract member OpenKeyFolder : path: string -> unit
        abstract member OpenKeyFile   : path: string -> unit
        abstract member CallSmartContract: withdrawTo: string -> minPayout: float -> Async<unit>
        abstract member MakeUsbStick: drive: int -> wipe: bool -> release: Release -> progress: (Progress.State -> unit) -> Async<unit>
        abstract member CloseApp: unit -> unit

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

            let mapStateOk map (state: State, res: Result<'r, 'e>) = 
                match res with
                | Result.Ok value -> map state value
                | Result.Error _ -> state
                , res

            let startAsync (msg: AsyncTask<'res> -> Msg) = Cmd.ofSub (fun d -> AsyncTask.Start |> msg |> d)

            let getAsyncCmd (serviceMethod: unit -> Async<'res>) mapMsg = 
                Cmd.ofAsync
                    serviceMethod 
                    ()
                    (fun res -> res |> Result.Ok |> mapMsg)
                    (fun e -> e |> Error  |> mapMsg)

            let getPending (s: State) (serviceMethod: unit -> Async<'res>) mapMsg = 
                let cmd = getAsyncCmd serviceMethod mapMsg
                { s with isPending = true }, cmd

                
            let toScreen (scr: Screen) errorHeader (s, res) = 
                match res with
                | Result.Ok _ -> goToStep s (scr, s.CurrentStepNum () + 1)
                | Result.Error e -> ExnHlp.showExn false errorHeader s e
                , res

            let processTask 
                (s: State) 
                (serviceMethod: unit -> Async<'res>) 
                mapMsg 
                (mapRes: State * Result<'res,exn> -> State * Cmd<Msg>) 
                = function
                | AsyncTask.Start -> 
                    getPending s serviceMethod (AsyncTask.Complete >> mapMsg)
                | AsyncTask.Complete res -> 
                    mapRes ({ s with isPending = false }, res)
            
            // ToDo: think of better name
            let startAsyncTask (factory: Dispatch<'msg> -> Async<'r>) (success: 'r -> 'msg) (error: exn -> 'msg) = 
                let sub dispatch = 
                    let task = factory dispatch
                    async {
                        let! r = task |> Async.Catch
                        match r with
                        | Choice1Of2 r -> r |> success
                        | Choice2Of2 err -> err |> error
                        |> dispatch
                    } 
                    |> Async.Start
                Cmd.ofSub sub

            let processProgressTask 
                (s: State) 
                (factory: 'arg -> Dispatch<'msg> -> Async<'r>)
                (mapMsg: Result<'r, exn> -> 'msg)
                (mapRes: State * Result<'r,exn> -> State * Cmd<'msg>) 
                = function
                | Progress.Msg.Start arg -> 
                    let startCmd = startAsyncTask (factory arg) (Result.Ok >> mapMsg) (Result.Error >> mapMsg)
                    s, startCmd
                | Progress.Msg.Progress _   -> s, Cmd.none
                | Progress.Msg.Complete res -> 
                    mapRes (s, res)           

        let getMakingUsbProgressTpl: MakingUsbStages -> Progress.State = function
            | Formatting -> 
                {
                    captionTpl = "1/2 Formatting USB"
                    style = Progress.Marquee
                    current = 0.0
                    total = 0.0
                }
            | Extracting -> 
                {
                    captionTpl = "2/2 Copy files to USB: {0:0} of {1:0} ({2:0}%)"
                    style = Progress.Continuous
                    current = 0.0
                    total = 0.0
                }

        let startMakingUsb s release = 
            let stage = Formatting
            { s with 
                installationProgress = MakingUsb stage
                progress = stage |> getMakingUsbProgressTpl |> Some
            }, Progress.start MakeUsbStick release

        let nextBtn (srv: IService) (state: Main.State) (dialogRes: DlgRes option) = 

            let userTriesToGetNextState (s: Main.State) (dialogRes: DlgRes option) =
                let stay = s, Cmd.none, None
                let goTo (screen: Screen) = s, Cmd.none, Some screen
                
                match s.CurrentScreen() with
                | Screen.S0Welcome         -> 
                    let ns, cmd = 
                        match s.installationProgress with
                        | WaitForStart -> 
                            {s with installationProgress = MetadataDownloading },
                            Progress.start DownloadMetadata ()
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
                    | true  -> ns, (AsyncHlp.startAsync GenerateKey), None
                    | false -> ns, Cmd.none, None
                | Screen.S2a2KeyGenSuccess -> goTo Screen.S4SelectDisk
                | Screen.S2b1SelectJson    -> goTo Screen.S2b2JsonPassword
                | Screen.S2b2JsonPassword  -> s, (AsyncHlp.startAsync (ImportKey.Import >> Msg.ImportKey)), None
                | Screen.S3MoneyOut        -> s, (AsyncHlp.startAsync CallSmartContract), None
                | Screen.S4SelectDisk      -> 
                    match dialogRes with
                    | None -> 
                        let box = { 
                            caption = "Caution"
                            text = "All data from selected USB drive will be erased. Continue?" }
                        { s with show = ShowMessageBox box }, Cmd.none, None
                    | Some DlgRes.Ok     -> 
                        match s.installationProgress with
                        | DownloadComplete (Result.Ok release) -> 
                            let (ns, cmd) = startMakingUsb s release
                            ns, cmd, Some Screen.S5Progress
                        | Downloading -> goTo Screen.S5Progress
                        | _ -> failwith "unexpected case"
                    | Some DlgRes.Cancel -> stay
                | Screen.S5Progress
                | Screen.S6Finish          -> 
                    let closeApp = Cmd.ofSub (fun _ -> srv.CloseApp())
                    s, closeApp, None
                | _                        -> failwith "Unknown case"
    
            userTriesToGetNextState state dialogRes
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
                    | Screen.S0Welcome        -> s.isProcessElevated
                    | Screen.S2a1KeyGen       -> s.newKeyState.NextAllowed()
                    | Screen.S2b1SelectJson   -> s.existingKeystore.path.IsSome
                    | Screen.S2b2JsonPassword -> not <| String.IsNullOrEmpty(s.existingKeystore.password)
                    | Screen.S4SelectDisk     -> s.usbDrives.selectedDrive.IsSome && match s.installationProgress with 
                                                                                     |DownloadComplete(_) -> true
                                                                                     | _ -> false
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
                            | ShowMessagePage _ 
                            | ShowMessageBox _ -> false
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
                | _ -> failwith "Illegal case"
            | NextBtn -> nextBtn srv s None
            | DialogResult result -> 
                let ns, cmd = nextBtn srv s (Some result)
                { ns with show = ShowStep }, cmd
            | ChangeProgressState p -> { s with progress = p }, Cmd.none
            | DownloadMetadata task ->
                let completion (s, res) =
                    match res with 
                    | Result.Ok cm -> {s with channelMetadata = Some cm }, Progress.Msg.Start cm.SonmOS.Latest |> DownloadRelease |> Cmd.ofMsg
                    | Error exn -> 
                        ExnHlp.showExn 
                            false 
                            "Download error" 
                            s 
                            exn
                        , Cmd.none
                let factory arg dispatch =
                    let progress ps = 
                        ps |> Some |> ChangeProgressState |> dispatch
                    srv.DownloadMetadata progress
                task
                |> AsyncHlp.processProgressTask s factory 
                    (Progress.Msg.Complete >> DownloadMetadata)
                    (AsyncHlp.mapStateOk (fun s res -> { s with installationProgress = MetadataDownloadCompelete  })
                        >> completion)
            | DownloadRelease task  ->
                let factory arg dispatch =
                    let progress ps = 
                        ps |> Some |> ChangeProgressState |> dispatch
                    srv.DownloadRelease arg progress
                let completion (s, res) =
                    match res with 
                    | Result.Ok r -> {s with downloadedRelease = Some r }, Cmd.none
                    | Error exn -> 
                        ExnHlp.showExn 
                            false 
                            "Download error" 
                            s 
                            exn
                        , Cmd.none
                task
                |> AsyncHlp.processProgressTask s factory
                    (Progress.Msg.Complete >> DownloadRelease)
                    (AsyncHlp.mapStateOk (fun s res -> { s with installationProgress = Result.Ok res |> DownloadComplete })
                        >> completion)
            | HasWallet hasWallet -> { s with hasWallet = hasWallet }, Cmd.none
            | NewKeyMsg action -> 
                let res = NewKeyPage.update s.newKeyState action
                let ns = { s with newKeyState = res }
                ns, Cmd.none
            | GenerateKey task -> 
                task 
                |> AsyncHlp.processTask s
                    (fun () -> srv.GenerateKeyStore s.newKeyState.keyPath s.newKeyState.password) // ToDo: make keyPath 'option
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
                        (fun () -> srv.ImportKeyStore s.existingKeystore.path.Value s.existingKeystore.password)
                        (ImportKey.Import >> Msg.ImportKey)
                        (AsyncHlp.mapStateOk (fun s addr -> { s with etherAddress = Some addr })
                        >> AsyncHlp.toScreen Screen.S4SelectDisk "Key Store Import Error:"
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
            | UsbDrives act -> 
                let usbDrives = 
                    match act with
                    | Change -> 
                        let list = srv.GetUsbDrives ()
                        let currentSelected = s.usbDrives.selectedDrive
                        let selected = 
                            if currentSelected.IsNone || not <| List.contains currentSelected.Value list then None 
                            else currentSelected
                        let wipe = match selected with | Some d -> d.containsSonm && s.usbDrives.wipePreviousData | None -> false
                        { s.usbDrives with list = list; selectedDrive = selected; wipePreviousData = wipe}
                    | SelectDrive(Some(index)) ->
                        let list = s.usbDrives.list
                        let selected = List.tryFind (fun d -> d.index = index) list
                        let wipe = match selected with | Some d -> d.containsSonm && s.usbDrives.wipePreviousData | None -> false
                        { s.usbDrives with selectedDrive = selected; wipePreviousData = wipe }
                    | SelectDrive None -> { s.usbDrives with selectedDrive = None }
                    | WipePreviousData flag -> { s.usbDrives with wipePreviousData = flag }
                { s with usbDrives = usbDrives }, Cmd.none 
            | MakeUsbStick task -> 
                let factory release dispatch = 
                    let driveIndex = s.usbDrives.selectedDrive.Value.index
                    let progress state = 
                        state |> Some |> ChangeProgressState |> dispatch
                    let wipe = s.usbDrives.wipePreviousData
                    srv.MakeUsbStick driveIndex wipe release progress

                task
                |> AsyncHlp.processProgressTask s factory 
                    (Progress.Msg.Complete >> MakeUsbStick)
                    (AsyncHlp.mapStateOk (fun s _ -> { s with installationProgress = Finish })
                     >> AsyncHlp.toScreen Screen.S6Finish "Error" 
                     >> fun (s, _) -> s, Cmd.none)
        
    open Impl

    let update (service: IService) (state: Main.State) =
        computeNewState service state
        >> withButtons

    let init (srv: IService) () =
        ({
            currentStep = Screen.S0Welcome, 0
            stepsHistory = []
            show = ShowStep
            backButton = button.btnBack
            nextButton = button.btnNext
            progress = None
            installationProgress = InstallationProgress.WaitForStart
            channelMetadata = None
            downloadedRelease = None
            isProcessElevated = srv.IsProcessElevated ()
            hasWallet = false
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
            usbDrives = {
                list = srv.GetUsbDrives()
                selectedDrive = None
                wipePreviousData = false
            }
        }, Cmd.none)
        |> withButtons