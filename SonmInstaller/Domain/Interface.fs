namespace SonmInstaller.Domain

open SonmInstaller
open SonmInstaller.Components

type Service = 
    {
        // Main
        startDownload:  
            (int64 -> int64 -> unit) ->     // progressCb: bytesDownloaded -> total
            (Result<unit, exn> -> unit) ->  // completeCb
            unit
        generateKeyStore: string -> Async<string> // password -> path
        importKeyStore : string -> Async<string>
        openKeyFolder: (*path*) string -> unit
        openKeyFile:   (*path*) string -> unit
        callSmartContract: string -> float -> Async<unit>
        makeUsbStick: int -> Async<unit>
        closeApp: unit -> unit
    } 
    interface NewKeyPage.IService with
        member __.DefaultNewKeyPath = Tools.defaultNewKeyPath
        
    interface Main.IService with
        member x.StartDownload progressCb completeCb = x.startDownload progressCb completeCb
        member x.GenerateKeyStore password = x.generateKeyStore password
        member x.ImportKeyStore password = x.importKeyStore password
        member x.OpenKeyFolder path = x.openKeyFolder path
        member x.OpenKeyFile path = x.openKeyFile path
        member x.CallSmartContract withdrawTo minPayout = x.callSmartContract withdrawTo minPayout
        member x.MakeUsbStick drive = x.makeUsbStick drive
        member x.CloseApp () = x.closeApp ()
