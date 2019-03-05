module SonmInstaller.Tools

open System
open System.Reflection
open System.IO
open System.Threading.Tasks
open System.Security.Cryptography
open System
open System.Diagnostics

type ReportStream(s: Stream, report) =
    inherit Stream()
    let s = s
    let report = report
    interface IDisposable with
        member this.Dispose() = s.Dispose()

    override this.Flush() = s.Flush() 
    override this.Close() = s.Close() 
    override this.Read(buffer, offset, count) = report count
                                                s.Read(buffer, offset, count)
    override this.Write(buffer, offset, count) = report count
                                                 s.Write(buffer, offset, count)
    override this.Seek(offset, origin) = s.Seek(offset, origin)
    override this.SetLength(value) = s.SetLength(value)
    override this.CanRead with get() = s.CanRead
    override this.CanSeek with get() = s.CanSeek
    override this.CanWrite with get() = s.CanWrite
    override this.Length with get() = s.Length
    override this.Position with get() = s.Position and set(v) = s.Position <- v


type ListItem (value: int, text: string) =
    member val Value = value with get, set
    member val Text = text with get, set
    override x.ToString () = x.Text

let getExePath () = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location)

let homePath = 
    if Environment.OSVersion.Platform = PlatformID.Unix || Environment.OSVersion.Platform = PlatformID.MacOSX then
        Environment.GetEnvironmentVariable("HOME")
    else
        Environment.ExpandEnvironmentVariables("%HOMEDRIVE%%HOMEPATH%")

let appPath = Path.Combine(
                    Environment.GetFolderPath Environment.SpecialFolder.LocalApplicationData,
                    "SONM")

let keyPath = Path.Combine(
                    Environment.GetFolderPath Environment.SpecialFolder.MyDocuments,
                    "SONM")

let ensurePathExists path () =
    if Directory.Exists(path) |> not then Directory.CreateDirectory(path) |> ignore

let ensureAppPathExists = ensurePathExists appPath
        
let ensureKeyPathExists = ensurePathExists keyPath

let defaultNewKeyPath = Path.Combine(keyPath, "key.json")

let saveTextFile path content =
    File.WriteAllText(path, content)

let hashFile progress (path: string) =
    let bufferSize = 65536
    let hashFunction = new SHA256Cng()
    let rec hashBlock currentBlock count (stream: Stream) = async {
        let buffer = Array.zeroCreate<byte> bufferSize
        let! readCount = stream.AsyncRead buffer
        if readCount = 0 then
            hashFunction.TransformFinalBlock(currentBlock, 0, count) |> ignore
        else 
            hashFunction.TransformBlock(currentBlock, 0, count, currentBlock, 0) |> ignore
            return! hashBlock buffer readCount stream
    }
    async {
        use stream: Stream = match progress with
                             | Some f -> let file = File.OpenRead path
                                         new ReportStream(file, f) :> Stream
                             | None -> File.OpenRead path :> Stream
        let buffer = Array.zeroCreate<byte> bufferSize
        let! readCount = stream.AsyncRead buffer
        do! hashBlock buffer readCount stream
        let sum = hashFunction.Hash |> Array.map (fun b -> b.ToString("x2")) |> String.concat String.Empty
        return sum
    }
    

module Exn = 
    let getMessagesStack (e: exn) = 
        let rec loop (e: exn) (res: string) = 
            if e <> null && not <| String.IsNullOrEmpty e.Message then
                let msg = e.Message.Trim().TrimEnd('.')
                if String.IsNullOrEmpty res then msg else sprintf "%s. %s" res msg
                |> loop e.InnerException
            else res
        loop e ""

