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

type InstallationStatus =
    | WaitForStart
    | Downloading
    | DownloadComplete
    | Writing
    | Finish

type State = {
    CurrentStep: Step
    StepsHistory: Step list
    InstallationStatus: InstallationStatus
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

type Msg =
    | Back
    | Next
    | DownloadProgress of int64 * int64 // bytes downloaded * total
    | DownloadComplete
    | HasWallet of bool
    | NewKeyAction of NewKeyPage.Msg
    | OpenKeyDir
    | OpenKeyFile

namespace SonmInstaller.Components
open SonmInstaller.Components.Main

module main = 
    let init () =
        {
            CurrentStep = Screen.S0Welcome, 0
            StepsHistory = []
            InstallationStatus = InstallationStatus.WaitForStart
            HasWallet = false
            BackButton = button.btnHidden
            NextButton = button.btnBegin
            NewKeyState = newKeyPage.init
        }, []