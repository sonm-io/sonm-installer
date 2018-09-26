namespace SonmInstaller

open System
open System.Linq;
open System.Windows.Forms
open SonmInstaller.ViewModel
open FormUpdate
open SonmInstaller.EventHandlers

type WizardForm() as this =
    inherit WizardFormBase()

    let mutable state: UiState = ViewModel.Main.initial

    let dispatch action =
        let next = update state action
        let current = state
        state <- next
        updateView this current next false
        
    do
        setEventHandlers this dispatch
        updateView this state state true

    






    

    