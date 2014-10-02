namespace Keybase

module Response = 

    open System
    open RestSharp
    open Newtonsoft.Json

    type Status = 
        {
            [<JsonProperty("code")>]Code: int
            [<JsonProperty("name")>]Name: string
        }
