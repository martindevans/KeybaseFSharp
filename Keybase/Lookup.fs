// Learn more about F# at http://fsharp.net

namespace Keybase.User

open Newtonsoft.Json

type Basics = 
    {
        [<JsonProperty("username")>]Username: string
        [<JsonProperty("ctime")>]CTime: int
        [<JsonProperty("mtime")>]MTime: int
        [<JsonProperty("id_version")>]IdVersion: int
        [<JsonProperty("track_version")>]TrackVersion: int
        [<JsonProperty("last_id_change")>]LastIdChange: int
        [<JsonProperty("salt")>]Salt: string
    }

type Profile = 
    {
        [<JsonProperty("mtime")>]mtime: int
        [<JsonProperty("full_name")>]FullName: string
        [<JsonProperty("location")>]Location: string
        [<JsonProperty("bio")>]Bio: string
    }

type Picture =
    {
        [<JsonProperty("url")>]Url: string
        [<JsonProperty("width")>]Width: int
        [<JsonProperty("height")>]Height: int
    }

type Pictures = 
    {
        [<JsonProperty("primary")>]Primary: Picture
    }

type Key =
    {
        [<JsonProperty("kid")>]KeyId: string
        [<JsonProperty("key_type")>]KeyType: int
        [<JsonProperty("bundle")>]Bundle: string
        [<JsonProperty("mtime")>]MTime: int
        [<JsonProperty("ctime")>]CTime: int
        [<JsonProperty("key_fingerprint")>]KeyFingerprint: string
        [<JsonProperty("key_bits")>]KeyBits: int
        [<JsonProperty("key_algo")>]KeyAlgorithm: int
    }

type KeyCollection =
    {
        [<JsonProperty("primary")>]Primary: Key
    }

type Proof =
    {
        [<JsonProperty("proof_type")>]ProofType: string
        [<JsonProperty("nametag")>]NameTag: string
        [<JsonProperty("state")>]State: int
        [<JsonProperty("proof_url")>]ProofUrl: string
        [<JsonProperty("sig_id")>]SignatureId: string
        [<JsonProperty("proof_id")>]ProofId: string
        [<JsonProperty("human_url")>]HumanUrl: string
        [<JsonProperty("service_url")>]ServiceUrl: string
        [<JsonProperty("presentation_group")>]PresentationGroup: string
        [<JsonProperty("presentation_tag")>]PresentationTag: string
    }

type ProofsSummary =
    {
        //by_proof_type
        //by_presentation_group
        [<JsonProperty("all")>]All: Proof[]
    }

type User = 
    {
        [<JsonProperty("id")>]Id: string
        [<JsonProperty("basics")>]Basics: Basics
        [<JsonProperty("profile")>]Profile: Profile
        [<JsonProperty("pictures")>]Pictures: Pictures
        [<JsonProperty("public_keys")>]PublicKeys: KeyCollection
        [<JsonProperty("private_keys")>]PrivateKeys: KeyCollection
        [<JsonProperty("proofs_summary")>]ProofsSummary: ProofsSummary
    }

module Lookup =

    open System
    open RestSharp
    open Keybase.Response
    open Keybase.Request

    type UserResponse = 
        {
            [<JsonProperty("status")>]Status: Status
            [<JsonProperty("them")>]Them: User[]
        }

    let Username (usernames : string[]) =
        Keybase.Request.MakeRequest<UserResponse> (Keybase.Request.MakeClient()) "_/api/1.0/user/lookup.json?usernames={username}" Method.GET (fun a -> a.AddUrlSegment("username", String.concat "," usernames))

    let Domain (domain : string) = 
        Keybase.Request.MakeRequest<UserResponse> (Keybase.Request.MakeClient()) "_/api/1.0/user/lookup.json?domain={domain}" Method.GET (fun a -> a.AddUrlSegment("domain", domain))

    let Twitter (username : string) = 
        Keybase.Request.MakeRequest<UserResponse> (Keybase.Request.MakeClient()) "_/api/1.0/user/lookup.json?twitter={username}" Method.GET (fun a -> a.AddUrlSegment("username", username))

    let Github (username : string) = 
        Keybase.Request.MakeRequest<UserResponse> (Keybase.Request.MakeClient()) "_/api/1.0/user/lookup.json?github={username}" Method.GET (fun a -> a.AddUrlSegment("username", username))

    let Reddit (username : string) = 
        Keybase.Request.MakeRequest<UserResponse> (Keybase.Request.MakeClient()) "_/api/1.0/user/lookup.json?reddit={username}" Method.GET (fun a -> a.AddUrlSegment("username", username))

    let HackerNews (username : string) = 
        Keybase.Request.MakeRequest<UserResponse> (Keybase.Request.MakeClient()) "_/api/1.0/user/lookup.json?hackernews={username}" Method.GET (fun a -> a.AddUrlSegment("username", username))

    let Coinbase (username : string) = 
        Keybase.Request.MakeRequest<UserResponse> (Keybase.Request.MakeClient()) "_/api/1.0/user/lookup.json?coinbase={username}" Method.GET (fun a -> a.AddUrlSegment("username", username))

    let KeyFingerprint (fingerprint : string) = 
        Keybase.Request.MakeRequest<UserResponse> (Keybase.Request.MakeClient()) "_/api/1.0/user/lookup.json?key_fingerprint={fingerprint}" Method.GET (fun a -> a.AddUrlSegment("fingerprint", fingerprint))