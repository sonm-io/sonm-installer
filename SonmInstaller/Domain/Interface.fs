namespace SonmInstaller.Domain

type Progress = int64 -> int64 -> unit

type IService =
    abstract member StartDownload: 
        uri: string -> 
        dest: string -> 
        progressCb: (int64 -> int64 -> unit) -> // bytesDownloaded -> total
        completeCb: (unit -> unit) ->
        unit
    abstract member OpenKeyFolder: path: string -> unit
    abstract member OpenKeyFile: path: string -> unit

