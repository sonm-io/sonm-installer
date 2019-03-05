module SonmInstaller.ReleaseMetadata

open System
open Newtonsoft.Json

type ArchivedFile = {
    [<JsonProperty(PropertyName = "name")>]
    Path: string
    Size: int
    Sha256: string
}

type Archive = {
    Name: string
    Contents: ArchivedFile list
    Sha256: string
    URL: string
}

type Component = {
    Name: string
    Archives: Archive list
    Size: int
}

type Version = {
    Major: int
    Minor: int
    Patch: int
    Build: int
    Revision: string
}

type Release = {
    Version: Version
    Channel: string
    Date: string
    Components: Component list
}

type SonmOsMetadata = {
    [<JsonProperty(PropertyName = "latest")>]
    Latest: Release
    Releases: Release list
}

type ChannelMetadata = {
    Channel: string
    [<JsonProperty(PropertyName = "sonmos")>]
    SonmOS: SonmOsMetadata
    
}

let versionToString v = 
    String.Format("{0}.{1}.{2}-{3}-{4}", v.Major, v.Minor, v.Patch, v.Build, v.Revision)
