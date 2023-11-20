using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Talpa_10_WebApp.RequestModels;

public class SuggestionRequest
{
    [Required(ErrorMessage = "The Name field is required.")]
    public string Name { get; set; }

    public string? ImageUrl { get; set; }

    [Required(ErrorMessage = "The Image field is required.")]
    public IFormFile Image { get; set; }

    public string? Description { get; set; }

    public List<string>? SelectedRestrictionIds { get; set; } = new();

    public List<SelectListItem>? RestrictionOptions { get; set; } = new();
}