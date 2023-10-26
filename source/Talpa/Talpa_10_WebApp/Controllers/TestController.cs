using Microsoft.AspNetCore.Mvc;
using Talpa_10_WebApp.Models;

namespace Talpa_10_WebApp.Controllers;

public class TestController : Controller
{
    public IActionResult Create()
    {
        return View();
    }
    
    [HttpPost]
    public IActionResult Create(TestModel testModel)
    {
        if (!ModelState.IsValid)
        {
            return View(testModel);
        }
        
        return View();
    }
}