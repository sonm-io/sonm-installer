namespace SonmInstaller.Components.Main

open SonmInstaller.ReleaseMetadata
open SonmInstaller.Components
open SonmInstaller.Tools

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

type MakingUsbStages = Formatting | Extracting

type InstallationProgress =
    | WaitForStart
    | MetadataDownloading
    | MetadataDownloadCompelete
    | ReleaseDownloading
    | ReleaseDownloadComplete
    | Downloading
    | DownloadComplete of Result<Release, exn>
    | MakingUsb of MakingUsbStages
    | Finish

type MessagePage = {
    header: string
    message: string
    tryAgainVisible: bool
}

type ExistingKeystore = {
    path: string option
    password: string
}

type Withdraw = {
    address: string
    thresholdPayout: string
}

type MsgBox = {
    caption: string
    text: string
}

type Show = 
    | ShowStep 
    | ShowMessagePage of MessagePage
    | ShowMessageBox of MsgBox

type UsbDrives = {
    list: (int * string) list
    selectedDrive: (int * string) option
}

type State = {
    currentStep: Step
    stepsHistory: Step list
    show: Show
    progress: Progress.State option
    installationProgress: InstallationProgress
    channelMetadata: ChannelMetadata option
    downloadedRelease: Release option
    backButton: Button.State
    nextButton: Button.State
    isPending: bool
    usbDrives: UsbDrives
    isProcessElevated: bool
    hasWallet: bool
    newKeyState: NewKeyPage.State
    existingKeystore: ExistingKeystore
    etherAddress: string option
    withdraw: Withdraw
}
with
    member this.IsAtBeginning () = this.stepsHistory = []
    member this.HistoryTail () =
        match this.stepsHistory with
        | _::tail -> tail
        | _ -> []
    member this.PrevStep () = 
        match this.stepsHistory with
        | head::_ -> head
        | _ -> this.currentStep
    member this.CurrentScreen () = this.currentStep |> fst
    member this.CurrentStepNum () = this.currentStep |> snd


namespace SonmInstaller.Components.Main.Msg

open SonmInstaller.Components
open SonmInstaller.ReleaseMetadata
open SonmInstaller.Components.Main
open SonmInstaller.Tools
open SonmInstaller.Components.Progress

type DlgRes = 
    | Ok
    | Cancel

type AsyncTask<'r> = 
    | Start 
    | Complete of Result<'r, exn>

type ImportKey = 
    | ChoosePath of string
    | ChangePassword of string
    | Import of AsyncTask<string>

type WithdrawMsg = 
    | Address of string
    | Threshold of string

type UsbDrivesMsg = 
    | Change
    | SelectDrive of (int * string) option

type Msg =
    | BackBtn
    | NextBtn
    | DialogResult of DlgRes
    | ChangeProgressState of Progress.State option
    | Download of Progress.Msg<unit, unit>
    | DownloadMetadata of Progress.Msg<unit, ChannelMetadata>
    | DownloadRelease of Progress.Msg<Release, Release>
    | HasWallet of bool
    | NewKeyMsg of NewKeyPage.Msg
    | GenerateKey of AsyncTask<string>
    | OpenKeyDir
    | OpenKeyFile
    | ImportKey of ImportKey
    | Withdraw of WithdrawMsg
    | CallSmartContract of AsyncTask<unit>
    | UsbDrives of UsbDrivesMsg
    | MakeUsbStick of Progress.Msg<Release, unit>