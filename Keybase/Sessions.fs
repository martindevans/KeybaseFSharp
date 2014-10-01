namespace Keybase

module Session = 

    open System
    open RestSharp
    open Newtonsoft.Json
    open Keybase.Response

    type SignupResponse = 
        {
            status: Status
            csrf_token: string
        }

    type GetSaltResponse = 
        {
            status: Status
            salt: string
            csrf_token: string
            login_session: string
        }

    type LoginResponse = 
        {
            status: Status
            session: string
            me: string
        }

    let Signup (name : string) (email : string) (username : string) (pwh : string) (salt : string) (invitation_id : string) =
        Response.MakeRequest<SignupResponse> "_/api/1.0/signup.json" Method.POST (fun a ->
            a.AddParameter("name", name).AddParameter("email", email).AddParameter("username", username).AddParameter("pwh", pwh).AddParameter("salt", salt).AddParameter("invitation_id", invitation_id)
        )

    let GetSalt (emailOrUsername : string) =
        Response.MakeRequest<GetSaltResponse> "_/api/1.0/getsalt.json" Method.GET (fun a -> a.AddParameter("email_or_username", emailOrUsername))

    let Login (emailOrUsername : string) (hmac_pwh : string) (login_session : string) =
        Response.MakeRequest<LoginResponse> "_/api/1.0/login.json" Method.POST (fun a ->
            a.AddParameter("email_or_username", emailOrUsername).AddParameter("hmac_pwh", hmac_pwh).AddParameter("login_session", login_session)
        )
