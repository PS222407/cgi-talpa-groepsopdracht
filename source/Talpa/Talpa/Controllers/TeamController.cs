using BusinessLogicLayer.Interfaces;
using BusinessLogicLayer.Models;
using BusinessLogicLayer.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talpa.RequestModels;
using Talpa.ViewModels;

namespace Talpa.Controllers
{
    public class TeamController : Controller
    {
        private readonly ITeamService _teamService;

        public TeamController(ITeamService teamService)
        {
            _teamService = teamService;
        }
        public ActionResult Index()
        {
            return View(_teamService.GetAll().Select(team => new TeamViewModel(team.Id, team.Name)).ToList());
        }

        // GET: TeamController/Details/5
        public ActionResult Details(int id)
        {
            Team? team = _teamService.GetById(id);
            if (team == null)
            {
                TempData["Message"] = "Er bestaat geen entiteit met dit id.";
                TempData["MessageType"] = "danger";

                return View();
            }

            return View(new TeamViewModel(team.Id, team.Name));
        }

        // GET: TeamController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TeamController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(TeamRequest teamRequest)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            Team team = new Team(null, teamRequest.Name);
            Team teamEntry = _teamService.Create(team);
            if (teamEntry.Id == null)
            {
                TempData["Message"] = "Fout tijdens het aanmaken.";
                TempData["MessageType"] = "danger";
                return View();
            }

            TempData["Message"] = "Item succesvol aangemaakt";
            TempData["MessageType"] = "success";

            return RedirectToAction(nameof(Index));
        }

        // GET: TeamController/Edit/5
        public ActionResult Edit(int id)
        {
            Team? team = _teamService.GetById(id);
            if (team == null)
            {
                TempData["Message"] = "Er bestaat geen entiteit met dit id.";
                TempData["MessageType"] = "danger";

                return View();
            }

            TeamViewModel teamViewModel = new TeamViewModel(team.Id, team.Name);

            return View(teamViewModel);
        }

        // POST: TeamController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, TeamRequest teamRequest)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            Team team = new Team(id, teamRequest.Name);
            if (!_teamService.Update(team))
            {
                TempData["Message"] = "Fout tijdens het opslaan van de data.";
                TempData["MessageType"] = "danger";

                return View();
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

            TeamViewModel teamViewModel = new TeamViewModel(id, team.Name);

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
}
