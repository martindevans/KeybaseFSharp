namespace Keybase

module Key = 

    open System
    open System.Net
    open RestSharp
    open Keybase.Response
    open Keybase.Request

    let Key (username : string) =
        let response = MakeRequestRaw (MakeClient()) "{username}/key.asc" Method.GET (fun a -> a.AddUrlSegment("username", username))
        match response.StatusCode with
            | HttpStatusCode.NotFound -> null
            | _ -> response.Content