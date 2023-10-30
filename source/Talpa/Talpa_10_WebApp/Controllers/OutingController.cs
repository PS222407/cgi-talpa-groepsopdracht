using System.Security.Claims;
using BusinessLogicLayer.Interfaces.Services;
using BusinessLogicLayer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Talpa_10_WebApp.Constants;
using Talpa_10_WebApp.RequestModels;
using Talpa_10_WebApp.Translations;
using Talpa_10_WebApp.ViewModels;

namespace Talpa_10_WebApp.Controllers;

public class OutingController : Controller
{
    private readonly IOutingService _outingService;

    private readonly IUserService _userService;

    private readonly Shared _localizer;

    public OutingController(IOutingService outingService, IUserService userService, IStringLocalizer<Shared> localizer)
    {
        _outingService = outingService;
        _userService = userService;
        _localizer = new Shared(localizer);
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
            TempData["Message"] = _localizer.Get("You are not assigned to a team");
            TempData["MessageType"] = "danger";

            return View(new List<OutingViewModel>());
        }

        return View(_outingService.GetAllFromTeam((int)teamId).Select(outing => new OutingViewModel(outing.Id, outing.Name)).ToList());
    }

    [HttpGet("Outing/Details/{id:int}")]
    public ActionResult ChooseSuggestion(int id)
    {
        Outing? outing = _outingService.GetById(id);

        if (outing == null)
        {
            TempData["Message"] = _localizer.Get("Outing does not exist");
            TempData["MessageType"] = "danger";

            return RedirectToAction("Index");
        }

        List<SuggestionViewModel> suggestionViewModels = new();
        foreach (Suggestion suggestion in outing.Suggestions ?? new List<Suggestion>())
        {
            suggestionViewModels.Add(new SuggestionViewModel
            {
                Name = suggestion.Name,
                Restrictions = suggestion.Restrictions?.Select(restriction => restriction.Name).ToList() ?? new List<string>(),
            });
        }

        OutingViewModel outingViewModel = new()
        {
            Id = outing.Id,
            Name = outing.Name,
            Suggestions = suggestionViewModels,
        };

        return View(outingViewModel);
    }

    public ActionResult VoteSuggestion(int id)
    {
        Outing? outing = _outingService.GetById(id);
        if (outing == null)
        {
            TempData["Message"] = "Er bestaat geen entiteit met dit id.";
            TempData["MessageType"] = "danger";

            return View();
        }

        List<Suggestion> suggestions = outing.Suggestions;

        var suggestionViewModels = suggestions.Select(suggestion => new SuggestionViewModel(
            suggestion.Id,
            suggestion.Name,
            suggestion.Restrictions.Select(restriction => restriction.Name).ToList()
        )).ToList();
        return View(new OutingRequest(outing.Id, outing.Name, suggestionViewModels, outing.OutingDates.Select(dates => dates.Date).ToList()));
    }

    [HttpPost]
    public ActionResult VoteSuggestion(OutingRequest outingRequest)
    {
        return View();
    }
}