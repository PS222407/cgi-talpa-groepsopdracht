using System.ComponentModel.DataAnnotations;
using BusinessLogicLayer.Models;
using Talpa_10_WebApp.ViewModels;

namespace Talpa_10_WebApp.RequestModels;

public class ConfirmOutingRequest
{
    public string? Name { get; set; }
    
    [Required]
    public int? OutingDateId { get; set; }

    [Required]
    public int? SuggestionId { get; set; }
    
    public List<OutingDate> OutingDates { get; set; } = new();
    
    public List<SuggestionViewModel> Suggestions { get; set; } = new();

    public int? OutingDateVoteCount { get; set; }

    public int? SuggestionVoteCount { get; set; }
}