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

module private Internal =

    let getLengthFor response = 
        response.Headers
            |> Map.tryFind "Content-Length" 
            |> Option.bind (fun s -> try s |> int |> Some with | _ -> None) 

    let state caption total progress = 
        {
            Progress.State.captionTpl=if total > 0 then caption + " {0:0.0} of {1:0.0} ({2:0}%)" else caption
            Progress.State.style=if total > 0 then Progress.ProgressStyle.Continuous else Progress.ProgressStyle.Marquee
            Progress.State.current=float progress
            Progress.State.total=float total
        }

    let parseMetadata streamReader =
        let ser = JsonSerializer ()
        use reader = new JsonTextReader(streamReader)
        let cm = ser.Deserialize<ChannelMetadata>(reader)
        if obj.ReferenceEquals(null, cm) then
            failwith "Error parsing release metadata"
        cm

    let calcCheckSum path = async {
        let digester = SHA256.Create()
        let file = File.OpenRead path
        let buffer = Array.empty<byte>
        let hash = digester.ComputeHash buffer
        return true
    }
    
    let archiveLocation path (archive: Archive) = 
        Path.Combine(path, archive.Name)

    let archiveExistsAndCheckSumMatch path sum = async {
        match File.Exists path with
        | true -> return! calcCheckSum path
        | false -> return false
    }
    
    let downloadArchive path (archive: Archive) = async {
        let archivePath = archiveLocation path archive
        match! archiveExistsAndCheckSumMatch archivePath archive.Sha256 with
        | true -> ()
        | false -> ()
        let! response = Http.AsyncRequestStream(archive.URL)
        return ()       
    }
    
    let downloadComponent path comp = 
        comp.Archives |> List.map (downloadArchive path)

open Internal

let downloadMetadata (url: string) (progress: Progress.State -> unit) = async {
    let statec = state "Downloading release information"
    statec 0 0 |> progress
    let! response = Http.AsyncRequestStream(url)
    let length = getLengthFor response |> Option.defaultValue 0
    let statel = statec length
    statel 0 |> progress
    use reader = new StreamReader(response.ResponseStream)
    let result = parseMetadata reader
    statel length |> progress
    return result
}    

let downloadRelease (path: string) (r: Release) (progress: Progress.State -> unit) = async {
    let statec = state "Downloading release files"
    statec 0 0 |> progress

    let! res = r.Components |> List.map (downloadComponent path) |> List.concat |> Async.Parallel
    return {
        Channel = "internal"
        SonmOS = {
            Latest = r
            Releases = List.Empty
        }
    }
}    

let startDownload (url: string) (destinationPath: string) (progressCb: int64 -> int64 -> unit) (completeCb: Result<unit, exn> -> unit) =
    let uri = new Uri(url)
    let client = new WebClient();
    client.DownloadProgressChanged.Add <| fun e -> 
        progressCb e.BytesReceived e.TotalBytesToReceive
    client.DownloadDataCompleted.Add <| fun e -> 
        let r = if e.Error = null then Ok () else Error e.Error
        use reader = new StringReader(System.Text.Encoding.ASCII.GetString e.Result)
        Trace.WriteLine(String.Format("gotMetadata: {0}", parseMetadata reader))
        completeCb r
    client.DownloadDataAsync(uri);

