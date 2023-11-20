using System.Diagnostics;
using BusinessLogicLayer.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Talpa_10_WebApp.Translations;
using Talpa_10_WebApp.ViewModels;
using BusinessLogicLayer.Interfaces.Services;

namespace Talpa_10_WebApp.Controllers;

[Authorize]
public class HomeController : Controller
{
    private readonly IOutingService _outingService;

    private readonly IUserService _userService;

    private readonly IStringLocalizer<Shared> _localizer;

    public HomeController(IStringLocalizer<Shared> localizer, IUserService userService, IOutingService outingService)
    {
        _localizer = localizer;
        _userService = userService;
        _outingService = outingService;
    }

    public async Task<ActionResult> Index()
    {
        // string frogTranslated = new Shared(_localizer).Get("Frog");
        string? id = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
        User? user = await _userService.GetById(id);
        
        int? teamId = user?.TeamId;
        if (teamId == null)
        {
            TempData["Message"] = new Shared(_localizer).Get("You are not assigned to a team");
            TempData["MessageType"] = "danger";

            return View(new List<OutingViewModel>());
        }

        return View(_outingService.GetAllConfirmedFromTeam((int)teamId).Select(outing => new OutingViewModel(outing.Id, outing.Name)
        {
            ConfirmedSuggestion = outing.ConfirmedSuggestion,
            ConfirmedOutingDate = outing.ConfirmedOutingDate,
            ImageUrl = outing.ImageUrl,
        }).OrderBy(o => o.ConfirmedOutingDate?.Date).ToList());
    }

    [HttpGet("TestError")]
    public IActionResult TestError()
    {
        throw new NullReferenceException();
        return Ok();
    }

    [HttpGet("SetLocale")]
    public ActionResult SetLocale(string locale)
    {
        try
        {
            locale = locale switch
            {
                "nl" => "nl-NL",
                "us" => "en-US",
                _ => locale,
            };

            Response.Cookies.Append(
                CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(locale)),
                new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(100) }
            );
        }
        catch (Exception)
        {
            return BadRequest();
        }

        return Json(new { message = "Success" });
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}