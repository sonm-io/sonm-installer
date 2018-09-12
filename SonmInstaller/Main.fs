module SonmInstaller.Main

open System
open System.Windows.Forms

[<EntryPoint; STAThread>]
let main argv =
    Application.EnableVisualStyles()
    Application.SetCompatibleTextRenderingDefault(false)
    use form = new WizardForm()
    Application.Run(form)    
    0 // return an integer exit code
