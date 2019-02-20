module SonmInstaller.Domain.Download

open Newtonsoft.Json
open System
open System.IO
open System.Net

let startDownload (url: string) (destinationPath: string) (progressCb: int64 -> int64 -> unit) (completeCb: Result<unit, exn> -> unit) =
    let uri = new Uri(url)
    let client = new WebClient();
    client.DownloadProgressChanged.Add <| fun e -> 
        progressCb e.BytesReceived e.TotalBytesToReceive
    client.DownloadDataCompleted.Add <| fun e -> 
        let r = if e.Error = null then Ok () else Error e.Error
        completeCb r
    client.DownloadDataAsync(uri);

let parseMetadata(bytes: byte[]) =
    JsonConvert.DeserializeObject(System.Text.Encoding.ASCII.GetString bytes)