using System.Globalization;
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

public class OutingController : Controller
{
    private readonly IOutingService _outingService;
    private readonly IUserService _userService;
    private readonly ISuggestionService _suggestionService;

    public OutingController(IOutingService outingService, IUserService userService, ISuggestionService suggestionService)
    {
        _outingService = outingService;
        _userService = userService;
        _suggestionService = suggestionService;
    }

    // GET: Outing
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

    // GET: Outing/Details/5
    [Authorize(Roles = $"{RoleName.Admin}, {RoleName.Manager}")]
    public ActionResult Details(int id)
    {
        Outing? outing = _outingService.GetById(id);
        if (outing == null)
        {
            TempData["Message"] = "Er bestaat geen entiteit met dit id.";
            TempData["MessageType"] = "danger";

            return View();
        }

        return View(new OutingViewModel(outing.Id, outing.Name));
    }

    // GET: Outing/Create
    [Authorize(Roles = $"{RoleName.Admin}, {RoleName.Manager}")]
    public ActionResult Create()
    {
        List<Suggestion> suggestions = _suggestionService.GetAll();
        List<SelectListItem> suggestionOptions = suggestions.Select(suggestion => new SelectListItem { Value = suggestion.Id.ToString(), Text = suggestion.Name, }).ToList();

        OutingRequest outingRequest = new()
        {
            SuggestionOptions = suggestionOptions
        };
        
        return View(outingRequest);
    }

    // POST: Outing/Create
    [Authorize(Roles = $"{RoleName.Admin}, {RoleName.Manager}")]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> Create(OutingRequest outingRequest)
    {
        if (!ModelState.IsValid)
        {
            return View(outingRequest);
        }
        
        List<OutingDate> outingDates = outingRequest.Dates.Select(date => new OutingDate { Date = date }).ToList();
        Outing outing = new() { Name = outingRequest.Name, OutingDates = outingDates };
        
        string id = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value!;
        User user = (await _userService.GetById(id))!;
        
        Outing outingEntry;
        try
        {
            outingEntry = _outingService.Create(outing, (int)user.TeamId!);
        }
        catch (TeamNotFoundException e)
        {
            TempData["Message"] = "Je bent niet gekoppeld aan een geldig team.";
            TempData["MessageType"] = "danger";
            return View();
        }
        
        if (outingEntry.Id == null)
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
        Outing? outing = _outingService.GetById(id);
        if (outing == null)
        {
            TempData["Message"] = "Er bestaat geen entiteit met dit id.";
            TempData["MessageType"] = "danger";

            return View();
        }

        List<Suggestion> suggestions = _suggestionService.GetAll();
        List<SelectListItem> suggestionOptions = suggestions.Select(suggestion => new SelectListItem { Value = suggestion.Id.ToString(), Text = suggestion.Name, }).ToList();

        OutingViewModel outingViewModel = new()
        {
            Id = outing.Id,
            Name = outing.Name,
            SuggestionOptions = suggestionOptions,
            SelectedSuggestionIds = new List<string>(), // TODO: Get selected suggestions
            OutingDates = outing.OutingDates,
        };

        return View(outingViewModel);
    }

    // POST: Outing/Edit/5
    [Authorize(Roles = $"{RoleName.Admin}, {RoleName.Manager}")]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Edit(int id, OutingRequest outingRequest)
    {
        if (!ModelState.IsValid)
        {
            return View();
        }

        Outing outing = new() { Id = id, Name = outingRequest.Name };
        if (!_outingService.Update(outing))
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
        Outing? outing = _outingService.GetById(id);
        if (outing == null)
        {
            TempData["Message"] = "Er Er bestaat geen entiteit met dit id.";
            TempData["MessageType"] = "danger";

            return View();
        }

        OutingViewModel outingViewModel = new OutingViewModel(id, outing.Name);

        return View(outingViewModel);
    }

    // POST: Outing/Delete/5
    [Authorize(Roles = $"{RoleName.Admin}, {RoleName.Manager}")]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Destroy(int id)
    {
        if (!_outingService.Delete(id))
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