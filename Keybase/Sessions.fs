namespace Keybase

module Session = 

    open System
    open RestSharp
    open Newtonsoft.Json
    open Keybase.Response
    open Keybase.Request

    type SignupResponse = 
        {
            [<JsonProperty("status")>]Status: Status
            [<JsonProperty("csrf_token")>]CsrfToken: string
        }

    type GetSaltResponse = 
        {
            [<JsonProperty("status")>]Status: Status
            [<JsonProperty("salt")>]Salt: string
            [<JsonProperty("csrf_token")>]CsrfToken: string
            [<JsonProperty("login_session")>]LoginSession: string
        }

    type LoginResponse = 
        {
            [<JsonProperty("status")>]Status: Status
            [<JsonProperty("session")>]Session: string
            [<JsonProperty("me")>]Me: Keybase.User.User
        }

    type KillAllSessionResponse = 
        {
            [<JsonProperty("status")>]Status: Status
            [<JsonProperty("csrf_token")>]CsrfToken: string
        }

    let Signup (client : RestClient) (name : string) (email : string) (username : string) (pwh : string) (salt : string) (invitation_id : string) =
        Request.MakeRequest<SignupResponse> client "_/api/1.0/signup.json" Method.POST (fun a ->
            a.AddParameter("name", name).AddParameter("email", email).AddParameter("username", username).AddParameter("pwh", pwh).AddParameter("salt", salt).AddParameter("invitation_id", invitation_id)
        )

    let GetSalt (client : RestClient) (emailOrUsername : string) =
        Request.MakeRequest<GetSaltResponse> client "_/api/1.0/getsalt.json" Method.GET (fun a -> a.AddParameter("email_or_username", emailOrUsername))

    let Login (client : RestClient) (emailOrUsername : string) (hmac_pwh : string) (login_session : string) =
        Request.MakeRequest<LoginResponse> client "_/api/1.0/login.json" Method.POST (fun a ->
            a.AddParameter("email_or_username", emailOrUsername).AddParameter("hmac_pwh", hmac_pwh).AddParameter("login_session", login_session)
        )

    let KillAll (client : RestClient) : bool =
        let response = Request.MakeRequest<KillAllSessionResponse> client "_/api/1.0/session/killall.json" Method.POST (fun a -> a :> IRestRequest)
        false
