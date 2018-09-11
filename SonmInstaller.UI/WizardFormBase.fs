namespace SonmInstaller.FormBase

open System
open System.Windows.Forms

open SonmInstaller.ViewModel



type WizardForm() =
    inherit Form1()
    member this.updateView (state: UiState) (changes: UiStateChanges) =
        changes |> List.iter (fun change -> )
