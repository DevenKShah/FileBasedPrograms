#:package Newtonsoft.Json@13.0.1

using Newtonsoft.Json;


Console.WriteLine(GetData(GetToken(args[0], args[1])));

string GetToken(string clientId, string clientSecret)
{
    var url = "https://login.microsoftonline.com/fcsazuresfabisgov.onmicrosoft.com/oauth2/token";

    var formContent = new FormUrlEncodedContent(new[]
    {
        new KeyValuePair<string, string>("client_id", clientId),
        new KeyValuePair<string, string>("client_secret", clientSecret),
        new KeyValuePair<string, string>("resource", "https://fcsazuresfabisgov.onmicrosoft.com/das-roatpservice-api"),
        new KeyValuePair<string, string>("grant_type", "client_credentials")
    });
    using (var httpClient = new HttpClient())
    {
        var response = httpClient.PostAsync(url, formContent).Result;
        // var responseContent = response.Content.ReadAsStringAsync().Result;
        // return responseContent;

        response.EnsureSuccessStatusCode();

        dynamic jsonObject = JsonConvert.DeserializeObject<dynamic>(response.Content.ReadAsStringAsync().Result)!;
        return jsonObject.access_token;
    }
}

string GetData(string token)
{
    var url = "https://providers-api.apprenticeships.education.gov.uk/api/v1/Organisation/engagements?sinceEventId=37474&pageSize=100&pageNumber=1";
    using (var httpClient = new HttpClient())
    {
        httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
        var response = httpClient.GetAsync(url).Result;
        // return response.StatusCode.ToString();
        var responseContent = response.Content.ReadAsStringAsync().Result;
        return responseContent;
    }
}