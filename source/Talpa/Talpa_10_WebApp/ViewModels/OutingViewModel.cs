﻿using BusinessLogicLayer.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Talpa_10_WebApp.ViewModels;

public class OutingViewModel
{
    private List<OutingDate>? _outingDates;

    public DateTime? Deadline { get; set; }

    public int? Id { get; set; }

    public string? Name { get; set; }

    public string? ImageUrl { get; set; }

    public List<SuggestionViewModel>? Suggestions { get; set; }

    public List<string>? SelectedSuggestionIds { get; set; }

    public List<SelectListItem>? SuggestionOptions { get; set; }
    
    public int? ConfirmedOutingDateId { get; set; }
    
    public OutingDate? ConfirmedOutingDate { get; set; }

    public Suggestion? ConfirmedSuggestion { get; set; }

    public List<OutingDate>? OutingDates
    {
        get => _outingDates;
        set
        {
            StringDates = ConvertDatesToString(value?.Select(od => od.Date).ToList() ?? new List<DateTime>());
            _outingDates = value;
        }
    }

    public string StringDates { get; set; }

    public OutingViewModel(int? id = null, string? name = null)
    {
        Id = id;
        Name = name;
    }

    public OutingViewModel(int? id, string? name, List<SuggestionViewModel>? suggestions)
    {
        Id = id;
        Name = name;
        Suggestions = suggestions;
    }

    public int GetDaysLeft()
    {
        return (ConfirmedOutingDate?.Date - DateTime.Now.Date)?.Days ?? 0;
    }

    private string ConvertDatesToString(List<DateTime> dateList)
    {
        return string.Join(",", dateList.Select(date => date.ToString("dd/MM/yyyy")).ToList());
    }
}