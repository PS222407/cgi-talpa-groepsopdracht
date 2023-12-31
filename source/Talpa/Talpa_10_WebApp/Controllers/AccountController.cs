﻿using System.Security.Claims;
using Auth0.AspNetCore.Authentication;
using BusinessLogicLayer.Interfaces.Services;
using BusinessLogicLayer.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Talpa_10_WebApp.Controllers;

public class AccountController : Controller
{
    private readonly IUserService _userService;

    public AccountController(IUserService userService)
    {
        _userService = userService;
    }

    public async Task Login()
    {
        bool appIsInProduction = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Production";
        string callbackUrl = Url.Action(nameof(LoginHook), null, null, "http")!;
        AuthenticationProperties authenticationProperties = new LoginAuthenticationPropertiesBuilder()
            .WithRedirectUri(callbackUrl)
            .Build();

        await HttpContext.ChallengeAsync(Auth0Constants.AuthenticationScheme, authenticationProperties);
    }

    public async Task<IActionResult> LoginHook()
    {
        string? id = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
        User? user = await _userService.GetByIdWithRoles(id!);

        ClaimsIdentity? userClaims = User.Identity as ClaimsIdentity;
        if (user?.Roles != null && userClaims != null)
        {
            foreach (Role role in user.Roles)
            {
                userClaims.AddClaim(new Claim(ClaimTypes.Role, role.Name));
            }

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(userClaims));
        }

        return RedirectToAction(nameof(Index), "Home");
    }

    [Authorize]
    public async Task Logout()
    {
        AuthenticationProperties authenticationProperties = new LogoutAuthenticationPropertiesBuilder()
            .WithRedirectUri(Url.Action("Index", "Home")!)
            .Build();

        await HttpContext.SignOutAsync(Auth0Constants.AuthenticationScheme, authenticationProperties);
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
    }

    // [Authorize]
    // public IActionResult Profile()
    // {
    //     return View(new UserViewModel
    //     {
    //         Name = User.Identity?.Name,
    //         EmailAddress = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value,
    //         ProfileImage = User.Claims.FirstOrDefault(c => c.Type == "picture")?.Value,
    //     });
    // }

    // [Authorize]
    // public IActionResult Claims()
    // {
    //     string? roles = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;
    //
    //     foreach (Claim claim in User.Claims)
    //     {
    //         Claim a = claim;
    //     }
    //
    //     return View();
    // }

    public IActionResult AccessDenied()
    {
        return View();
    }
}