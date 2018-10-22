namespace SonmInstaller.Domain

open System.Diagnostics
open System.IO
open System.Configuration
open SonmInstaller.Tools

open Nethereum.Web3.Accounts


module private Impl = 
    let startProc (path: string) = path |> Process.Start |> ignore
    let openKeyFolder path = path |> Path.GetDirectoryName |> startProc

open Impl

type DomainService () = 

    let mutable account = Blockchain.genAccount ()

    let getAppSetting (key: string) = 
        ConfigurationManager.AppSettings.[key];
    
    let sonmOsImageUrl = getAppSetting "SonmOsImageUrl"

    let sonmOsImageDestination = 
        Path.Combine (appPath, Path.GetFileName sonmOsImageUrl)

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

    do  
        ensureAppPathExists () 

    member x.GetService () = 
        { Mock.createEmptyService 3000 with
            getUtcFilePath = fun () -> 
                Path.Combine (appPath, (Blockchain.getUtcFileName account.Address) + ".json")
            startDownload = Download.startDownload sonmOsImageUrl sonmOsImageDestination
            generateKeyStore = generateKeyStore
            importKeyStore = importKeyStore
            openKeyFolder = openKeyFolder
            openKeyFile = startProc
        }