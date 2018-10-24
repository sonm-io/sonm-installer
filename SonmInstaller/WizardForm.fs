namespace SonmInstaller

open System.Linq;
open System.Windows.Forms

type WizardForm() as this =
    inherit WizardFormDesign()
    
    let mutable headerLabels: Label list = []
    let mutable headerTpls: string list = []
    
    let getHeaders () = 
        let getHeaderFromTab (tab: TabPage) = 
            tab.Controls.Cast<Control>()
            |> Seq.tryFind (fun ctl -> ctl.Tag <> null && ctl.Tag.ToString() = "header")
        
        this.tabs.TabPages.Cast<TabPage>()
        |> Seq.map (fun tab -> tab |> getHeaderFromTab)
        |> Seq.filter (function Some _ -> true | _ -> false)
        |> Seq.map (function Some v -> v | _ -> failwith "imposible")
        |> Seq.map (fun i -> i :?> Label)
        |> List.ofSeq
    
    do
        headerLabels <- getHeaders ()
        headerTpls <- headerLabels |> Seq.map (fun i -> i.Text) |> List.ofSeq

    member x.HeaderLabels with get () = headerLabels
    member x.HeaderTpls with get () = headerTpls
