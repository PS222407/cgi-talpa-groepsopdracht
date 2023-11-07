using System.ComponentModel.DataAnnotations;
using BusinessLogicLayer.Models;
using Talpa_10_WebApp.ViewModels;

namespace Talpa_10_WebApp.RequestModels;

public class VoteDateRequest
{
    public int OutingId { get; set; }

    public string? OutingName { get; set; }

    [Required] public int SuggestionId { get; set; }

    public List<OutingDate>? OutingDates { get; set; }
    
    public List<int>? VotedOutingDates { get; set; }

    public List<Checkbox> Checkboxes { get; set; }
}