using Microsoft.AspNetCore.Mvc.Rendering;

namespace Talpa_10_WebApp.RequestModels;

public class TeamRequest
{
    public string Name { get; set; }

    public List<string>? SelectedUserIds { get; set; }

    public List<SelectListItem>? UserOptions { get; set; }
}