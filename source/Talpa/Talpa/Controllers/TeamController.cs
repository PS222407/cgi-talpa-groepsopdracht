using BusinessLogicLayer.Interfaces;
using BusinessLogicLayer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Talpa.RequestModels;
using Talpa.ViewModels;

namespace Talpa.Controllers;

public class TeamController : Controller
{
    private readonly ITeamService _teamService;
    private readonly IUserService _userService;

    public TeamController(ITeamService teamService, IUserService userService)
    {
        _teamService = teamService;
        _userService = userService;
    }

    public ActionResult Index()
    {
        return View(_teamService.GetAll().Select(team => new TeamViewModel { Id = team.Id, Name = team.Name }).ToList());
    }

    // GET: TeamController/Details/5
    public async Task<ActionResult> Details(int id)
    {
        List<UserViewModel>? users = (await _userService.GetByTeam(id))?.Select(u => new UserViewModel{Name = u.Name}).ToList();
        Team? team = _teamService.GetById(id);
        if (team == null)
        {
            TempData["Message"] = "Er bestaat geen entiteit met dit id.";
            TempData["MessageType"] = "danger";

            return View();
        }

        return View(new TeamViewModel { Id = team.Id, Name = team.Name, Users = users});
    }

    // GET: TeamController/Create
    public async Task<ActionResult> Create()
    {
        List<UserViewModel> userViewModels = new List<UserViewModel>();
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

        Team team = new Team(null, teamRequest.Name);
        Team teamEntry = _teamService.Create(team);
        if (teamEntry.Id == null)
        {
            TempData["Message"] = "Fout tijdens het aanmaken.";
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
            TempData["Message"] = "Fout tijdens het koppelen van de gebruikers.";
            TempData["MessageType"] = "danger";
            return View(new TeamViewModel
            {
                Name = teamRequest.Name,
                UserOptions = teamRequest.UserOptions,
                SelectedUserIds = teamRequest.SelectedUserIds,
            });
        }

        TempData["Message"] = "Item succesvol aangemaakt";
        TempData["MessageType"] = "success";

        return RedirectToAction(nameof(Index));
    }

    // GET: TeamController/Edit/5
    public async Task<ActionResult> Edit(int id)
    {
        List<UserViewModel> userViewModels = new List<UserViewModel>();
        List<User>? users = await _userService.GetAll();
        if (users != null)
        {
            userViewModels.AddRange(users.Select(user => new UserViewModel { EmailAddress = user.Email, Name = user.Name }));
        }
        
        Team? team = _teamService.GetById(id);
        if (team == null)
        {
            TempData["Message"] = "Er bestaat geen entiteit met dit id.";
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

        Team team = new Team(id, teamRequest.Name);
        if (!_teamService.Update(team))
        {
            TempData["Message"] = "Fout tijdens het opslaan van de data.";
            TempData["MessageType"] = "danger";

            return View();
        }

        if (!await _teamService.SyncUsers(team.Id ?? -1, teamRequest.SelectedUserIds ?? new List<string>()))
        {
            TempData["Message"] = "Fout tijdens het koppelen van de gebruikers.";
            TempData["MessageType"] = "danger";
            return View(new TeamViewModel
            {
                Name = teamRequest.Name,
                UserOptions = teamRequest.UserOptions,
                SelectedUserIds = teamRequest.SelectedUserIds,
            });
        }

        TempData["Message"] = "Item succesvol gewijzigd";
        TempData["MessageType"] = "success";

        return RedirectToAction(nameof(Index));
    }

    // GET: TeamController/Delete/5
    public ActionResult Delete(int id)
    {
        Team? team = _teamService.GetById(id);
        if (team == null)
        {
            TempData["Message"] = "Er Er bestaat geen entiteit met dit id.";
            TempData["MessageType"] = "danger";

            return View();
        }

        TeamViewModel teamViewModel = new TeamViewModel { Id = id, Name = team.Name };

        return View(teamViewModel);
    }

    // POST: TeamController/Delete/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Destroy(int id)
    {
        if (!_teamService.Delete(id))
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