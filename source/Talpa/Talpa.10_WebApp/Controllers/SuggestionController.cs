using System.Security.Claims;
using BusinessLogicLayer.Exceptions;
using BusinessLogicLayer.Interfaces.Services;
using BusinessLogicLayer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Talpa.Constants;
using Talpa.RequestModels;
using Talpa.ViewModels;

namespace Talpa.Controllers;

public class SuggestionController : Controller
{
    private readonly ISuggestionService _suggestionService;
    private readonly IUserService _userService;
    private readonly IRestrictionService _restrictionService;

    public SuggestionController(ISuggestionService suggestionService, IUserService userService, IRestrictionService restrictionService)
    {
        _suggestionService = suggestionService;
        _userService = userService;
        _restrictionService = restrictionService;
    }

    //GET: Outing
    public async Task<ActionResult> Index()
    {
        string? id = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
        User? user = await _userService.GetById(id);

        if (User.IsInRole(RoleName.Admin))
        {
            List<Suggestion> allSuggestions = _suggestionService.GetAll();

            var suggestionViewModels1 = allSuggestions.Select(suggestion =>
                new SuggestionViewModel(
                    suggestion.Id,
                    suggestion.Name,
                    suggestion.Restrictions.Select(restriction => restriction.Name).ToList()
                )
            );
            foreach (var suggestion in allSuggestions)
            {
                if (suggestion.Restrictions == null)
                {
                    return View(suggestionViewModels1);

                }
            }
            var suggestionViewModels = allSuggestions.Select(suggestion =>
                new SuggestionViewModel(
                    suggestion.Id,
                    suggestion.Name,
                    suggestion.Restrictions.Select(restriction => restriction.Name).ToList()
                )
            );

            return View(suggestionViewModels);
        }

        int? teamId = user?.TeamId;
        if (teamId == null)
        {
            TempData["Message"] = "Je bent niet toegewezen aan een team.";
            TempData["MessageType"] = "danger";

            return View();
        }

        return View(_suggestionService.GetAll().Select(suggestion => new SuggestionViewModel(suggestion.Id, suggestion.Name, suggestion.Restrictions.Select(restriction => restriction.Name).ToList())));
    }

    //GET: Outing/Details/5
    [Authorize(Roles = $"{RoleName.Admin}, {RoleName.Manager}")]
    public ActionResult Details(int id)
    {
        Suggestion? suggestion = _suggestionService.GetById(id);
        if (suggestion == null)
        {
            TempData["Message"] = "Er bestaat geen entiteit met dit id.";
            TempData["MessageType"] = "danger";

            return View();
        }

        return View(new SuggestionViewModel(suggestion.Id, suggestion.Name, suggestion.Restrictions.Select(restriction => restriction.Name).ToList()));
    }

    // GET: Outing/Create
    [Authorize(Roles = $"{RoleName.Admin}, {RoleName.Manager}")]
    public ActionResult Create()
    {
        var restrictions = _restrictionService.GetAll();
        List<SelectListItem> restrictionsOptions = restrictions.Select(restriction => new SelectListItem { Value = restriction.Id.ToString(), Text = restriction.Name, }).ToList();

        SuggestionRequest suggestionRequest = new SuggestionRequest
        {
            RestrictionOptions = restrictionsOptions,
        };

        return View(suggestionRequest);
    }

    // POST: Outing/Create
    [Authorize(Roles = $"{RoleName.Admin}, {RoleName.Manager}")]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> Create(SuggestionRequest suggestionRequest)
    {

        //if (!ModelState.IsValid)
        //{
        //    return View(suggestionRequest);
        //}

        Suggestion suggestion = new() { Name = suggestionRequest.Name, Restrictions = suggestionRequest.SelectedRestrictionIds.Select(restriction => new Restriction { Name = restriction }).ToList() };
        string id = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value!;
        User user = (await _userService.GetById(id))!;

        Suggestion suggestionEntry;
        try
        {
            suggestionEntry = _suggestionService.Create(suggestion, user.Id);
        }
        catch (TeamNotFoundException e)
        {
            TempData["Message"] = "Je bent niet gekoppeld aan een geldig team.";
            TempData["MessageType"] = "danger";
            return View();
        }

        if (suggestionEntry.Id == null)
        {
            TempData["Message"] = "Fout tijdens het aanmaken.";
            TempData["MessageType"] = "danger";
            return View();
        }

        TempData["Message"] = "Item succesvol aangemaakt";
        TempData["MessageType"] = "success";

        return RedirectToAction(nameof(Index));
    }

    // GET: Outing/Edit/5
    [Authorize(Roles = $"{RoleName.Admin}, {RoleName.Manager}")]
    public ActionResult Edit(int id)
    {
        var restrictions = _restrictionService.GetAll();
        List<SelectListItem> restrictionsOptions = restrictions.Select(restriction => new SelectListItem { Value = restriction.Id.ToString(), Text = restriction.Name, }).ToList();

        Suggestion? suggestion = _suggestionService.GetById(id);
        if (suggestion == null)
        {
            TempData["Message"] = "Er bestaat geen entiteit met dit id.";
            TempData["MessageType"] = "danger";

            return View();
        }

        SuggestionRequest suggestionRequest = new SuggestionRequest { 
            Name = suggestion.Name, 
            SelectedRestrictionIds = suggestion.Restrictions.Select(restriction => restriction.Id.ToString()).ToList(),
            RestrictionOptions = restrictionsOptions
        };

        return View(suggestionRequest);
    }

    // POST: Outing/Edit/5
    [Authorize(Roles = $"{RoleName.Admin}, {RoleName.Manager}")]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Edit(int id, SuggestionRequest suggestionRequest)
    {
        if (!ModelState.IsValid)
        {
            return View();
        }

        Suggestion suggestion = new Suggestion { Id = id, Name = suggestionRequest.Name, Restrictions = suggestionRequest.SelectedRestrictionIds.Select(restriction => new Restriction() { Name = restriction }).ToList() };
        if (!_suggestionService.Update(suggestion))
        {
            TempData["Message"] = "Fout tijdens het opslaan van de data.";
            TempData["MessageType"] = "danger";

            return View();
        }

        TempData["Message"] = "Item succesvol gewijzigd";
        TempData["MessageType"] = "success";

        return RedirectToAction(nameof(Index));
    }

    // GET: Outing/Delete/5
    [Authorize(Roles = $"{RoleName.Admin}, {RoleName.Manager}")]
    public ActionResult Delete(int id)
    {
        Suggestion? suggestion = _suggestionService.GetById(id);
        if (suggestion == null)
        {
            TempData["Message"] = "Er Er bestaat geen entiteit met dit id.";
            TempData["MessageType"] = "danger";

            return View();
        }

        SuggestionViewModel suggestionViewModel = new SuggestionViewModel(id, suggestion.Name, suggestion.Restrictions.Select(restriction => restriction.Name).ToList());

        return View(suggestionViewModel);
    }

    // POST: Outing/Delete/5
    [Authorize(Roles = $"{RoleName.Admin}, {RoleName.Manager}")]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Destroy(int id)
    {
        if (!_suggestionService.Delete(id))
        {
            TempData["Message"] = "Fout tijdens het verwijderen van de data.";
            TempData["MessageType"] = "danger";

            return View("Delete");
        }

        TempData["Message"] = "Item succesvol verwijderd";
        TempData["MessageType"] = "success";

        return RedirectToAction(nameof(Index));
    }
}