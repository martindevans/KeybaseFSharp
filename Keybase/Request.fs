namespace Keybase

module Request =

    open System
    open RestSharp
    open Newtonsoft.Json

    let MakeClient () =
        new RestSharp.RestClient("https://keybase.io/")

    let internal MakeRequestRaw (client : RestClient) (url : string) (httpMethod : Method) requestModifier =
        client.Execute (requestModifier (new RestRequest(url, httpMethod)))

    let internal MakeRequest<'T> (session : RestClient) (url : string) (httpMethod : Method) requestModifier =
        let response = MakeRequestRaw session url httpMethod requestModifier
        JsonConvert.DeserializeObject<'T>(response.Content)