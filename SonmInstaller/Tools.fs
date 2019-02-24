module SonmInstaller.Tools

open System
open System.Reflection
open System.IO
open System.Threading.Tasks

type ListItem (value: int, text: string) =
    member val Value = value with get, set
    member val Text = text with get, set
    override x.ToString () = x.Text

let getExePath () = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location)

let homePath = 
    if Environment.OSVersion.Platform = PlatformID.Unix || Environment.OSVersion.Platform = PlatformID.MacOSX then
        Environment.GetEnvironmentVariable("HOME")
    else
        Environment.ExpandEnvironmentVariables("%HOMEDRIVE%%HOMEPATH%")

let appPath = Path.Combine(
                    Environment.GetFolderPath Environment.SpecialFolder.LocalApplicationData,
                    "SONM")

let ensureAppPathExists () =
    if Directory.Exists(appPath) |> not then Directory.CreateDirectory(appPath) |> ignore
        
let defaultNewKeyPath = Path.Combine(appPath, "key.json")

let saveTextFile path content =
    File.WriteAllText(path, content)

module Exn = 
    let getMessagesStack (e: exn) = 
        let rec loop (e: exn) (res: string) = 
            if e <> null && not <| String.IsNullOrEmpty e.Message then
                let msg = e.Message.Trim().TrimEnd('.')
                if String.IsNullOrEmpty res then msg else sprintf "%s. %s" res msg
                |> loop e.InnerException
            else res
        loop e ""

