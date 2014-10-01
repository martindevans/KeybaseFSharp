namespace Keybase

module Autocomplete = 

    open System
    open RestSharp
    open Newtonsoft.Json
    open Keybase.Response

    type CompletionComponentScore = 
        {
            //val: string
            score: float
        }

    type CompletionComponentScoreFingerprint = 
        {
            //val: string
            score: float
            algo: int
            nbits: int
        }

    type CompletionComponentScoreWebsite = 
        {
            //val: string
            score: float
            protocol: string
        }

    type CompletionComponents = 
        {
            username: CompletionComponentScore
            key_fingerprint: CompletionComponentScoreFingerprint
            github: CompletionComponentScore
            twitter: CompletionComponentScore
            reddit: CompletionComponentScore
            coinbase: CompletionComponentScore
            hackernews: CompletionComponentScore
            websites: CompletionComponentScoreWebsite[]
        }

    type Completion = 
        {
            total_score: float
            components: CompletionComponents
            uid: string
            thumbnail: string
        }

    type AutocompleteResponse =
        {
            status: Status
            completions: Completion[]
        }

    let Autocomplete (incomplete : string) =
        Response.MakeRequest<AutocompleteResponse> "_/api/1.0/user/autocomplete.json?q={incomplete}" Method.GET (fun a -> a.AddUrlSegment("incomplete", incomplete))