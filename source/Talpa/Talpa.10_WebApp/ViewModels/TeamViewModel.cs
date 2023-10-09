using Microsoft.AspNetCore.Mvc.Rendering;

namespace Talpa.ViewModels;

public class TeamViewModel
{
    public int? Id { get; set; }

    public string? Name { get; set; }

    public List<UserViewModel>? Users { get; set; }
    
    public List<string>? SelectedUserIds { get; set; }
    
    public List<SelectListItem>? UserOptions { get; set; }
}