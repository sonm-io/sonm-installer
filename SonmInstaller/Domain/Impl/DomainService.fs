namespace SonmInstaller.Domain

open System
open System.Diagnostics
open System.IO
open System.Configuration
open SonmInstaller.Tools
open UsbDrivesManager

open Nethereum.Web3.Accounts


module private Impl = 
    let startProc (path: string) = path |> Process.Start |> ignore
    let openKeyFolder path = path |> Path.GetDirectoryName |> startProc

open Impl
open UsbStickMaker

type DomainService () = 

    let mutable account = Blockchain.genAccount ()

    let usbMan = new UsbManager()

    let getAppSetting (key: string) = 
        ConfigurationManager.AppSettings.[key];
    
    let sonmOsImageUrl = getAppSetting "SonmOsImageUrl"

    let sonmOsImageDestination = 
        Path.Combine (appPath, Path.GetFileName sonmOsImageUrl)

    let libPath = Path.Combine(getExePath (), "lib")

    let generateKeyStore path pass = async {
        let json = Blockchain.generateKeyStore account pass
        File.WriteAllText (path, json)
        return account.Address
    }

    let importKeyStore path pass = async {
        let json = File.ReadAllText path
        account <- Account.LoadFromKeyStore(json, pass)
        return account.Address
    }

    let getUsbDrives () = 
        let toStr (di: DiskInfo) =
            let toGb (size: Int64) = (float size) / 1024. / 1024. / 1024.
            sprintf "%d: %s [parts: %d] [%0.3f Gb]" di.Index di.Model di.Partitions (toGb di.Size)
        usbMan.GetUsbDrives()
        |> Seq.map (fun i -> i.Index, toStr i)
        |> Seq.sortBy fst
        |> List.ofSeq
    
    let makeUsbStick diskIndex progress = 
        let cfg = {
            zipPath = sonmOsImageDestination
            toolsPath = libPath
            usbDiskIndex = diskIndex
            progress = progress
            output = fun _ -> ()
        }
        async {
            UsbStickMaker.makeUsbStick cfg
        }

    do  
        ensureAppPathExists () 

    member x.GetService () = 
        { Mock.createEmptyService 3000 with
            getUtcFilePath = fun () -> 
                Path.Combine (appPath, (Blockchain.getUtcFileName account.Address) + ".json")
            getUsbDrives = getUsbDrives
            startDownload = Download.startDownload sonmOsImageUrl sonmOsImageDestination
            generateKeyStore = generateKeyStore
            importKeyStore = importKeyStore
            openKeyFolder = openKeyFolder
            openKeyFile = startProc
            makeUsbStick = makeUsbStick
        }