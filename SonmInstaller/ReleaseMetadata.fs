module SonmInstaller.ReleaseMetadata


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
    Size: int64
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

