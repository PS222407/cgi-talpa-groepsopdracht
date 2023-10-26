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

namespace Talpa_10_WebApp.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = $"{RoleName.Admin}")]
public class TeamController : Controller
{
    private readonly ITeamService _teamService;

    private readonly IUserService _userService;
    
    private readonly Shared _localizer;

    public TeamController(ITeamService teamService, IUserService userService, IStringLocalizer<Shared> localizer)
    {
        _teamService = teamService;
        _userService = userService;
        _localizer = new Shared(localizer);
    }

    public ActionResult Index()
    {
        return View(_teamService.GetAll().Select(team => new TeamViewModel { Id = team.Id, Name = team.Name }).ToList());
    }

    // GET: TeamController/Details/5
    public async Task<ActionResult> Details(int id)
    {
        List<UserViewModel>? users = (await _userService.GetByTeam(id))?.Select(u => new UserViewModel { Name = u.Name }).ToList();
        Team? team = _teamService.GetById(id);
        if (team == null)
        {
            TempData["Message"] = _localizer.Get("No entity found with this id");
            TempData["MessageType"] = "danger";

            return View();
        }

        return View(new TeamViewModel { Id = team.Id, Name = team.Name, Users = users });
    }

    // GET: TeamController/Create
    public async Task<ActionResult> Create()
    {
        List<UserViewModel> userViewModels = new();
        List<User>? users = await _userService.GetAll();
        if (users != null)
        {
            userViewModels.AddRange(users.Select(user => new UserViewModel { EmailAddress = user.Email, Name = user.Name }));
        }

        return View(new TeamViewModel
        {
            UserOptions = users?.Select(u => new SelectListItem
            {
                Value = u.Id,
                Text = u.Name,
            }).ToList(),
            Users = userViewModels,
        });
    }

    // POST: TeamController/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> Create(TeamRequest teamRequest)
    {
        if (!ModelState.IsValid)
        {
            return View(new TeamViewModel
            {
                Name = teamRequest.Name,
                UserOptions = teamRequest.UserOptions,
                SelectedUserIds = teamRequest.SelectedUserIds,
            });
        }

        Team team = new() { Name = teamRequest.Name };
        Team teamEntry = _teamService.Create(team);
        if (teamEntry.Id == null)
        {
            TempData["Message"] = _localizer.Get("Error while creating");
            TempData["MessageType"] = "danger";
            return View(new TeamViewModel
            {
                Name = teamRequest.Name,
                UserOptions = teamRequest.UserOptions,
                SelectedUserIds = teamRequest.SelectedUserIds,
            });
        }

        if (!await _teamService.AttachUsers(teamEntry.Id ?? -1, teamRequest.SelectedUserIds ?? new List<string>()))
        {
            TempData["Message"] = _localizer.Get("Error white attaching users");
            TempData["MessageType"] = "danger";
            return View(new TeamViewModel
            {
                Name = teamRequest.Name,
                UserOptions = teamRequest.UserOptions,
                SelectedUserIds = teamRequest.SelectedUserIds,
            });
        }

        TempData["Message"] = _localizer.Get("Item successfully created");
        TempData["MessageType"] = "success";

        return RedirectToAction(nameof(Index));
    }

    // GET: TeamController/Edit/5
    public async Task<ActionResult> Edit(int id)
    {
        List<UserViewModel> userViewModels = new();
        List<User>? users = await _userService.GetAll();
        if (users != null)
        {
            userViewModels.AddRange(users.Select(user => new UserViewModel { EmailAddress = user.Email, Name = user.Name }));
        }

        Team? team = _teamService.GetById(id);
        if (team == null)
        {
            TempData["Message"] = _localizer.Get("No entity found with this id");
            TempData["MessageType"] = "danger";

            return View();
        }

        return View(new TeamViewModel
        {
            Id = team.Id,
            Name = team.Name,
            UserOptions = users?.Select(u => new SelectListItem
            {
                Value = u.Id,
                Text = u.Name,
            }).ToList(),
            SelectedUserIds = (await _userService.GetByTeam(team.Id ?? -1))?.Select(u => u.Id).ToList(),
            Users = userViewModels,
        });
    }

    // POST: TeamController/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> Edit(int id, TeamRequest teamRequest)
    {
        if (!ModelState.IsValid)
        {
            return View(new TeamViewModel
            {
                Name = teamRequest.Name,
                UserOptions = teamRequest.UserOptions,
                SelectedUserIds = teamRequest.SelectedUserIds,
            });
        }

        Team team = new() { Id = id, Name = teamRequest.Name };
        if (!_teamService.Update(team))
        {
            TempData["Message"] = _localizer.Get("Error while creating");
            TempData["MessageType"] = "danger";

            return View();
        }

        if (!await _teamService.SyncUsers(team.Id ?? -1, teamRequest.SelectedUserIds ?? new List<string>()))
        {
            TempData["Message"] = _localizer.Get("Error white attaching users");
            TempData["MessageType"] = "danger";
            return View(new TeamViewModel
            {
                Name = teamRequest.Name,
                UserOptions = teamRequest.UserOptions,
                SelectedUserIds = teamRequest.SelectedUserIds,
            });
        }

        TempData["Message"] = _localizer.Get("Item successfully updated");
        TempData["MessageType"] = "success";

        return RedirectToAction(nameof(Index));
    }

    // GET: TeamController/Delete/5
    public ActionResult Delete(int id)
    {
        Team? team = _teamService.GetById(id);
        if (team == null)
        {
            TempData["Message"] = _localizer.Get("No entity found with this id");
            TempData["MessageType"] = "danger";

            return View();
        }

        TeamViewModel teamViewModel = new() { Id = id, Name = team.Name };

        return View(teamViewModel);
    }

    // POST: TeamController/Delete/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> Destroy(int id)
    {
        if (!await _teamService.Delete(id))
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