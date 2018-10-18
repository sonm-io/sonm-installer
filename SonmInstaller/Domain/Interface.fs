namespace SonmInstaller.Domain

open SonmInstaller
open SonmInstaller.Components

type Service = 
    {
        startDownload:  
            (int64 -> int64 -> unit) ->     // progressCb: bytesDownloaded -> total
            (Result<unit, exn> -> unit) ->  // completeCb
            unit
        generateKeyStore: string -> Async<unit> // password -> path
        openKeyFolder: (*path*) string -> unit
        openKeyFile:   (*path*) string -> unit
    } 
    interface NewKeyPage.IService with
        member x.DefaultNewKeyPath = Tools.defaultNewKeyPath
        member x.GenerateKeyStore password = x.generateKeyStore password
    interface Main.IService with
        member x.StartDownload progressCb completeCb = x.startDownload progressCb completeCb
        member x.OpenKeyFolder path = x.openKeyFolder path
        member x.OpenKeyFile path = x.openKeyFile path
