namespace SonmInstaller.Domain

open System
open System.Diagnostics
open System.IO
open System.Configuration

open SonmInstaller.Tools
open UsbDrivesManager
open SonmInstaller.Utils

open Nethereum.Web3.Accounts


module private Impl = 
    let startProc (path: string) = path |> Process.Start |> ignore
    let openKeyFolder path = path |> Path.GetDirectoryName |> startProc

open Impl
open UsbStickMaker

type DomainService () = 

    let mutable master = Blockchain.genAccount ()
    let admin = Blockchain.genAccount ()
    let isProcessElevated = UacHelper.IsProcessElevated

    let usbMan = new UsbManager()

    let getAppSetting (key: string) = 
        ConfigurationManager.AppSettings.[key];
    
    let sonmOsMetadataUrl = getAppSetting "SonmReleaseMetadataUrl"

    let sonmOsImageDestination = 
        Path.Combine (appPath, Path.GetFileName sonmOsMetadataUrl)

    let libPath = Path.Combine(getExePath (), "lib")

    let generateKeyStore path pass = async {
        let json = Blockchain.generateKeyStore master pass
        File.WriteAllText (path, json)
        return master.Address
    }

    let importKeyStore path pass = async {
        let json = File.ReadAllText path
        master <- Account.LoadFromKeyStore(json, pass)
        return master.Address
    }

    let getUsbDrives () = 
        let toStr (di: DiskInfo) =
            let toGb (size: Int64) = (float size) / 1024. / 1024. / 1024.
            sprintf "%d: %s [parts: %d] [%0.3f Gb]" di.Index di.Model di.Partitions (toGb di.Size)
        usbMan.GetUsbDrives()
        |> Seq.map (fun i -> i.Index, toStr i)
        |> Seq.sortBy fst
        |> List.ofSeq
    
    let makeUsbStick diskIndex release progress = 
        let getAdminKeyContent () = 
            [
                admin.Address
                admin.PrivateKey
            ] |> String.concat "\n"
        
        let cfg = {
            release = release
            downloadsPath = appPath
            toolsPath = libPath
            usbDiskIndex = diskIndex
            masterAddr = master.Address
            adminKeyContent = getAdminKeyContent ()
            progress = progress
            output = fun _ -> ()
        }
        UsbStickMaker.makeUsbStick cfg

    let callSmartContract (withdrawTo: string) (minPayout: float) = async {
        do! {
                Blockchain.blockChainUrl = getAppSetting "BlockChainUrl"
                Blockchain.smartContractAddress = getAppSetting "RegisterAdminScAddr"
                Blockchain.account = master
                Blockchain.adminAddr = admin.Address
            } 
            |> Blockchain.registerAdmin
    }

    do  
        ensureAppPathExists () 

    member x.GetService () = 
        { 
            isProcessElevated = (fun () -> isProcessElevated)
            getUtcFilePath = fun () -> 
                Path.Combine (keyPath, (Blockchain.getUtcFileName master.Address) + ".json")
            getUsbDrives = getUsbDrives
            downloadMetadata = Download.downloadMetadata sonmOsMetadataUrl
            downloadRelease = Download.downloadRelease appPath
            generateKeyStore = generateKeyStore
            importKeyStore = importKeyStore
            openKeyFolder = openKeyFolder
            openKeyFile = startProc
            callSmartContract = callSmartContract
            makeUsbStick = makeUsbStick
            closeApp = id
        }