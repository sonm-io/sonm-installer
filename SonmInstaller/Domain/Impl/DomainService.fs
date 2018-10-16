namespace SonmInstaller.Domain

open System.Diagnostics
open System.IO

module private Impl = 
    let startProc (path: string) = path |> Process.Start |> ignore
    let openKeyFile path = path |> Path.GetFileName |> startProc

open Impl

type DomainService = 
    member __.GetService () = {
        Mock.service with
            openKeyFolder = startProc
            openKeyFile = openKeyFile
    }