using Microsoft.AspNetCore.Mvc.Rendering;

namespace Talpa_10_WebApp.RequestModels;

public class SuggestionRequest
{
    public string Name { get; set; }

    public string? Description { get; set; }

    public List<string>? SelectedRestrictionIds { get; set; } = new();

    public List<SelectListItem>? RestrictionOptions { get; set; } = new();
}