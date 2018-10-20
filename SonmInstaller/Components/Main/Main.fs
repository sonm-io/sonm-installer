namespace SonmInstaller.Components.Main

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

type InstallationProgress =
    | WaitForStart
    | Downloading
    | DownloadComplete of Result<unit, exn>

type MessagePage = {
    Header: string
    Message: string
    TryAgainVisible: bool
}

type ExistingKeystore = {
    path: string option
    password: string
}

type Show = ShowStep | ShowMessagePage of MessagePage

type State = {
    CurrentStep: Step
    StepsHistory: Step list
    Show: Show
    InstallationProgress: InstallationProgress
    BackButton: Button.State
    NextButton: Button.State
    IsPending: bool
    // progress:
    HasWallet: bool
    NewKeyState: NewKeyPage.State
    ExistingKeystore: ExistingKeystore
    EtherAddress: string option
    SelectedDrive: ListItem option
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


namespace SonmInstaller.Components.Main.Msg

open SonmInstaller.Components
open SonmInstaller.Components.Main
open SonmInstaller.Tools

type Download = 
    | Start
    | Progress of int64 * int64 // bytes downloaded * total
    | Complete of Result<unit, exn>

type AsyncTask<'r> = 
    | Start 
    | Complete of Result<'r, exn>

type ImportKey = 
    | ChoosePath of string
    | ChangePassword of string
    | Import of AsyncTask<string>

type Msg =
    | BackBtn
    | NextBtn
    | GoTo of Step
    | Download of Download
    | HasWallet of bool
    | NewKeyMsg of NewKeyPage.Msg
    | GenerateKey of AsyncTask<string>
    | OpenKeyDir
    | OpenKeyFile
    | ImportKey of ImportKey
    | CallSmartContract of AsyncTask<unit>
    | SelectDrive of ListItem
    | MakeUsbStick of AsyncTask<unit>
