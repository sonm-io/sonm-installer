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

    let hashFile (path: string) =
        let bufferSize = 4096
        let hashFunction = new SHA256Cng()
        let rec hashBlock currentBlock count (stream: FileStream) = async {
            let buffer = Array.zeroCreate<byte> bufferSize
            let! readCount = stream.AsyncRead buffer
            if readCount = 0 then
                hashFunction.TransformFinalBlock(currentBlock, 0, count) |> ignore
            else 
                hashFunction.TransformBlock(currentBlock, 0, count, currentBlock, 0) |> ignore
                return! hashBlock buffer readCount stream
        }
        async {
            Trace.WriteLine(String.Format("Checking checksum for {0} ", path))
            use stream = File.OpenRead path
            let buffer = Array.zeroCreate<byte> bufferSize
            let! readCount = stream.AsyncRead buffer
            do! hashBlock buffer readCount stream
            let sum = hashFunction.Hash |> Array.map (fun b -> b.ToString("x2")) |> String.concat String.Empty
            Trace.WriteLine(String.Format("Checksum for {0} is {1}", path, sum))
            return sum
        }
    
    let archiveLocation path (archive: Archive) = 
        Path.Combine(path, archive.Name)

    let archiveExistsAndCheckSumMatch (path: string) sum = async {
        Trace.WriteLine(String.Format("Checking archive {0} exists", path))
        match File.Exists path with
        | true -> let! localSum = hashFile path
                  return sum = localSum
        | false -> return false
    }
    
    let downloadArchive archivePath (archive: Archive) = async {
        Trace.WriteLine(String.Format("Actual downloading archive {0}", archive.Name))
        let! response = Http.AsyncRequestStream(archive.URL)
        use stream = File.OpenWrite archivePath
        let! _ = response.ResponseStream.CopyToAsync stream |> Async.AwaitTask        
        return ()       
    }
    let downloadArchiveIfNeeded path (archive: Archive) = async {
        Trace.WriteLine(String.Format("Downloading archive {0}", archive.Name))
        let archivePath = archiveLocation path archive
        match! archiveExistsAndCheckSumMatch archivePath archive.Sha256 with
        | false -> let! a = downloadArchive archivePath archive
                   return! archiveExistsAndCheckSumMatch archivePath archive.Sha256
        | res -> return res
    }

    let ensureDirExists path =
        if Directory.Exists path |> not then Directory.CreateDirectory path |> ignore
    
    let downloadComponent version path comp = 
        Trace.WriteLine(String.Format("Downloading component {0}", comp.Name))
        let releasePath = Path.Combine(path, versionToString version)
        ensureDirExists releasePath
        comp.Archives |> List.map (downloadArchiveIfNeeded releasePath)

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
    Trace.WriteLine("Starting download release")
    let! res = r.Components 
               |> List.map (downloadComponent r.Version path)
               |> List.concat
               |> Async.Parallel
    if Array.fold (fun (i:bool) (b:bool) -> i && b) true res |> not then
        failwithf "There are error occured while dowloading release version %s from channel %s" (versionToString r.Version) r.Channel

    return r
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

