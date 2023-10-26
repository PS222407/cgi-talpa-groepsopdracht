using System.Net;
using System.Net.Http.Headers;
using System.Text.Json;
using BusinessLogicLayer.Exceptions;

namespace DataAccessLayer.Repositories;

public class Repository
{
    private readonly string _domain;

    private readonly string _apiClientId;

    private readonly string _apiClientSecret;

    protected Repository(string clientId, string clientSecret, string domain, string apiClientId, string apiClientSecret)
    {
        _domain = domain;
        _apiClientId = apiClientId;
        _apiClientSecret = apiClientSecret;
    }

    protected async Task<string?> GetAccessToken()
    {
        bool validAccessTokenExists = Auth0.AccessToken != null && DateTime.Now.AddMinutes(1) < JwtService.GetTokenExpirationDate(Auth0.AccessToken);
        if (validAccessTokenExists)
        {
            return Auth0.AccessToken;
        }

        using HttpClient httpClient = new();

        string url = $"https://{_domain}{Auth0.OauthTokenEndpoint}";
        string audience = $"https://{_domain}/api/v2/";

        FormUrlEncodedContent? postData = new(new[]
        {
            new KeyValuePair<string, string>("grant_type", "client_credentials"),
            new KeyValuePair<string, string>("client_id", _apiClientId),
            new KeyValuePair<string, string>("client_secret", _apiClientSecret),
            new KeyValuePair<string, string>("audience", audience),
        });
        postData.Headers.ContentType = new MediaTypeHeaderValue("application/x-www-form-urlencoded");

        HttpResponseMessage response = await httpClient.PostAsync(url, postData);
        if (response.StatusCode == HttpStatusCode.Forbidden)
        {
            throw new Auth0ForbiddenException();
        }
        
        string status = response.Content.ReadAsStringAsync().Result;
        Token? token = JsonSerializer.Deserialize<Token>(status);
        Auth0.AccessToken = token?.access_token;

        return token?.access_token;
    }
}