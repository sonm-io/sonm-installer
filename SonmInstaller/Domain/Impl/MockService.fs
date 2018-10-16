module SonmInstaller.Domain.Mock

open System
open System.Threading

module private Impl =

    let startDownload (progressCb: int64 -> int64 -> unit) (completeCb: unit -> unit) =
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
                completeCb ()

        Async.Start <| async {
            loop 0L
        } 
        
        Console.WriteLine("StartDownload")

    let startDownload2 (progressCb: int64 -> int64 -> unit) (completeCb: unit -> unit) =
        Console.WriteLine("StartDownload")
        progressCb (int64 5) (int64 10)
        completeCb ()


    let consoleWrite header message = Console.WriteLine ("{0}: {1}", header, message)

let service = {
    startDownload = Impl.startDownload
    openKeyFolder = Impl.consoleWrite "openKeyFolder"
    openKeyFile = Impl.consoleWrite "openKeyFolder"
}
