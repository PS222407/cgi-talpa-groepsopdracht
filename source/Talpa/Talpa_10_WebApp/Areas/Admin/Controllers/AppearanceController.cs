using BusinessLogicLayer.Interfaces.Services;
using BusinessLogicLayer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Talpa_10_WebApp.Constants;
using Talpa_10_WebApp.RequestModels;
using Talpa_10_WebApp.Services;

namespace Talpa_10_WebApp.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = $"{RoleName.Admin}")]
public class AppearanceController : Controller
{
    private readonly IAppearanceService _appearanceService;

    private readonly FileService _fileService;

    private readonly IWebHostEnvironment _webHostEnvironment;

    public AppearanceController(IAppearanceService appearanceService, FileService fileService, IWebHostEnvironment webHostEnvironment)
    {
        _appearanceService = appearanceService;
        _fileService = fileService;
        _webHostEnvironment = webHostEnvironment;
    }

    public IActionResult Index()
    {
        Appearance? appearance = _appearanceService.Get();

        AppearanceRequest appearanceRequest = new();
        if (appearance != null)
        {
            appearanceRequest.Main = appearance.Main;
            appearanceRequest.Secondary = appearance.Secondary;
            appearanceRequest.Background = appearance.Background;
            appearanceRequest.ImageUrl = appearance.ImageUrl;
        }

        return View(appearanceRequest);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> Edit(AppearanceRequest appearanceRequest)
    {
        string imageUrl;
        if (appearanceRequest.Image != null)
        {
            imageUrl = await _fileService.SaveImageAsync(appearanceRequest.Image, _webHostEnvironment) ?? "";
        }
        else
        {
            imageUrl = _appearanceService.Get()?.ImageUrl ?? "";
        }

        Appearance appearance = new()
        {
            Main = appearanceRequest.Main,
            Secondary = appearanceRequest.Secondary,
            Background = appearanceRequest.Background,
            ImageUrl = imageUrl,
        };
        _appearanceService.Update(appearance);
        
        SetCss(appearance.Main, appearance.Secondary, appearance.Background);

        return RedirectToAction(nameof(Index));
    }
    
    private void SetCss(string main, string secondary, string background) {
        
        string cssPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "css", "appearance.css");
        
        try
        {
            using StreamWriter writer = new(cssPath);

            string css = $":root {{" +
                         $"\n    --main: {main};" +
                         $"\n    --backgroundcolor: {background};" +
                         $"\n    --white: {secondary};" +
                         $"\n}}";
            
            writer.Write(css);

            writer.Close();
        }
        catch (Exception)
        {
            // ignored
        }
    }
}