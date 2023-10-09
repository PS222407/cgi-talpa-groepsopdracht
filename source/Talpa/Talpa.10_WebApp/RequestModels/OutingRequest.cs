using System.Globalization;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Talpa.RequestModels;

public class OutingRequest
{
    private string _stringDates;
    
    public string Name { get; set; }

    public string StringDates
    {
        get => _stringDates;
        set
        {
            Dates = ConvertStringToDates(value);
            _stringDates = value;
        }
    }

    public List<DateTime> Dates { get; set; } = new();
    
    public List<string>? SelectedSuggestionIds { get; set; } = new List<string>();

    public List<SelectListItem>? SuggestionOptions { get; set; } = new List<SelectListItem> ();

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