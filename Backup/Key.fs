namespace Keybase

module Key = 

    open System
    open System.Net
    open RestSharp
    open Keybase.Response

    let Key (username : string) =
        let response = MakeRequestRaw "{username}/key.asc" Method.GET (fun a -> a.AddUrlSegment("username", username))
        match response.StatusCode with
            | HttpStatusCode.NotFound -> null
            | _ -> response.Content