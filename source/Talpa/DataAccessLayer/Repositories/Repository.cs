using System.Net.Http.Headers;
using System.Text.Json;
using DataAccessLayer.Dtos;

namespace DataAccessLayer.Repositories;

public class Repository
{
    private readonly string _clientId;
    
    private readonly string _clientSecret;

    protected Repository(string clientId, string clientSecret)
    {
        _clientId = clientId;
        _clientSecret = clientSecret;
    }

    protected async Task<string?> GetAccessToken()
    {
        bool validAccessTokenExists = Auth0.AccessToken != null && DateTime.Now.AddMinutes(1) < JwtService.GetTokenExpirationDate(Auth0.AccessToken);
        if (validAccessTokenExists)
        {
            return Auth0.AccessToken;
        }
        
        using HttpClient httpClient = new HttpClient();
        
        string url = $"https://{Auth0.Domain}{Auth0.OauthTokenEndpoint}";
        string audience = $"https://{Auth0.Domain}/api/v2/";
        
        var postData = new FormUrlEncodedContent(new[]
        {
            new KeyValuePair<string, string>("grant_type", "client_credentials"),
            new KeyValuePair<string, string>("client_id", _clientId),
            new KeyValuePair<string, string>("client_secret", _clientSecret),
            new KeyValuePair<string, string>("audience", audience)
        });
        postData.Headers.ContentType = new MediaTypeHeaderValue("application/x-www-form-urlencoded");
    
        HttpResponseMessage response = await httpClient.PostAsync(url, postData);
        string status = response.Content.ReadAsStringAsync().Result;
        TokenDto? tokenDto = JsonSerializer.Deserialize<TokenDto>(status);
        Auth0.AccessToken = tokenDto?.access_token;

        return tokenDto?.access_token;
    }
}
