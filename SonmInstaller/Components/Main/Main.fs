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
    | Writing
    | Finish

type UiState = {
    CurrentStep: Step
    StepsHistory: Step list
    InstallationStatus: InstallationStatus
    HasWallet: bool
    BackButton: Button.ButtonState
    NextButton: Button.ButtonState
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

type UiStateAction =
    | Back
    | Next
    | HasWallet of bool
    | NewKeyAction of NewKeyPage.Action
    | OpenKeyDir
    | OpenKeyFile
    | ChangeInstallationStatus of InstallationStatus

[<AutoOpen>]
module Init = 
    open SonmInstaller.Components.Button
    let init () =
        {
            CurrentStep = Screen.S0Welcome, 0
            StepsHistory = []
            InstallationStatus = InstallationStatus.WaitForStart
            HasWallet = false
            BackButton = btnHidden
            NextButton = btnBegin
            NewKeyState = NewKeyPage.Init.init ()
        }, []