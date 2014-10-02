namespace Keybase

module Sig = 

    open System
    open RestSharp
    open Newtonsoft.Json
    open Keybase.Response
    open Keybase.Request

    type Sig = 
        {
            [<JsonProperty("seqno")>]SequenceNumber: int
            [<JsonProperty("payload_hash")>]PayloadHash: string
            [<JsonProperty("sig_id")>]SignatureId: string
            [<JsonProperty("sig_id_short")>]ShortSignatureId: string
            [<JsonProperty("kid")>]KId: string
            [<JsonProperty("sig")>]Signature: string
            [<JsonProperty("payload_json")>]PayloadJson: string
            [<JsonProperty("sig_type")>]SignatureType: int
            [<JsonProperty("ctime")>]CTime: int
            [<JsonProperty("etime")>]ETime: int
            [<JsonProperty("sig_status")>]SignatureStatus: int
        }

    type SigResponse = 
        {
            [<JsonProperty("status")>]Status: Status
            [<JsonProperty("csrf_token")>]CsrfToken: string
            [<JsonProperty("sigs")>]Sigs: Sig[]
        }

    let Sigs (userId : string) =
        Request.MakeRequest<SigResponse> (Request.MakeClient()) "_/api/1.0/sig/get.json" Method.GET (fun a ->
            a.AddParameter("uid", userId)
        )