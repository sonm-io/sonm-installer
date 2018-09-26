module SonmInstaller.Tools

open System
open System.IO

let defaultNewKeyPath = Path.Combine(Env.homePath, "key.json")