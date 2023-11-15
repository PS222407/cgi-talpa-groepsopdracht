using System.Security.Claims;
using BusinessLogicLayer.Exceptions;
using BusinessLogicLayer.Interfaces.Services;
using BusinessLogicLayer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Localization;
using Talpa_10_WebApp.Constants;
using Talpa_10_WebApp.RequestModels;
using Talpa_10_WebApp.Translations;
using Talpa_10_WebApp.ViewModels;

namespace Talpa_10_WebApp.Controllers;

public class SuggestionController : Controller
{
    private readonly ISuggestionService _suggestionService;

    private readonly IUserService _userService;

    private readonly IRestrictionService _restrictionService;

    private readonly Shared _localizer;

    public SuggestionController(ISuggestionService suggestionService, IUserService userService, IRestrictionService restrictionService, IStringLocalizer<Shared> localizer)
    {
        _suggestionService = suggestionService;
        _userService = userService;
        _restrictionService = restrictionService;
        _localizer = new Shared(localizer);
    }

    public async Task<ActionResult> Index()
    {
        string? id = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
        User? user = await _userService.GetById(id);

        int? teamId = user?.TeamId;
        if (teamId == null)
        {
            TempData["Message"] = _localizer.Get("You are not assigned to a team");
            TempData["MessageType"] = "danger";

            return View(new List<SuggestionViewModel>());
        }

        return View(_suggestionService.GetAllByUserId(id)
            .Select(suggestion => new SuggestionViewModel(
                suggestion.Id,
                suggestion.Name,
                suggestion.Restrictions?.Select(restriction => restriction.Name).ToList() ?? new List<string>())));
    }

    [Authorize(Roles = $"{RoleName.Admin}, {RoleName.Manager}, {RoleName.Employee}")]
    public ActionResult Create()
    {
        List<Restriction> restrictions = _restrictionService.GetAll();
        List<SelectListItem> restrictionsOptions = restrictions.Select(restriction => new SelectListItem { Value = restriction.Id.ToString(), Text = restriction.Name }).ToList();

        SuggestionRequest suggestionRequest = new()
        {
            RestrictionOptions = restrictionsOptions,
        };

        return View(suggestionRequest);
    }

    [Authorize(Roles = $"{RoleName.Admin}, {RoleName.Manager}, {RoleName.Employee}")]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> Create(SuggestionRequest suggestionRequest)
    {
        if (!ModelState.IsValid)
        {
            return View(suggestionRequest);
        }

        Suggestion suggestion = new()
        {
            Name = suggestionRequest.Name,
            Description = suggestionRequest.Description,
            Restrictions = suggestionRequest.SelectedRestrictionIds?.Select(restriction => new Restriction { Name = restriction }).ToList(),
        };
        string id = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value!;
        User user = (await _userService.GetById(id))!;

        Suggestion suggestionEntry;
        try
        {
            suggestionEntry = _suggestionService.Create(suggestion, user.Id);
        }
        catch (TeamNotFoundException)
        {
            TempData["Message"] = _localizer.Get("You are not assigned to a team");
            TempData["MessageType"] = "danger";
            return View();
        }

        if (suggestionEntry.Id == null)
        {
            TempData["Message"] = _localizer.Get("Error while creating");
            TempData["MessageType"] = "danger";
            return View();
        }

        TempData["Message"] = _localizer.Get("Item successfully created");
        TempData["MessageType"] = "success";

        return RedirectToAction(nameof(Index));
    }

    // GET: Outing/Edit/5
    [Authorize(Roles = $"{RoleName.Admin}, {RoleName.Manager}, {RoleName.Employee}")]
    public ActionResult Edit(int id)
    {
        List<Restriction> restrictions = _restrictionService.GetAll();
        List<SelectListItem> restrictionsOptions = restrictions.Select(restriction => new SelectListItem
        {
            Value = restriction.Id.ToString(), Text = restriction.Name,
        }).ToList();

        string userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value!;

        Suggestion? suggestion = _suggestionService.GetById(id, userId);
        if (suggestion == null)
        {
            TempData["Message"] = _localizer.Get("No entity found with this id");
            TempData["MessageType"] = "danger";

            return View();
        }

        SuggestionRequest suggestionRequest = new()
        {
            Name = suggestion.Name,
            Description = suggestion.Description,
            SelectedRestrictionIds = suggestion.Restrictions?.Select(restriction => restriction.Id.ToString()).ToList(),
            RestrictionOptions = restrictionsOptions,
        };

        return View(suggestionRequest);
    }

    // POST: Outing/Edit/5
    [Authorize(Roles = $"{RoleName.Admin}, {RoleName.Manager}, {RoleName.Employee}")]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Edit(int id, SuggestionRequest suggestionRequest)
    {
        if (!ModelState.IsValid)
        {
            suggestionRequest.RestrictionOptions = _restrictionService.GetAll().Select(restriction => new SelectListItem
            {
                Value = restriction.Id.ToString(), Text = restriction.Name,
            }).ToList();

            return View(suggestionRequest);
        }

        Suggestion suggestion = new()
        {
            Id = id,
            Name = suggestionRequest.Name,
            Description = suggestionRequest.Description,
            Restrictions = suggestionRequest.SelectedRestrictionIds?.Select(restriction => new Restriction
            {
                Name = restriction,
            }).ToList(),
        };

        string userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value!;

        if (!_suggestionService.Update(suggestion, userId))
        {
            TempData["Message"] = _localizer.Get("Error while updating");
            TempData["MessageType"] = "danger";
            
            suggestionRequest.RestrictionOptions = _restrictionService.GetAll().Select(restriction => new SelectListItem
            {
                Value = restriction.Id.ToString(), Text = restriction.Name,
            }).ToList();

            return View(suggestionRequest);
        }

        TempData["Message"] = _localizer.Get("Item successfully updated");
        TempData["MessageType"] = "success";

        return RedirectToAction(nameof(Index));
    }

    // GET: Outing/Delete/5
    [Authorize(Roles = $"{RoleName.Admin}, {RoleName.Manager}, {RoleName.Employee}")]
    public ActionResult Delete(int id)
    {
        string userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value!;
        Suggestion? suggestion = _suggestionService.GetById(id, userId);
        if (suggestion == null)
        {
            TempData["Message"] = _localizer.Get("No entity found with this id");
            TempData["MessageType"] = "danger";

            return View();
        }

        SuggestionViewModel suggestionViewModel = new(id, suggestion.Name, suggestion.Restrictions?.Select(restriction => restriction.Name).ToList() ?? new List<string>());

        return View(suggestionViewModel);
    }

    // POST: Outing/Delete/5
    [Authorize(Roles = $"{RoleName.Admin}, {RoleName.Manager}, {RoleName.Employee}")]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Destroy(int id)
    {
        string userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value!;

        if (!_suggestionService.Delete(id, userId))
        {
            TempData["Message"] = _localizer.Get("Error while deleting");
            TempData["MessageType"] = "danger";

            return View("Delete");
        }

        TempData["Message"] = _localizer.Get("Item successfully deleted");
        TempData["MessageType"] = "success";

        return RedirectToAction(nameof(Index));
    }
}