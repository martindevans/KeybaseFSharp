namespace Keybase

module Merkle = 

    open System
    open Keybase.Response
    open RestSharp

    type MerkleRootResponse = 
        {
            status: Status
            hash: string
            seqno: int
            ctime_string: string
            ctime: int
            //sig: string
            payload_json: string
        }

    type MerkleBlock =
        {
            tab: string[]
            //type: int
        }

    type MerkleBlockResponse =
        {
            status: Status
            hash: string
            value: MerkleBlock
        }

    let Root (seqno : Nullable<int>) (ctime : Nullable<int>) = 
        Keybase.Response.MakeRequest<MerkleRootResponse> "_/api/1.0/merkle/root.json" Method.GET (fun a ->
            if seqno.HasValue then ignore (a.AddParameter ("seqno", seqno.Value))
            if ctime.HasValue then ignore (a.AddParameter ("ctime", ctime.Value))
            a :> IRestRequest
        )

    let Block (hash : string) = 
        Keybase.Response.MakeRequest<MerkleBlockResponse> "_/api/1.0/merkle/block.json" Method.GET (fun a -> a.AddParameter("hash ", hash))