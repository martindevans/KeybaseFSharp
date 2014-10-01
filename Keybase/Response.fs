namespace Keybase

module Response = 

    open System
    open RestSharp
    open Newtonsoft.Json

    type Status = 
        {
            code: int
            name: string
        }

    let internal MakeRequestRaw (url : string) (httpMethod : Method) requestModifier =
        let client = new RestClient("https://keybase.io")
        let request = requestModifier (new RestRequest(url, httpMethod))
        client.Execute request

    let internal MakeRequest<'T> (url : string) (httpMethod : Method) requestModifier =
        let response = MakeRequestRaw url httpMethod requestModifier
        JsonConvert.DeserializeObject<'T>(response.Content)
