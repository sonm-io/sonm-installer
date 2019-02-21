module SonmInstaller.Domain.Download

open Newtonsoft.Json
open System
open System.IO
open System.Net
open System.Diagnostics
open Newtonsoft.Json.Serialization
open SonmInstaller.Domain
open SonmInstaller.ReleaseMetadata
open SonmInstaller.Components



let parseMetadata(bytes: byte[]) =
    JsonConvert.DeserializeObject<ChannelMetadata>(System.Text.Encoding.ASCII.GetString bytes)

let downloadMetadata (progress: Progress.State -> unit) = async {
    return {Channel="internal"; SonmOS=None}
    //return parseMetadata [|12uy; 22uy|]
}    

let startDownload (url: string) (destinationPath: string) (progressCb: int64 -> int64 -> unit) (completeCb: Result<unit, exn> -> unit) =
    let uri = new Uri(url)
    let client = new WebClient();
    client.DownloadProgressChanged.Add <| fun e -> 
        progressCb e.BytesReceived e.TotalBytesToReceive
    client.DownloadDataCompleted.Add <| fun e -> 
        let r = if e.Error = null then Ok () else Error e.Error
        Trace.WriteLine(String.Format("gotMetadata: {0}", parseMetadata e.Result))
        completeCb r
    client.DownloadDataAsync(uri);

