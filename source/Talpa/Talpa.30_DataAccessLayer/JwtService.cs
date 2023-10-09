using System.IdentityModel.Tokens.Jwt;

namespace DataAccessLayer;

public static class JwtService
{
    public static DateTime? GetTokenExpirationDate(string token)
    {
        if (string.IsNullOrEmpty(token))
        {
            return null;
        }

        try
        {
            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
            JwtSecurityToken jwtToken = handler.ReadJwtToken(token);

            if (jwtToken.ValidTo != DateTime.MinValue)
            {
                return jwtToken.ValidTo;
            }
        }
        catch (Exception e)
        {
            Console.WriteLine($"JWT token error: {e.Message}");
        }

        return null;
    }
}
