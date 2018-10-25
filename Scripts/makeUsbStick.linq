<Query Kind="FSharpProgram">
  <Reference Relative="..\SonmInstaller\bin\Debug\SonmInstaller.exe">D:\sonm\prj\installer\repo\SonmInstaller\bin\Debug\SonmInstaller.exe</Reference>
  <Namespace>SonmInstaller.Domain.UsbStickMaker</Namespace>
</Query>

let progressBar = new Util.ProgressBar("Extracting")
progressBar.Dump() 

let progress (transferred: int) (total: int) = 
    let percent = 
        (float transferred / float total)
        |> (*)100. 
        |> int
    progressBar.Percent <- percent

let cfg = {
    zipPath = @"d:\sonm\sosna\sonmos.zip"
    toolsPath = @"d:\sonm\prj\installer\repo\SonmInstaller\bin\Debug\lib"
    usbDiskIndex = 1
    progress = progress
    output = fun s -> Console.WriteLine(s)
}

open Impl

let letter = getFstVolumeLetter cfg.usbDiskIndex

//Console.WriteLine (letter)
//makePartitions cfg.usbDiskIndex |> cfg.output
//runCmd "label.exe" (sprintf "%s SOSNA" letter) |> cfg.output
//runCmd (Path.Combine (cfg.toolsPath, "syslinux64.exe")) (sprintf "-m -a %s" letter) |> cfg.output
//extractZip cfg.progress cfg.zipPath letter
//[
//    @"\boot\libcom32.c32"
//    @"\boot\libutil.c32"
//    @"\boot\vesamenu.c32"
//]
//|> List.map (fun i -> letter + i)
//|> List.iter (copy (letter))

makeUsbStick cfg

Console.WriteLine ("done")