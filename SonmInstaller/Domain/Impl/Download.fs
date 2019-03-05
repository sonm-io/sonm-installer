module SonmInstaller.Domain.Download

open Newtonsoft.Json
open System
open System.IO
open System.Net
open System.Diagnostics
open System.Security.Cryptography
open SonmInstaller.ReleaseMetadata
open SonmInstaller.Components
open FSharp.Data
open SonmInstaller.Atom

module private Internal =
    open SonmInstaller.Tools

    let getLengthFor response = 
        response.Headers
            |> Map.tryFind "Content-Length" 
            |> Option.bind (fun s -> try s |> int |> Some with | _ -> None) 

    let stateComplete = 
        {
            Progress.State.captionTpl="Download complete"
            Progress.State.style=Progress.ProgressStyle.Continuous
            Progress.State.current=100.0
            Progress.State.total=100.0
        }
       
    let sharedState caption cont total progress =
        let counter = atom (fun () -> 0)
        let state = {
            Progress.State.captionTpl=if cont then caption + " {0:0.0} of {1:0.0} Mb ({2:0}%)" else caption
            Progress.State.style=if cont then Progress.ProgressStyle.Continuous else Progress.ProgressStyle.Marquee
            Progress.State.current=0.0
            Progress.State.total=(float total) / 1024.0 / 1024.0
        }
        (fun current -> 
            swap counter (fun f -> (fun result () -> result + current) <| f()) |> ignore
            {state with current=float ((!counter)()) / 1024.0 / 1024.0} |> progress)

    let parseMetadata streamReader =
        let ser = JsonSerializer ()
        use reader = new JsonTextReader(streamReader)
        let cm = ser.Deserialize<ChannelMetadata>(reader)
        if obj.ReferenceEquals(null, cm) then
            failwith "Error parsing release metadata"
        cm

    let archiveLocation path (archive: Archive) = 
        Path.Combine(path, archive.Name)

    let archiveExistsAndCheckSumMatch progress (path: string) sum = async {
        match File.Exists path with
        | true -> let! localSum = hashFile progress path
                  return sum = localSum
        | false -> return false
    }
    
    let downloadArchive progress archivePath (archive: Archive) = async {
        let! response = Http.AsyncRequestStream(archive.URL)
        let fileStream = File.OpenWrite archivePath
        use reportStream = new ReportStream(fileStream, progress)
        let! _ = response.ResponseStream.CopyToAsync reportStream |> Async.AwaitTask        
        return ()       
    }
    let downloadArchiveIfNeeded progress path (archive: Archive) = async {
        let archivePath = archiveLocation path archive
        match! archiveExistsAndCheckSumMatch (Some progress) archivePath archive.Sha256 with
        | false -> progress (if File.Exists archivePath then -(int (FileInfo archivePath).Length) else 0)
                   let! a = downloadArchive progress archivePath archive
                   return! archiveExistsAndCheckSumMatch None archivePath archive.Sha256
        | res -> return res
    }

    let ensureDirExists path =
        if Directory.Exists path |> not then Directory.CreateDirectory path |> ignore
    
    let downloadComponent progress version path  comp = 
        Trace.WriteLine(String.Format("Downloading component {0}", comp.Name))
        let releasePath = Path.Combine(path, versionToString version)
        ensureDirExists releasePath
        comp.Archives |> List.map (downloadArchiveIfNeeded progress releasePath)

open Internal

let downloadMetadata (url: string) (progress: Progress.State -> unit) = async {
    let state = sharedState "Downloading release information" false 0 progress
    state 0
    let! response = Http.AsyncRequestStream(url)
    use reader = new StreamReader(response.ResponseStream)
    let result = parseMetadata reader
    stateComplete |> progress
    return result
}    

let downloadRelease (path: string) (r: Release) (progress: Progress.State -> unit) = async {
    let releaseSize = r.Components |> List.fold (fun acc  c -> acc + c.Size) 0 
    let statec = sharedState "Downloading release files" true releaseSize progress
    statec 0
    let! res = r.Components 
               |> List.map (downloadComponent statec r.Version path)
               |> List.concat
               |> Async.Parallel
    if Array.fold (fun (i:bool) (b:bool) -> i && b) true res |> not then
        failwithf "There are error occured while dowloading release version %s from channel %s" (versionToString r.Version) r.Channel
    stateComplete |> progress
    return r
}
