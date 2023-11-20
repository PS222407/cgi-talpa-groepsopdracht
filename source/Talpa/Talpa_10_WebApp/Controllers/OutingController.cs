using System.Globalization;
using System.Security.Claims;
using BusinessLogicLayer.Interfaces.Services;
using BusinessLogicLayer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
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
            return View(_outingService.GetAllComplete().Select(outing => new OutingViewModel(outing.Id, outing.Name) { ImageUrl = outing.ImageUrl }).ToList());
        }

        int? teamId = user?.TeamId;
        if (teamId == null)
        {
            TempData["Message"] = _localizer.Get("You are not assigned to a team");
            TempData["MessageType"] = "danger";

            return View(new List<OutingViewModel>());
        }

        return View(_outingService.GetAllCompleteFromTeam((int)teamId).Select(outing => new OutingViewModel(outing.Id, outing.Name) { ImageUrl = outing.ImageUrl }).ToList());
    }

    [HttpGet("Outing/{id:int}/VoteSuggestion")]
    public ActionResult VoteSuggestion(int id)
    {
        Outing? outing = _outingService.GetByIdWithVotes(id);

        if (outing == null || outing.DeadLine == null || outing.DeadLine <= DateTime.Now)
        {
            return RedirectToAction(nameof(Index));
        }

        string userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value!;

        List<string> errors = new();
        foreach (string key in TempData.Keys)
        {
            if (TempData[key] != null && key.Contains("ErrorMessage"))
            {
                errors.Add(TempData[key].ToString());
            }
        }

        ViewBag.Errors = errors;

        if (_outingService.UserHasVotedForOuting(userId, id))
        {
            TempData["Message"] = _localizer.Get("You have already voted for this outing");
            TempData["MessageType"] = "danger";

            return RedirectToAction(nameof(Index));
        }

        if (outing == null)
        {
            TempData["Message"] = _localizer.Get("Outing does not exist");
            TempData["MessageType"] = "danger";

            return View();
        }

        List<SuggestionViewModel>? suggestionViewModels = outing.Suggestions?.Select(suggestion => new SuggestionViewModel
        {
            Id = suggestion.Id,
            Name = suggestion.Name,
            Restrictions = suggestion.Restrictions?.Select(restriction => restriction.Name).ToList() ?? new List<string>(),
            Votes = suggestion.SuggestionVotes?.Count ?? 0,
            ImageUrl = suggestion.ImageUrl ?? "",
        }).ToList();

        return View(new VoteSuggestionRequest
        {
            OutingId = id,
            OutingName = outing.Name,
            Suggestions = suggestionViewModels,
        });
    }

    [HttpPost("Outing/{id:int}/VoteDate")]
    public ActionResult VoteDate(int id, VoteSuggestionRequest voteSuggestionRequest)
    {
        if (!ModelState.IsValid)
        {
            foreach (string key in ModelState.Keys)
            {
                foreach (ModelError error in ModelState[key].Errors)
                {
                    TempData["ErrorMessage" + key] = error.ErrorMessage;
                }
            }

            TempData["Message"] = _localizer.Get("Invalid input");
            TempData["MessageType"] = "danger";

            return RedirectToAction(nameof(VoteSuggestion), new { id });
        }

        Outing? outing = _outingService.GetById(id);
        if (outing == null)
        {
            TempData["Message"] = _localizer.Get("Outing does not exist");
            TempData["MessageType"] = "danger";

            return View();
        }

        List<Checkbox> checkboxes = outing.OutingDates?.Select(outingDate => new Checkbox
        {
            Id = outingDate.Id,
            Name = outingDate.Date.ToString("dddd d MMMM yyyy", CultureInfo.CurrentCulture),
            IsSelected = false,
        }).ToList() ?? new List<Checkbox>();

        return View(new VoteDateRequest
        {
            OutingId = id,
            OutingName = outing.Name,
            SuggestionId = voteSuggestionRequest.SuggestionId,
            OutingDates = outing.OutingDates ?? new List<OutingDate>(),
            Checkboxes = checkboxes,
        });
    }

    [HttpPost("Outing/{id:int}/StoreVote")]
    public ActionResult StoreVote(int id, VoteDateRequest voteDateRequest)
    {
        if (!ModelState.IsValid)
        {
            return View("VoteDate", voteDateRequest);
        }

        string userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value!;
        List<int> votedDateIds = voteDateRequest.Checkboxes.Where(c => c.IsSelected).Select(c => c.Id).ToList();

        if (_outingService.Vote(userId, id, voteDateRequest.SuggestionId, votedDateIds))
        {
            TempData["Message"] = _localizer.Get("Item successfully created");
            TempData["MessageType"] = "success";
        }
        else
        {
            TempData["Message"] = _localizer.Get("Error while creating");
            TempData["MessageType"] = "danger";
        }

        return RedirectToAction(nameof(Index));
    }
}