namespace SonmInstaller.Components.NewKeyPage

open SonmInstaller

[<AutoOpen>]
module View =
    let view (form: WizardForm) (state: State) =
        form.lblNewKeyErrorMessage.Text <- 
            match state.ErrorMessage with
            | Some err -> err
            | None -> ""
        form.lblNewKeyErrorMessage.Visible <- Option.isSome state.ErrorMessage
