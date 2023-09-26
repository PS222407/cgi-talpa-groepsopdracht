using BusinessLogicLayer.Interfaces;
using BusinessLogicLayer.Models;
using Microsoft.AspNetCore.Mvc;
using Talpa.RequestModels;
using Talpa.ViewModels;

namespace Talpa.Controllers;

public class OutingController : Controller
{
    private readonly IOutingService _outingService;

    public OutingController(IOutingService outingService)
    {
        _outingService = outingService;
    }

    // GET: Outing
    public ActionResult Index()
    {
        return View(_outingService.GetAll().Select(outing => new OutingViewModel(outing.Id, outing.Name)).ToList());
    }

    // GET: Outing/Details/5
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
    public ActionResult Create()
    {
        return View();
    }

    // POST: Outing/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Create(OutingRequest courtRequest)
    {
        if (!ModelState.IsValid)
        {
            return View();
        }

        Outing outing = new Outing(null, courtRequest.Name);
        Outing outingEntry = _outingService.Create(outing);
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
    public ActionResult Edit(int id)
    {
        Outing? outing = _outingService.GetById(id);
        if (outing == null)
        {
            TempData["Message"] = "Er bestaat geen entiteit met dit id.";
            TempData["MessageType"] = "danger";

            return View();
        }

        OutingViewModel outingViewModel = new OutingViewModel(outing.Id, outing.Name);

        return View(outingViewModel);
    }

    // POST: Outing/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Edit(int id, OutingRequest outingRequest)
    {
        if (!ModelState.IsValid)
        {
            return View();
        }

        Outing outing = new Outing(id, outingRequest.Name);
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