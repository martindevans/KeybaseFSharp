namespace Keybase

module Sig = 

    open System
    open RestSharp
    open Newtonsoft.Json
    open Newtonsoft.Json.Linq
    open Keybase.Response
    open Keybase.Request

    type SignatureType =
        | Self = 1
        | Proof = 2
        | Track = 3
        | Probably_RevokeTrack = 4
        | Unknown_RevokeSomething = 5
        | Currency = 6

    type Key =
        {
            [<JsonProperty("fingerprint")>]Fingerprint: string
            [<JsonProperty("host")>]Host: string
            [<JsonProperty("key_id")>]KeyId: string
            [<JsonProperty("uid")>]UId: string
            [<JsonProperty("username")>]Username: string
        }

    type LightweightKey =
        {
            [<JsonProperty("key_fingerprint")>]KeyFingerprint: string
            [<JsonProperty("kid")>]KeyId: string
        }

    type TrackSignaturePayloadBodyBasics =
        {
            [<JsonProperty("id_version")>]IdVersion: int
            [<JsonProperty("last_id_change")>]LastIdChange: int
            [<JsonProperty("username")>]Username: string
        }

    type TrackingSignaturePayloadBodyTrack =
        {
            [<JsonProperty("basics")>]Basics: TrackSignaturePayloadBodyBasics
            [<JsonProperty("id")>]Id: string
            [<JsonProperty("key")>]Key: LightweightKey
            //remote_proofs
            //seq_tail
        }

    type TrackingSignaturePayloadBody =
        {
            //client
            [<JsonProperty("key")>]Key: Key
            [<JsonProperty("track")>]Track: TrackingSignaturePayloadBodyTrack
            [<JsonProperty("type")>]Type: string
            [<JsonProperty("version")>]Version: int
        }

    type TrackingSignaturePayload =
        {
            [<JsonProperty("body")>]Body: TrackingSignaturePayloadBody
            [<JsonProperty("ctime")>]CTime: int
            [<JsonProperty("expire_in")>]ExpireIn: int
            [<JsonProperty("prev")>]Previous: string
            [<JsonProperty("seqno")>]SequenceNumber: int
            [<JsonProperty("tag")>]Tag: string
        }

    type Sig = 
        {
            [<JsonProperty("seqno")>]SequenceNumber: int
            [<JsonProperty("payload_hash")>]PayloadHash: string
            [<JsonProperty("sig_id")>]SignatureId: string
            [<JsonProperty("sig_id_short")>]ShortSignatureId: string
            [<JsonProperty("kid")>]KId: string
            [<JsonProperty("sig")>]Signature: string
            [<JsonProperty("payload_json")>]PayloadJson: string
            [<JsonProperty("sig_type")>]SignatureType: SignatureType
            [<JsonProperty("ctime")>]CTime: int
            [<JsonProperty("etime")>]ETime: int
            [<JsonProperty("sig_status")>]SignatureStatus: int
        }

        with
            member this.AsTrackeeSignature =
                if not (this.SignatureType.Equals(SignatureType.Track)) then
                    raise (new System.InvalidOperationException("Signature is not a tracking signature"))
                JsonConvert.DeserializeObject<TrackingSignaturePayload>(this.PayloadJson)


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