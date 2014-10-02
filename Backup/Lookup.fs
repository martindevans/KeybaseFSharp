﻿// Learn more about F# at http://fsharp.net

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

type User = 
    {
        [<JsonProperty("id")>]Id: string
        [<JsonProperty("basics")>]Basics: Basics
        [<JsonProperty("profile")>]Profile: Profile
    }

module Lookup =

    open System
    open RestSharp
    open Keybase.Response

    type UserResponse = 
        {
            [<JsonProperty("status")>]Status: Status
            [<JsonProperty("them")>]Them: User[]
        }

    let Username (usernames : string[]) =
        Keybase.Response.MakeRequest<UserResponse> "_/api/1.0/user/lookup.json?usernames={username}" Method.GET (fun a -> a.AddUrlSegment("username", String.concat "," usernames))

    let Domain (domain : string) = 
        Keybase.Response.MakeRequest<UserResponse> "_/api/1.0/user/lookup.json?domain={domain}" Method.GET (fun a -> a.AddUrlSegment("domain", domain))

    let Twitter (username : string) = 
        Keybase.Response.MakeRequest<UserResponse> "_/api/1.0/user/lookup.json?twitter={username}" Method.GET (fun a -> a.AddUrlSegment("username", username))

    let Github (username : string) = 
        Keybase.Response.MakeRequest<UserResponse> "_/api/1.0/user/lookup.json?github={username}" Method.GET (fun a -> a.AddUrlSegment("username", username))

    let Reddit (username : string) = 
        Keybase.Response.MakeRequest<UserResponse> "_/api/1.0/user/lookup.json?reddit={username}" Method.GET (fun a -> a.AddUrlSegment("username", username))

    let HackerNews (username : string) = 
        Keybase.Response.MakeRequest<UserResponse> "_/api/1.0/user/lookup.json?hackernews={username}" Method.GET (fun a -> a.AddUrlSegment("username", username))

    let Coinbase (username : string) = 
        Keybase.Response.MakeRequest<UserResponse> "_/api/1.0/user/lookup.json?coinbase={username}" Method.GET (fun a -> a.AddUrlSegment("username", username))

    let KeyFingerprint (fingerprint : string) = 
        Keybase.Response.MakeRequest<UserResponse> "_/api/1.0/user/lookup.json?key_fingerprint={fingerprint}" Method.GET (fun a -> a.AddUrlSegment("fingerprint", fingerprint))