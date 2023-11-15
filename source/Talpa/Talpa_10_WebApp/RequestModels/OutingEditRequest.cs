using System.ComponentModel.DataAnnotations;
using System.Globalization;
using Microsoft.AspNetCore.Mvc.Rendering;
using Talpa_10_WebApp.Validations;
using Talpa_10_WebApp.ViewModels;

namespace Talpa_10_WebApp.RequestModels;

public class OutingEditRequest
{
    public int? Id { get; set; }

    private string? _stringDates;

    private List<DateTime>? _dates = new();

    [Required(ErrorMessage = "The Name field is required.")]
    [Display(Name = "Name")]
    public string Name { get; set; }

    public string? StringDates
    {
        get => _stringDates;
        set
        {
            Dates = value != null ? ConvertStringToDates(value) : null;
            _stringDates = value;
        }
    }

    [Required(ErrorMessage = "You need to select a deadline")]
    [DeadLine(ErrorMessage = "You can't select a timestamp that has already happened")]
    public DateTime? DeadLine { get; set; }

    [Required(ErrorMessage = "You are required to select a date.")]
    public List<DateTime>? Dates
    {
        get => _dates;
        set
        {
            _stringDates = value != null ? string.Join(",", value.Select(date => date.ToString("dd-MM-yyyy")).ToList()) : null;
            _dates = value;
        }
    }

    [SuggestionCount(ErrorMessage = "You can only select 3 suggestions.")]
    [StringListNotEmpty(ErrorMessage = "You are required to select a suggestion.")]
    public List<string>? SelectedSuggestionIds { get; set; } = new();

    public List<SelectListItem>? SuggestionOptions { get; set; } = new();

    public List<SuggestionViewModel>? Suggestions { get; set; } = new();

    public OutingEditRequest(int? id, string? name, List<SuggestionViewModel>? suggestions, List<DateTime>? dates)
    {
        Id = id;
        Name = name;
        Suggestions = suggestions;
        Dates = dates ?? new List<DateTime>();
    }

    public OutingEditRequest()  
    {
    }

    private List<DateTime> ConvertStringToDates(string value)
    {
        string[] dateStrings = value.Split(',');
        string dateFormat = "dd-MM-yyyy";
        List<DateTime> dateList = new();

        foreach (string dateString in dateStrings)
        {
            if (DateTime.TryParseExact(dateString, dateFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime date))
            {
                dateList.Add(date);
            }
        }

        return dateList;
    }
}