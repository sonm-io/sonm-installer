module SonmInstaller.Tools

open System
open System.IO

type ListItem (value: int, text: string) =
    member val Value = value with get, set
    member val Text = text with get, set
    override x.ToString () = x.Text

let homePath = 
    if Environment.OSVersion.Platform = PlatformID.Unix || Environment.OSVersion.Platform = PlatformID.MacOSX then
        Environment.GetEnvironmentVariable("HOME")
    else
        Environment.ExpandEnvironmentVariables("%HOMEDRIVE%%HOMEPATH%")

let appPath = Path.Combine(homePath, "SONM")

let ensureAppPathExists () =
    if Directory.Exists(appPath) |> not then Directory.CreateDirectory(appPath) |> ignore
        
let defaultNewKeyPath = Path.Combine(appPath, "key.json")

let saveTextFile path content =
    File.WriteAllText(path, content)

module Exn = 
    let getMessagesStack (e: exn) = 
        let rec loop (e: exn) (res: string) = 
            if e <> null && not <| String.IsNullOrEmpty e.Message then
                let msg = e.Message.Trim().TrimEnd('.')
                if String.IsNullOrEmpty res then msg else sprintf "%s. %s" res msg
                |> loop e.InnerException
            else res
        loop e ""

module DiskDrives =

    open System.Management
    open System

    type DriveInfo = {
        caption     : string
        description : string
        index       : int
        mediaType   : string
        model       : string
        partitions  : int
        size        : Int64
    }

    let getDiskDrives () =
        let map (mo: ManagementObject) = 
            let p = mo.Properties
            {
                caption     = p.["Caption"].Value     |> string
                description = p.["Description"].Value |> string
                index       = p.["Index"].Value       |> string |> int
                mediaType   = p.["MediaType"].Value   |> string
                model       = p.["Model"].Value       |> string
                partitions  = p.["Partitions"].Value  |> string |> int
                size        = p.["Size"].Value        |> string |> int64
            }
        let toStr (di: DriveInfo) =
            let toGb (size: Int64) = (float size) / 1024. / 1024. / 1024.
            sprintf "%d: %s [parts: %d] [%0.3f Gb]" di.index di.model di.partitions (toGb di.size)
    
        let query = new WqlObjectQuery "select Caption, Description, Index, MediaType, Model, Partitions, Size from Win32_DiskDrive where InterfaceType='USB'"
        use searcher = new ManagementObjectSearcher(query)
        searcher.Get()
        |> Seq.cast<ManagementObject>
        |> Seq.map map
        |> Seq.sortBy (fun i -> i.index)
        |> Seq.map (fun i -> new ListItem (i.index, toStr i))
        |> List.ofSeq