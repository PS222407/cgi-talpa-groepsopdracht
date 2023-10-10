using System.Globalization;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Talpa.RequestModels;

public class OutingRequest
{
    public int Id { get; set; }
    
    private string? _stringDates;
    
    private List<DateTime>? _dates = new();

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

    public List<DateTime>? Dates
    {
        get => _dates;
        set
        {
            _stringDates = value != null ? string.Join(",", value.Select(date => date.ToString("dd/MM/yyyy")).ToList()) : null;
            _dates = value;
        }
    }

    public List<string>? SelectedSuggestionIds { get; set; } = new();

    public List<SelectListItem>? SuggestionOptions { get; set; } = new();

    private List<DateTime> ConvertStringToDates(string value)
    {
        string[] dateStrings = value.Split(',');
        string dateFormat = "dd/MM/yyyy";
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