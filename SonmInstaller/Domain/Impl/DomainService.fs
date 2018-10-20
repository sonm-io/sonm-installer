namespace SonmInstaller.Domain

open System.Diagnostics
open System.IO
open System.Configuration
open SonmInstaller.Tools



module private Impl = 
    let startProc (path: string) = path |> Process.Start |> ignore
    let openKeyFile path = path |> Path.GetFileName |> startProc

open Impl

type DomainService () = 

    let getAppSetting (key: string) = 
        ConfigurationManager.AppSettings.[key];
    
    let sonmOsImageUrl = getAppSetting "SonmOsImageUrl"

    let sonmOsImageDestination = 
        Path.Combine (appPath, Path.GetFileName sonmOsImageUrl)

    //let generateKeyStore password = async {
    //    let (fileName, json) = Blockchain.generateKeyStore password
    //    let path = Path.Combine (dir, fileName)
    //    File.WriteAllText (path, json)
    //}

    do  
        ensureAppPathExists () 

    member __.GetService () = 
        { Mock.createEmptyService 3000 with
            startDownload = Download.startDownload sonmOsImageUrl sonmOsImageDestination
            openKeyFolder = startProc
            openKeyFile = openKeyFile
        }