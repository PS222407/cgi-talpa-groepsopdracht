namespace DataAccessLayer;

public static class Auth0
{
    public static string? AccessToken;

    public static readonly string GetUserEndpoint = "/users";

    public static readonly string OauthTokenEndpoint = "/oauth/token";
}