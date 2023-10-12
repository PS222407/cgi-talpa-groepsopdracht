namespace DataAccessLayer;

public static class Auth0
{
    public static string? AccessToken;

    public static readonly string Domain = "dev-2djpjw5qr1fcoxc2.eu.auth0.com";

    public static readonly string BaseUrl = "https://dev-2djpjw5qr1fcoxc2.eu.auth0.com/api/v2";

    public static readonly string GetUserEndpoint = "/users";

    public static readonly string OauthTokenEndpoint = "/oauth/token";
}