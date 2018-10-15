namespace SonmInstaller.Components.Button

type State = {
    Text: string;
    Enabled: bool;
    Visible: bool;
}

namespace SonmInstaller.Components
open SonmInstaller.Components.Button

module button =
    let private btnEmpty = { Text = ""; Enabled = true; Visible = true }
    let private createBtn text = { btnEmpty with Text = text }
    let btnHidden = { Text = ""; Enabled = true; Visible = false }
    let btnBegin  = createBtn "Begin"
    let btnBack   = createBtn "Back"
    let btnNext   = createBtn "Next"
    let btnClose  = createBtn "Close"
