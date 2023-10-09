using Microsoft.AspNetCore.Mvc.Rendering;

namespace Talpa.RequestModels;

public class SuggestionRequest
{
    public string Name { get; set; }
    public List<string>? SelectedRestrictionIds { get; set; } = new List<string>();
    public List<SelectListItem>? RestrictionOptions { get; set; } = new List<SelectListItem>();
}