using System.ComponentModel.DataAnnotations;
using Talpa_10_WebApp.ViewModels;

namespace Talpa_10_WebApp.RequestModels;

public class VoteSuggestionRequest
{
    public int OutingId { get; set; }

    public string OutingName { get; set; }

    [Required] public int SuggestionId { get; set; }

    public List<SuggestionViewModel>? Suggestions { get; set; } = new();
}