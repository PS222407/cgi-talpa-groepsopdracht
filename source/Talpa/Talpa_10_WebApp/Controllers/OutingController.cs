using System.Security.Claims;
using BusinessLogicLayer.Interfaces.Services;
using BusinessLogicLayer.Models;
using Microsoft.AspNetCore.Mvc;
using Talpa_10_WebApp.Constants;
using Talpa_10_WebApp.ViewModels;

namespace Talpa_10_WebApp.Controllers;

public class OutingController : Controller
{
    private readonly IOutingService _outingService;

    private readonly IUserService _userService;

    public OutingController(IOutingService outingService, IUserService userService)
    {
        _outingService = outingService;
        _userService = userService;
    }

    public async Task<ActionResult> Index()
    {
        string? id = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
        User? user = await _userService.GetById(id);

        if (User.IsInRole(RoleName.Admin))
        {
            return View(_outingService.GetAll().Select(outing => new OutingViewModel(outing.Id, outing.Name)).ToList());
        }

        int? teamId = user?.TeamId;
        if (teamId == null)
        {
            TempData["Message"] = "Je bent niet toegewezen aan een team.";
            TempData["MessageType"] = "danger";

            return View();
        }

        return View(_outingService.GetAllFromTeam((int)teamId).Select(outing => new OutingViewModel(outing.Id, outing.Name)).ToList());
    }

    [HttpGet("Outing/Details/{id:int}")]
    public ActionResult ChooseSuggestion(int id)
    {
        Outing? outing = _outingService.GetById(id);
        
        if (outing == null)
        {
            TempData["Message"] = "Dit uitje bestaat niet.";
            TempData["MessageType"] = "danger";

            return RedirectToAction("Index");
        }

        OutingViewModel outingViewModel = new()
        {
            Id = outing.Id,
            Name = outing.Name,
            Suggestions = outing.Suggestions,
        };
        
        return View(outingViewModel);
    }
}