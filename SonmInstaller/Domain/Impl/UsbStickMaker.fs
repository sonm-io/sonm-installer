module SonmInstaller.Domain.UsbStickMaker

open System.IO
open System.Diagnostics
open System.Management
open SonmInstaller
open SonmInstaller.Components.Progress
open Ionic.Zip

module Wmi = 
    
    let private executeQuery (query: string) = 
        let query = new WqlObjectQuery (query)
        let searcher = new ManagementObjectSearcher(query)
        let moc = searcher.Get()
        try
            moc.Count |> ignore
            moc |> Seq.cast<ManagementObject>
        with
        | exn -> Seq.empty

    let private getProp (prop: string) (mo: ManagementObject) = mo.Properties.[prop].Value |> string

    module private Query = 
        let partitions (diskIndex: int) = 
            sprintf @"associators of {Win32_DiskDrive.DeviceID='\\.\PHYSICALDRIVE%d'} where ResultClass = Win32_DiskPartition" diskIndex
        let volumes part = 
            sprintf @"associators of {Win32_DiskPartition.DeviceID='%s'} where ResultClass = Win32_LogicalDisk" part

    // Public Interface:

    type Volume = {
        driveLetter: string // Letter with colon, ie: "C:"
        volumeName: string
    } with
        static member FromManObj (mo: ManagementObject) = 
            {
                driveLetter = getProp "DeviceID" mo // Letter with colon, ie: "C:"
                volumeName = getProp "VolumeName" mo
            }

    let getVolumes (diskIndex: int) =
        Query.partitions diskIndex
        |> executeQuery
        |> Seq.map (fun mo -> mo |> getProp "DeviceID")
        |> Seq.map (Query.volumes >> executeQuery)
        |> Seq.concat
        |> Seq.map Volume.FromManObj

module Impl = 

    let getDiskPartScript (diskIndex: int) =
        [
            sprintf "select disk %d" diskIndex
            "clean"
            "create part primary size=2048"
            "select partition 1"
            "format fs=fat32 quick"
            "create partition primary"
            "exit\n"
        ] |> String.concat "\n"

    let getProc fileName args = 
        let p = new Process()
        let startInfo = p.StartInfo
        startInfo.UseShellExecute <- false
        startInfo.CreateNoWindow <- true
        startInfo.WindowStyle <- ProcessWindowStyle.Hidden
        startInfo.RedirectStandardOutput <- true
        startInfo.RedirectStandardInput <- true
        startInfo.FileName <- fileName
        startInfo.Arguments <- args
        p

    let makePartitions (diskIndex: int) = async {
        let p = getProc "diskpart.exe" ""
        p.Start() |> ignore
        p.StandardInput.WriteLine (getDiskPartScript diskIndex)
        p.WaitForExit()
        let! output = p.StandardOutput.ReadToEndAsync() |> Async.AwaitTask
        return output
    }

    let runCmd fileName args = 
        let p = getProc fileName args
        p.Start() |> ignore
        p.WaitForExit()
        p.StandardOutput.ReadToEnd()

    let getFstVolumeLetter (diskIndex: int) = 
        let firstVolume = Wmi.getVolumes diskIndex |> Seq.head
        firstVolume.driveLetter
        

open Impl
open SonmInstaller.ReleaseMetadata
open SonmInstaller.Tools
open SonmInstaller.Atom

type MakeUsbStickConfig = {
    release: Release
    downloadsPath: string
    toolsPath: string
    usbDiskIndex: int
    masterAddr: string
    adminKeyContent: string // content that will be saved to "admin" file
    progress: State -> unit
    output: string -> unit
}

let aggregateChecks checks =
    Array.fold (fun acc res -> acc && res) true checks

let checkFileIntegrity report basePath file = async {
    let archiveFile = Path.Combine(basePath, file.Path)
    if File.Exists archiveFile then
        let fileSize = FileInfo(archiveFile).Length
        let! hash = hashFile None archiveFile
        let res = hash = file.Sha256
        report 1
        return res || fileSize = 0L
    else
        report 1
        return false
}

let extractArchive
    report
    (cfg: MakeUsbStickConfig)
    (targetPath: string)
    (archive: Archive) =
    let version = versionToString cfg.release.Version
    let archiveName = Path.Combine(cfg.downloadsPath, version, archive.Name)
    let extraction = async {
        use zip = ZipFile.Read(archiveName)
        zip.ExtractProgress.Add <| (fun e -> 
            if e.EventType = ZipProgressEventType.Extracting_AfterExtractEntry &&
               not e.CurrentEntry.IsDirectory then
                report 1)
        zip.ExtractAll(targetPath, ExtractExistingFileAction.OverwriteSilently)
    }
    let check = archive.Contents |> List.map (checkFileIntegrity report targetPath) |> Async.Parallel
    async {
        let! _ = extraction
        let! check_res = check
        if check_res |> aggregateChecks |> not then
            failwithf @"Integrity check is failed for archive %s" archive.Name
    }


let extractComponent
    report
    (cfg: MakeUsbStickConfig)
    (targetPath: string)
    (comp: Component) = 
    comp.Archives |> List.map (extractArchive report cfg targetPath) |> Async.Parallel

let extractRelease
    report
    (cfg: MakeUsbStickConfig) 
    (targetPath: string) = async {
        let! _ = cfg.release.Components |> List.map (extractComponent report cfg targetPath) |> Async.Parallel
        return ()
    }
  
let copy targetPath source = 
    let file = Path.GetFileName (source)
    let target = Path.Combine (targetPath, file)
    let targetDir = Path.GetDirectoryName(target)
    if Directory.Exists(targetDir) |> not then 
        Directory.CreateDirectory(targetDir) |> ignore
    File.Copy (source, target, true)

let saveMasterAddr (master: string) (usbLetter: string) = async {
    let fileName = usbLetter |> sprintf @"%s\sonm.txt"
    let lines = if File.Exists fileName then
                    let lines = fileName |> File.ReadAllLines
                    let index = lines |> Array.findIndex (fun i -> i.StartsWith "MASTER_ADDR=")
                    lines.[index] <- lines.[index] + master
                    lines
                else
                    [|sprintf @"MASTER_ADDR=%s" master|]
    File.WriteAllLines (fileName, lines)
}

let saveAdminKey (adminKeyContent: string) (usbLetter: string) =
    File.WriteAllText (sprintf @"%s\admin" usbLetter, adminKeyContent)

let formatProgress = {
    State.captionTpl = "Formatting USB Drive"
    style = ProgressStyle.Marquee
    current = 0.0
    total = 0.0
}

let sharedState caption cont total progress =
    let counter = atom (fun () -> 0)
    let state = {
        State.captionTpl=if cont then caption + " {0:0} of {1:0} ({2:0}%)" else caption
        style=if cont then ProgressStyle.Continuous else ProgressStyle.Marquee
        current=0.0
        total=float total
    }
    (fun current -> 
        swap counter (fun f -> (fun result () -> result + current) <| f()) |> ignore
        {state with current=float ((!counter)())} |> progress)


let makeUsbStick (cfg: MakeUsbStickConfig) = async {
    cfg.progress formatProgress
    let! dpOut = makePartitions cfg.usbDiskIndex
    dpOut |> cfg.output
    let letter = getFstVolumeLetter cfg.usbDiskIndex
    // tasks:
    let syslinux  () = runCmd (Path.Combine (cfg.toolsPath, "syslinux64.exe")) (sprintf "-m -a %s" letter) |> cfg.output
    let fixBoot   () = async {
        [
            @"\boot\libcom32.c32"
            @"\boot\libutil.c32"
            @"\boot\vesamenu.c32"
        ]
        |> List.map (fun i -> letter + i)
        |> List.iter (copy letter)
    }
    
    // run tasks:
    cfg.progress {formatProgress with captionTpl = "Making USB Drive Bootable"}
    syslinux()
    let totalFiles = cfg.release.Components
                     |> List.map (fun c -> c.Archives)
                     |> List.concat
                     |> List.map (fun a -> a.Contents)
                     |> List.concat
                     |> List.length
    let report = sharedState "Extracting and verifying files" true (totalFiles * 2) cfg.progress
    let! _ = [
                extractRelease report cfg letter
                //fixBoot()
                saveMasterAddr cfg.masterAddr letter
                //saveAdminKey cfg.adminKeyContent letter
                ] |> Async.Parallel
    return ()
}
