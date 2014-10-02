namespace Keybase

module Autocomplete = 

    open System
    open RestSharp
    open Newtonsoft.Json
    open Keybase.Response
    open Keybase.Request

    type CompletionComponentScore = 
        {
            [<JsonProperty("val")>]Value: string
            [<JsonProperty("score")>]Score: float
        }

    type CompletionComponentScoreFingerprint = 
        {
            [<JsonProperty("val")>]Value: string
            [<JsonProperty("score")>]Score: float
            [<JsonProperty("algo")>]Algorithm: int
            [<JsonProperty("nbits")>]NumberOfBits: int
        }

    type CompletionComponentScoreWebsite = 
        {
            [<JsonProperty("val")>]Value: string
            [<JsonProperty("score")>]Score: float
            [<JsonProperty("protocol")>]Protocol: string
        }

    type CompletionComponents = 
        {
            [<JsonProperty("username")>]Username: CompletionComponentScore
            [<JsonProperty("key_fingerprint")>]KeyFingerprint: CompletionComponentScoreFingerprint
            [<JsonProperty("github")>]Github: CompletionComponentScore
            [<JsonProperty("twitter")>]Twitter: CompletionComponentScore
            [<JsonProperty("reddit")>]Reddit: CompletionComponentScore
            [<JsonProperty("coinbase")>]Coinbase: CompletionComponentScore
            [<JsonProperty("hackernews")>]Hackernews: CompletionComponentScore
            [<JsonProperty("websites")>]Websites: CompletionComponentScoreWebsite[]
        }

    type Completion = 
        {
            [<JsonProperty("total_score")>]TotalScore: float
            [<JsonProperty("components")>]Components: CompletionComponents
            [<JsonProperty("uid")>]UID: string
            [<JsonProperty("thumbnail")>]Thumbnail: string
        }

    type AutocompleteResponse =
        {
            [<JsonProperty("status")>]Status: Status
            [<JsonProperty("completions")>]Completions: Completion[]
        }

    let Autocomplete (incomplete : string) =
        Request.MakeRequest<AutocompleteResponse> (Request.MakeClient()) "_/api/1.0/user/autocomplete.json?q={incomplete}" Method.GET (fun a -> a.AddUrlSegment("incomplete", incomplete))