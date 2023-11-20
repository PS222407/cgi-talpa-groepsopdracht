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
using Talpa_10_WebApp.Services;
using Talpa_10_WebApp.Translations;
using Talpa_10_WebApp.ViewModels;

namespace Talpa_10_WebApp.Areas.Manager.Controllers;

[Area("Manager")]
[Route("Manager/[controller]/[action]/{id?}")]
[Authorize(Roles = $"{RoleName.Admin}, {RoleName.Manager}")]
public class OutingController : Controller
{
    private readonly IOutingService _outingService;

    private readonly IUserService _userService;

    private readonly ISuggestionService _suggestionService;

    private readonly IWebHostEnvironment _webHostEnvironment;

    private readonly Shared _localizer;

    private readonly FileService _fileService;

    public OutingController(
        IOutingService outingService,
        IUserService userService,
        ISuggestionService suggestionService,
        IStringLocalizer<Shared> localizer,
        FileService fileService,
        IWebHostEnvironment webHostEnvironment
    )
    {
        _outingService = outingService;
        _userService = userService;
        _fileService = fileService;
        _suggestionService = suggestionService;
        _localizer = new Shared(localizer);
        _webHostEnvironment = webHostEnvironment;
    }

    // GET: Outing
    [HttpGet("/Manager/[controller]")]
    public async Task<ActionResult> Index()
    {
        string? id = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
        User? user = await _userService.GetById(id!);

        if (User.IsInRole(RoleName.Admin))
        {
            return View(_outingService.GetAll().Select(outing => new OutingViewModel(outing.Id, outing.Name)).ToList());
        }

        int? teamId = user?.TeamId;
        if (teamId == null)
        {
            TempData["Message"] = _localizer.Get("You are not assigned to a team");
            TempData["MessageType"] = "danger";

            return View();
        }

        return View(_outingService.GetAllFromTeam((int)teamId).Select(outing => new OutingViewModel(outing.Id, outing.Name)).ToList());
    }

    // GET: Outing/Details/5
    public ActionResult Details(int id)
    {
        Outing? outing = _outingService.GetOutingByIdWithMostVotedDatesAndSuggestions(id);
        if (outing == null)
        {
            TempData["Message"] = _localizer.Get("There are no votes for this outing");
            TempData["MessageType"] = "danger";

            return RedirectToAction(nameof(Index));
        }

        ConfirmOutingRequest confirmOutingRequest = new()
        {
            Name = outing.Name,
            OutingDates = outing.OutingDates,
            Suggestions = outing.Suggestions.Select(s => new SuggestionViewModel
            {
                Id = s.Id,
                Name = s.Name,
            }).ToList(),
            OutingDateVoteCount = outing.OutingDateVoteCount,
            SuggestionVoteCount = outing.SuggestionVoteCount,
            SuggestionId = outing.ConfirmedSuggestionId,
            OutingDateId = outing.ConfirmedOutingDateId,
        };

        return View(confirmOutingRequest);
    }

    [HttpPost]
    public ActionResult Details(int id, ConfirmOutingRequest confirmOutingRequest)
    {
        if (!ModelState.IsValid)
        {
            return View(confirmOutingRequest);
        }

        Outing? outing = _outingService.GetById(id);

        if (outing == null)
        {
            TempData["Message"] = _localizer.Get("No entity found with this id");
            TempData["MessageType"] = "danger";

            return View(confirmOutingRequest);
        }

        _outingService.ConfirmOuting(id, confirmOutingRequest.SuggestionId ?? 0, confirmOutingRequest.OutingDateId ?? 0);

        TempData["Message"] = _localizer.Get("Item successfully updated");
        TempData["MessageType"] = "success";

        return RedirectToAction(nameof(Index));
    }

    // GET: Outing/Create
    public ActionResult Create()
    {
        return View();
    }

    // POST: Outing/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> Create(OutingCreateRequest outingRequest)
    {
        if (!ModelState.IsValid)
        {
            return View(outingRequest);
        }

        string id = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value!;
        User user = (await _userService.GetById(id))!;

        if (user.TeamId == 0 || user.TeamId == null)
        {
            TempData["Message"] = _localizer.Get("You are not assigned to a team");
            TempData["MessageType"] = "danger";
            return View(outingRequest);
        }

        List<OutingDate> outingDates = outingRequest.Dates?.Select(date => new OutingDate { Date = date }).ToList() ?? new List<OutingDate>();
        Outing outing = new()
        {
            Name = outingRequest.Name,
            OutingDates = outingDates,
            ImageUrl = await _fileService.SaveImageAsync(outingRequest.Image, _webHostEnvironment) ?? "",
        };

        Outing outingEntry;
        try
        {
            outingEntry = _outingService.Create(outing, (int)user.TeamId!);
        }
        catch (TeamNotFoundException)
        {
            TempData["Message"] = _localizer.Get("You are not assigned to a team");
            TempData["MessageType"] = "danger";
            return View();
        }

        if (outingEntry.Id == null)
        {
            TempData["Message"] = _localizer.Get("Error while deleting");
            TempData["MessageType"] = "danger";
            return View();
        }

        TempData["Message"] = _localizer.Get("Item successfully created");
        TempData["MessageType"] = "success";

        return RedirectToAction(nameof(Index));
    }

    // GET: Outing/Edit/5
    public ActionResult Edit(int id)
    {
        Outing? outing = _outingService.GetById(id);
        if (outing == null)
        {
            TempData["Message"] = _localizer.Get("No entity found with this id");
            TempData["MessageType"] = "danger";

            return RedirectToAction(nameof(Index));
        }

        List<Suggestion> suggestions = _suggestionService.GetAll();
        List<SelectListItem> suggestionOptions = suggestions.Select(suggestion => new SelectListItem { Value = suggestion.Id.ToString(), Text = suggestion.Name }).ToList();

        OutingEditRequest outingRequest = new()
        {
            Name = outing.Name,
            ImageUrl = outing.ImageUrl,
            SuggestionOptions = suggestionOptions,
            SelectedSuggestionIds = outing.Suggestions?.Select(s => s.Id.ToString()).ToList(),
            DeadLine = outing.DeadLine,
            Dates = outing.OutingDates?.Select(od => od.Date).ToList() ?? new List<DateTime>(),
        };

        return View(outingRequest);
    }

    // POST: Outing/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> Edit(int id, OutingEditRequest outingRequest)
    {
        if (!ModelState.IsValid)
        {
            outingRequest.SuggestionOptions = GetSuggestionOptions();
            return View(outingRequest);
        }

        List<OutingDate> outingDates = outingRequest.Dates?.Select(date => new OutingDate { Date = date }).ToList() ?? new List<OutingDate>();
        List<Suggestion> suggestions = _suggestionService.GetByIds(outingRequest.SelectedSuggestionIds?.Select(int.Parse).ToList() ?? new List<int>());
        string imageUrl;
        if (outingRequest.Image != null)
        {
            imageUrl = await _fileService.SaveImageAsync(outingRequest.Image, _webHostEnvironment) ?? "";
        }
        else
        {
            imageUrl = _outingService.GetById(id)?.ImageUrl ?? "";
        }

        Outing outing = new()
        {
            Id = id,
            Name = outingRequest.Name,
            DeadLine = outingRequest.DeadLine,
            OutingDates = outingDates,
            Suggestions = suggestions,
            ImageUrl = imageUrl,
        };
        if (!_outingService.Update(outing))
        {
            TempData["Message"] = _localizer.Get("Error while creating");
            TempData["MessageType"] = "danger";
            outingRequest.SuggestionOptions = GetSuggestionOptions();
            return View(outingRequest);
        }

        TempData["Message"] = _localizer.Get("Item successfully updated");
        TempData["MessageType"] = "success";

        return RedirectToAction(nameof(Index));
    }

    // GET: Outing/Delete/5
    public ActionResult Delete(int id)
    {
        Outing? outing = _outingService.GetById(id);
        if (outing == null)
        {
            TempData["Message"] = _localizer.Get("No entity found with this id");
            TempData["MessageType"] = "danger";

            return View();
        }

        OutingViewModel outingViewModel = new(id, outing.Name);

        return View(outingViewModel);
    }

    // POST: Outing/Delete/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Destroy(int id)
    {
        if (!_outingService.Delete(id))
        {
            TempData["Message"] = _localizer.Get("Error while deleting");
            TempData["MessageType"] = "danger";

            return View("Delete");
        }

        TempData["Message"] = _localizer.Get("Item successfully deleted");
        TempData["MessageType"] = "success";

        return RedirectToAction(nameof(Index));
    }

    private List<SelectListItem> GetSuggestionOptions()
    {
        List<Suggestion> suggestions = _suggestionService.GetAll();
        return suggestions.Select(suggestion => new SelectListItem { Value = suggestion.Id.ToString(), Text = suggestion.Name }).ToList();
    }
}