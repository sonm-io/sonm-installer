namespace SonmInstaller.Components.Button

type ButtonState = {
    Text: string;
    Enabled: bool;
    Visible: bool;
}

[<AutoOpen>]
module Init =
    let private btnEmpty = { Text = ""; Enabled = true; Visible = true }
    let private createBtn text = { btnEmpty with Text = text }
    let btnHidden = { Text = ""; Enabled = true; Visible = false }
    let btnBegin  = createBtn "Begin"
    let btnBack   = createBtn "Back"
    let btnNext   = createBtn "Next"
    let btnClose  = createBtn "Close"
