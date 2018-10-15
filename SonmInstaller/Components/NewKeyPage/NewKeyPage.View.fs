[<AutoOpen>]
module SonmInstaller.Components.NewKeyPageView

open SonmInstaller
open SonmInstaller.Components

module NewKeyPage =
    let view (form: WizardForm) (state: NewKeyPage.State) =
        form.lblNewKeyErrorMessage.Text <- 
            match state.ErrorMessage with
            | Some err -> err
            | None -> ""
        form.lblNewKeyErrorMessage.Visible <- Option.isSome state.ErrorMessage
