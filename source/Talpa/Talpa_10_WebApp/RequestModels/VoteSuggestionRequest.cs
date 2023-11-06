using System.ComponentModel.DataAnnotations;
using Talpa_10_WebApp.Validations;
using Talpa_10_WebApp.ViewModels;

namespace Talpa_10_WebApp.RequestModels;

public class VoteSuggestionRequest
{
    public int OutingId { get; set; }

    public string? OutingName { get; set; }

    [Required(ErrorMessage = "You must select a suggestion")]
    [NotZero(ErrorMessage = "You must select a suggestion")]
    public int SuggestionId { get; set; }

    public List<SuggestionViewModel>? Suggestions { get; set; } = new();
}