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
        fileSystem: string
    } with
        static member FromManObj (mo: ManagementObject) = 
            {
                driveLetter = getProp "DeviceID" mo // Letter with colon, ie: "C:"
                volumeName = getProp "VolumeName" mo
                fileSystem = getProp "FileSystem" mo
            }

    type Partition = {
        size: int64
    } with
        static member FromManObj mo = 
            {
                size = getProp "Size" mo |> int64
            }

    let getVolumes (diskIndex: int) =
        Query.partitions diskIndex
        |> executeQuery
        |> Seq.map (fun mo -> mo |> getProp "DeviceID")
        |> Seq.map (Query.volumes >> executeQuery)
        |> Seq.concat
        |> Seq.map Volume.FromManObj

    let getPartitions diskIndex =
        Query.partitions diskIndex
        |> executeQuery
        |> Seq.map Partition.FromManObj

module Impl = 

    let getFullCleanDiskPartScript (diskIndex: int) =
        [
            sprintf "select disk %d" diskIndex
            "clean"
            "create part primary size=2560"
            "select partition 1"
            "format label=\"SONM\" fs=fat32 quick"
            "create partition primary"
            "exit\n"
        ] |> String.concat "\n"

    let getCleanInstallPartitionScript diskIndex =
        [
            sprintf "select disk %d" diskIndex
            "select partition 1"
            "format label=\"SONM\" fs=fat32 quick"
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

    let makePartitions (diskIndex: int) wipe = async {
        let script = if wipe then getFullCleanDiskPartScript diskIndex else getCleanInstallPartitionScript diskIndex
        let p = getProc "diskpart.exe" ""
        p.Start() |> ignore
        p.StandardInput.WriteLine script
        p.WaitForExit()
        let! output = p.StandardOutput.ReadToEndAsync() |> Async.AwaitTask
        return output
    }

    let runCmd fileName args = 
        let p = getProc fileName args
        p.Start() |> ignore
        p.WaitForExit()
        p.StandardOutput.ReadToEnd()

    let getFstVolume (diskIndex: int) = 
        Wmi.getVolumes diskIndex |> List.ofSeq |> List.tryHead
        

open Impl
open SonmInstaller.ReleaseMetadata
open SonmInstaller.Tools
open SonmInstaller.Atom
open SonmInstaller.Domain

type MakeUsbStickConfig = {
    release: Release
    downloadsPath: string
    toolsPath: string
    usbDiskIndex: int
    masterAddr: string
    adminKeyContent: string // content that will be saved to "admin" file
    progress: State -> unit
    output: string -> unit
    wipe: bool
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

let saveMasterAddr sonmTxt (usbLetter: string) = async {
    let fileName = usbLetter |> sprintf @"%s\sonm.txt"
    File.WriteAllLines (fileName, sonmTxt)
}

let getSonmTxtContent cfg =
    let volume = getFstVolume cfg.usbDiskIndex
    let sonmTxtExists = match volume with 
                        |Some v -> File.Exists (sprintf @"%s\sonm.txt" v.driveLetter)
                        |None -> false
    if not cfg.wipe && Option.isSome volume && sonmTxtExists then
        let letter = volume.Value.driveLetter
        let fileName = letter |> sprintf @"%s\sonm.txt"
        File.ReadAllLines fileName
    else
        [|"# Ethereum address of master"
          sprintf @"MASTER_ADDR=%s" cfg.masterAddr|]

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

let doesDiskContainsSonm index =
    let twoPartitions _ = (Wmi.getPartitions index |> List.ofSeq |> List.length) = 2
    let firstPartitionLabeled _ = (Wmi.getVolumes index |> List.ofSeq |> List.head).volumeName = "SONM"
    let firstPartitionIsLargeEnough _ = (Wmi.getPartitions index |> List.ofSeq |> List.item 1).size >= 2L * pown 2L 30
    let firstPartitionIsFat _ = (Wmi.getVolumes index |> List.ofSeq |> List.head).fileSystem = "FAT32"
    let thereAreSonmTxt _ = 
        let driveLetter = (Wmi.getVolumes index |> List.ofSeq |> List.head).driveLetter
        driveLetter |> sprintf @"%s\sonm.txt" |> File.Exists
    twoPartitions () && firstPartitionLabeled () &&
        firstPartitionIsLargeEnough () && firstPartitionIsFat () && thereAreSonmTxt ()

let makeUsbStick (cfg: MakeUsbStickConfig) = async {
    cfg.progress formatProgress
    let containsSonm = doesDiskContainsSonm cfg.usbDiskIndex
    let sonmTxt = getSonmTxtContent cfg
    let! dpOut = makePartitions cfg.usbDiskIndex (cfg.wipe || not containsSonm)
    dpOut |> cfg.output

    let letter = (getFstVolume cfg.usbDiskIndex).Value.driveLetter
    // tasks:
    let syslinux  () = runCmd (Path.Combine (cfg.toolsPath, "syslinux64.exe")) (sprintf "-m -a -d boot %s" letter) |> cfg.output
    
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
                saveMasterAddr sonmTxt letter
                ] |> Async.Parallel
    return ()
}


