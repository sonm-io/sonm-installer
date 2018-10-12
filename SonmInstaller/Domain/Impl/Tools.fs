module SonmInstaller.Tools

open System
open System.IO

let homePath = 
    if Environment.OSVersion.Platform = PlatformID.Unix || Environment.OSVersion.Platform = PlatformID.MacOSX then
        Environment.GetEnvironmentVariable("HOME")
    else
        Environment.ExpandEnvironmentVariables("%HOMEDRIVE%%HOMEPATH%")

let sonmPath = Path.Combine(homePath, "SONM")

let ensureSonmPathExists () =
    if Directory.Exists(sonmPath) |> not then Directory.CreateDirectory(sonmPath) |> ignore
        
let defaultNewKeyPath = Path.Combine(sonmPath, "key.json")

let generateNewKey password = sprintf "new key content with password: %s" password

let saveTextFile path content =
    File.WriteAllText(path, content)
