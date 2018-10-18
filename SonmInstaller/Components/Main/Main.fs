namespace SonmInstaller.Components.Main

open SonmInstaller.Components

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
    | Writing
    | Finish

type State = {
    CurrentStep: Step
    StepsHistory: Step list
    InstallationProgress: InstallationProgress
    HasWallet: bool
    BackButton: Button.State
    NextButton: Button.State
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
    member this.IsPending () = this.NewKeyState.IsPending

type Msg =
    | Back
    | Next
    | StartDownload
    | DownloadProgress of int64 * int64 // bytes downloaded * total
    | DownloadComplete of Result<unit, exn>
    | HasWallet of bool
    | NewKeyAction of NewKeyPage.Msg
    | OpenKeyDir
    | OpenKeyFile

