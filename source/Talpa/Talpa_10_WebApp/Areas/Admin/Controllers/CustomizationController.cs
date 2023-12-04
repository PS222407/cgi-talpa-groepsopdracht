using BusinessLogicLayer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Talpa_10_WebApp.Constants;
using Talpa_10_WebApp.RequestModels;
using Talpa_10_WebApp.Translations;
using Talpa_10_WebApp.ViewModels;

namespace Talpa_10_WebApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = $"{RoleName.Admin}")]
    public class CustomizationController : Controller
    {
        private readonly Shared _localizer;

        public CustomizationController(IStringLocalizer<Shared> localizer) 
        {
            _localizer = new Shared(localizer);
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, CustomizationRequest customizationRequest)
        {
            if (!ModelState.IsValid)
            {
                return View(customizationRequest);
            }

            TempData["Message"] = _localizer.Get("Item successfully updated");
            TempData["MessageType"] = "success";

            return RedirectToAction(nameof(Index));
        }
    }
}
