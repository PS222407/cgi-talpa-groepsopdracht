using System.Security.Claims;
using Auth0.AspNetCore.Authentication;
using BusinessLogicLayer.Interfaces;
using BusinessLogicLayer.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Talpa.ViewModels;

namespace Talpa.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserService _userService;

        public AccountController(IUserService userService)
        {
            _userService = userService;
        }

        public async Task Login()
        {
            var authenticationProperties = new LoginAuthenticationPropertiesBuilder()
                .WithRedirectUri(Url.Action(nameof(LoginHook)))
                .Build();

            await HttpContext.ChallengeAsync(Auth0Constants.AuthenticationScheme, authenticationProperties);
        }

        public async Task<IActionResult> LoginHook()
        {
            string? id = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            User? user = await _userService.GetByIdWithRoles(id);
            
            ClaimsIdentity? userClaims = User.Identity as ClaimsIdentity;
            foreach (Role role in user.Roles)
            {
                userClaims.AddClaim(new Claim(ClaimTypes.Role, role.Name));
            }

            HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(userClaims));
            
            return RedirectToAction(nameof(Index), "Home");
        }

        [Authorize]
        public async Task Logout()
        {
            var authenticationProperties = new LogoutAuthenticationPropertiesBuilder()
                .WithRedirectUri(Url.Action("Index", "Home"))
                .Build();

            await HttpContext.SignOutAsync(Auth0Constants.AuthenticationScheme, authenticationProperties);
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        }

        [Authorize]
        public IActionResult Profile()
        {
            return View(new UserViewModel   
            {
                Name = User.Identity.Name,
                EmailAddress = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value,
                ProfileImage = User.Claims.FirstOrDefault(c => c.Type == "picture")?.Value
            });
        }
        
        public async Task<IActionResult> Claims()
        {
            var roles = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;

            foreach (Claim claim in User.Claims)
            {
                var a = claim;
            }

            return View();
        }

        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}