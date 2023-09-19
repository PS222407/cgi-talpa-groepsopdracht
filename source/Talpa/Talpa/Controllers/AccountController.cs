using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using Auth0.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Talpa.ViewModels;

namespace Talpa.Controllers
{
    public class AccountController : Controller
    {
        public async Task Login(string returnUrl = "/")
        {
            var authenticationProperties = new LoginAuthenticationPropertiesBuilder()
                .WithRedirectUri(returnUrl)
                .Build();

            await HttpContext.ChallengeAsync(Auth0Constants.AuthenticationScheme, authenticationProperties);
        }

        [Authorize]
        public async Task Logout()
        {
            var authenticationProperties = new LogoutAuthenticationPropertiesBuilder()
                // Indicate here where Auth0 should redirect the user after a logout.
                // Note that the resulting absolute Uri must be whitelisted in 
                .WithRedirectUri(Url.Action("Index", "Home"))
                .Build();

            await HttpContext.SignOutAsync(Auth0Constants.AuthenticationScheme, authenticationProperties);
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        }

        [Authorize]
        public IActionResult Profile()
        {
            return View(new UserViewModel()
            {
                Name = User.Identity.Name,
                EmailAddress = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value,
                ProfileImage = User.Claims.FirstOrDefault(c => c.Type == "picture")?.Value
            });
        }

        
        private HttpClient _httpClient;
        
        private const string BaseUrl = "https://dev-2djpjw5qr1fcoxc2.eu.auth0.com/api/v2";
        private const string Auth0TokenEndpoint = "/oauth/token";
        private const string GetUserEndpoint = "/users";
        private const string ClientId = "";
        private const string ClientSecret = "";
        private const string Audience = "https://dev-2djpjw5qr1fcoxc2.eu.auth0.com/api/v2/";

        /// <summary>
        /// This is just a helper action to enable you to easily see all claims related to a user. It helps when debugging your
        /// application to see the in claims populated from the Auth0 ID Token
        /// </summary>
        /// <returns></returns>
        [Authorize]
        public IActionResult Claims()
        {
            // using (var httpClient = new HttpClient())
            // {
            //     var requestContent = new StringContent(
            //         $"{{\"client_id\":\"{ClientId}\",\"client_secret\":\"{ClientSecret}\",\"audience\":\"{Audience}\",\"grant_type\":\"client_credentials\"}}",
            //         Encoding.UTF8,
            //         "application/json");
            //
            //     var response = await httpClient.PostAsync(Auth0TokenEndpoint, requestContent);
            //
            //     if (response.IsSuccessStatusCode)
            //     {
            //         var responseContent = await response.Content.ReadAsStringAsync();
            //     }
            //     else
            //     {
            //         // Handle the error response here
            //         throw new HttpRequestException($"Error: {response.StatusCode}");
            //     }
            // }
            
            foreach (Claim claim in User.Claims)
            {
                var a = claim;
            }

            var id = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            
            return View();
        }

        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}
