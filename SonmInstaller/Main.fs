module SonmInstaller.Main

open System
open System.Windows.Forms

open Elmish
open Elmish.Winforms
open SonmInstaller.EventHandlers
open SonmInstaller.Program


[<EntryPoint; STAThread>]
let main argv =
    Application.EnableVisualStyles()
    Application.SetCompatibleTextRenderingDefault(false)
    use form = new WizardForm()
    Program.runWith () (getProgram form) 
    Application.Run(form) 
    0 // return an integer exit code
