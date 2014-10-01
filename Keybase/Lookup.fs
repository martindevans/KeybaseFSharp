// Learn more about F# at http://fsharp.net

namespace Keybase.User

type Basics = 
    {
        username: string
        ctime: int
        mtime: int
        id_version: int
        track_version: int
        last_id_change: int
        salt: string
    }

type Profile = 
    {
        mtime: int
        full_name: string
        location: string
        bio: string
    }

type User = 
    {
        id: string
        basics: Basics
        profile: Profile
    }

module Lookup =

    open System
    open RestSharp
    open Newtonsoft.Json
    open Keybase.Response

    type UserResponse = 
        {
            status: Status
            them: User[]
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