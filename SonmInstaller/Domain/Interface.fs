namespace SonmInstaller.Domain

open SonmInstaller
open SonmInstaller.ReleaseMetadata
open SonmInstaller.Components
open SonmInstaller.Components.Main;

type Service = 
    {
        // NewKeyPage.IService
        getUtcFilePath: unit -> string
        
        // Main.IService
        isProcessElevated: unit -> bool
        getUsbDrives: unit -> UsbDrive list
        downloadMetadata: (Progress.State -> unit) -> Async<ChannelMetadata>
        downloadRelease: Release -> (Progress.State -> unit) -> Async<Release>
        generateKeyStore: string -> string -> Async<string> // path -> password -> address
        importKeyStore  : string -> string -> Async<string>
        openKeyFolder: (*path*) string -> unit
        openKeyFile:   (*path*) string -> unit
        callSmartContract: string -> float -> Async<unit>
        makeUsbStick: 
            int ->                          // disk index 
            bool ->
            Release ->
            (Progress.State -> unit) ->         // progress callback: etries extarcted -> total entries.
            Async<unit>
        closeApp: unit -> unit
    } 
    interface NewKeyPage.IService with
        member x.GetUtcFilePath () = x.getUtcFilePath ()
        
    interface Main.IService with
        member x.IsProcessElevated () = x.isProcessElevated ()
        member x.GetUsbDrives () = x.getUsbDrives ()
        member x.DownloadMetadata progress = x.downloadMetadata progress
        member x.DownloadRelease arg progress = x.downloadRelease arg progress
        member x.GenerateKeyStore path password = x.generateKeyStore path password
        member x.ImportKeyStore   path password = x.importKeyStore path password
        member x.OpenKeyFolder path = x.openKeyFolder path
        member x.OpenKeyFile path = x.openKeyFile path
        member x.CallSmartContract withdrawTo minPayout = x.callSmartContract withdrawTo minPayout
        member x.MakeUsbStick drive erase release progress = x.makeUsbStick drive erase release progress
        member x.CloseApp () = x.closeApp ()
