module SonmInstaller.Domain.UsbStickMaker

open System.IO
open System.Diagnostics
open System.Management
open SonmInstaller
open Ionic.Zip

module Wmi = 
    
    let private executeQuery (query: string) = 
        let query = new WqlObjectQuery (query)
        let searcher = new ManagementObjectSearcher(query)
        searcher.Get()
        |> Seq.cast<ManagementObject>

    let private getProp (prop: string) (mo: ManagementObject) = mo.Properties.[prop].Value |> string

    module private Query = 
        let partitions (diskIndex: int) = 
            sprintf @"associators of {Win32_DiskDrive.DeviceID='\\.\PHYSICALDRIVE%d'} where AssocClass = Win32_DiskDriveToDiskPartition" diskIndex
        let volumes part = 
            sprintf @"associators of {Win32_DiskPartition.DeviceID='%s'} where AssocClass = Win32_LogicalDiskToPartition" part

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
            "create part pri"
            "select part 1"
            "format fs=fat32 quick"
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

    let makePartitions (diskIndex: int) = 
        let p = getProc "diskpart.exe" ""
        p.Start() |> ignore
        p.StandardInput.WriteLine (getDiskPartScript diskIndex)
        p.WaitForExit()
        let output = p.StandardOutput.ReadToEnd()
        output

    let runCmd fileName args = 
        let p = getProc fileName args
        p.Start() |> ignore
        p.WaitForExit()
        p.StandardOutput.ReadToEnd()

    let getFstVolumeLetter (diskIndex: int) = 
        let firstVolume = Wmi.getVolumes diskIndex |> Seq.head
        firstVolume.driveLetter
        

open Impl

let extractMin 
    (zipPath: string) 
    (targetPath: string) =
    use zip = ZipFile.Read(zipPath)
    zip.SelectEntries("name=sonm.txt", @"\") |> Seq.iter (fun i -> i.Extract (targetPath, ExtractExistingFileAction.OverwriteSilently))

let extractZip 
    (progress: int -> int -> unit) // entries extracted -> total entries
    (zipPath: string) 
    (targetPath: string) =
    use zip = ZipFile.Read(zipPath)
    zip.ExtractProgress.Add <| (fun e -> 
        if e.EventType = ZipProgressEventType.Extracting_AfterExtractEntry then
            progress e.EntriesExtracted e.EntriesTotal
    )
    zip.ExtractAll (targetPath, ExtractExistingFileAction.OverwriteSilently)
  
let copy targetPath source = 
    let file = Path.GetFileName (source)
    let target = Path.Combine (targetPath, file)
    File.Copy (source, target, true)


type MakeUsbStickConfig = {
    zipPath: string
    toolsPath: string
    usbDiskIndex: int
    masterAddr: string
    adminKeyContent: string // content that will be saved to "admin" file
    progress: int -> int -> unit
    output: string -> unit
}

let saveMasterAddr (master: string) (usbLetter: string) =
    let fileName = usbLetter |> sprintf @"%s\sonm.txt"
    let lines = fileName |> File.ReadAllLines
    let index = lines |> Array.findIndex (fun i -> i.StartsWith "MASTER_ADDR=")
    lines.[index] <- lines.[index] + master    
    File.WriteAllLines (fileName, lines)

let saveAdminKey (adminKeyContent: string) (usbLetter: string) =
    File.WriteAllText (sprintf @"%s\admin" usbLetter, adminKeyContent)

let makeUsbStick (cfg: MakeUsbStickConfig) = 
    let letter = getFstVolumeLetter cfg.usbDiskIndex
    // tasks:
    let label     () = runCmd "label" (sprintf "%s SONM" letter) |> cfg.output
    let syslinux  () = runCmd (Path.Combine (cfg.toolsPath, "syslinux64.exe")) (sprintf "-m -a %s" letter) |> cfg.output
    let fixBoot   () = 
        [
            @"\boot\libcom32.c32"
            @"\boot\libutil.c32"
            @"\boot\vesamenu.c32"
        ]
        |> List.map (fun i -> letter + i)
        |> List.iter (copy letter)
    
    // run tasks:
    makePartitions cfg.usbDiskIndex |> cfg.output
    label()
    syslinux()
    //extractMin cfg.zipPath letter
    extractZip cfg.progress cfg.zipPath letter
    fixBoot()
    saveMasterAddr cfg.masterAddr letter
    saveAdminKey cfg.adminKeyContent letter
