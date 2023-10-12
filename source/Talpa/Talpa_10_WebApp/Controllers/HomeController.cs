using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Talpa_10_WebApp.ViewModels;

namespace Talpa_10_WebApp.Controllers;

[Authorize]
public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        return View();
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
                _ => locale
            };

            Response.Cookies.Append(
                CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(locale)),
                new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(100) }
            );
        }
        catch (Exception e)
        {
            return Redirect(Request.Headers.Referer.ToString());
        }

        return Redirect(Request.Headers.Referer.ToString());
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}