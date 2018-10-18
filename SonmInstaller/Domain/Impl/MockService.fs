module SonmInstaller.Domain.Mock

open System
open System.Threading

module private Impl =

    let consoleWrite header message = Console.WriteLine ("{0}: {1}", header, message)

    let startDownload (progressCb: int64 -> int64 -> unit) (completeCb: Result<unit, exn> -> unit) =
        let time = 10L * 1000L // ms
        let deltaTime = 100  // ms
        let total = 1000L
        let batch = total / (time / int64(deltaTime))

        let rec loop progress =
            Thread.Sleep deltaTime
            progressCb progress total
            if progress < total then
                loop (progress + batch)
            else 
                completeCb (Ok ())

        Async.Start <| async {
            loop 0L
        } 
        
        Console.WriteLine("StartDownload")

    let generateKeyStore password = async {
        do! Async.Sleep 4000
    }

let service = {
    startDownload = Impl.startDownload
    generateKeyStore = Impl.generateKeyStore
    openKeyFolder = Impl.consoleWrite "openKeyFolder"
    openKeyFile = Impl.consoleWrite "openKeyFolder"
}

