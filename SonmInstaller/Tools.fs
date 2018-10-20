module SonmInstaller.Tools

open System
open System.IO

type ListItem (value: int, text: string) =
    member val Value = value with get, set
    member val Text = text with get, set
    override x.ToString () = x.Text

let homePath = 
    if Environment.OSVersion.Platform = PlatformID.Unix || Environment.OSVersion.Platform = PlatformID.MacOSX then
        Environment.GetEnvironmentVariable("HOME")
    else
        Environment.ExpandEnvironmentVariables("%HOMEDRIVE%%HOMEPATH%")

let appPath = Path.Combine(homePath, "SONM")

let ensureAppPathExists () =
    if Directory.Exists(appPath) |> not then Directory.CreateDirectory(appPath) |> ignore
        
let defaultNewKeyPath = Path.Combine(appPath, "key.json")

let saveTextFile path content =
    File.WriteAllText(path, content)

let getDrives () = // ToDo
    [
        1, "C:"
        2, "D:"
        3, "E:"
    ]
    |> List.map (fun (value, text) -> new ListItem (value, text))

module Exn = 
    let getMessagesStack (e: exn) = 
        let rec loop (res: string) (e: exn) = 
            if e <> null && not <| String.IsNullOrEmpty e.Message then
                let msg = e.Message.Trim().TrimEnd('.')
                loop (sprintf "%s. %s" res msg) e.InnerException
            else res
        loop "" e
