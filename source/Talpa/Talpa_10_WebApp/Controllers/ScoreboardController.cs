using BusinessLogicLayer.Interfaces.Services;
using BusinessLogicLayer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Talpa_10_WebApp.Controllers;

[Authorize]
public class ScoreboardController : Controller
{
    private readonly IUserService _userService;

    public ScoreboardController(IUserService userService)
    {
        _userService = userService;
    }
    
    public async Task<IActionResult> Index()
    {
        List<UserScoreboard> userScoreboards = await _userService.GetTopTenUsersWhoOwnTheMostVotedSuggestions(); 
        
        return View(userScoreboards);
    }
}