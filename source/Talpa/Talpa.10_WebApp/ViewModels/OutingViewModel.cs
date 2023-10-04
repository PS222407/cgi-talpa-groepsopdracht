﻿using BusinessLogicLayer.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Talpa.ViewModels;

public class OutingViewModel
{
    private List<OutingDate>? _outingDates;
    
    public int? Id { get; set; }

    public string? Name { get; set; }
    
    public List<Suggestion>? Suggestions { get; set; } 
    
    public List<string>? SelectedSuggestionIds { get; set; }
    
    public List<SelectListItem>? SuggestionOptions { get; set; }

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
    
    private string ConvertDatesToString(List<DateTime> dateList)
    {
        return string.Join(",", dateList.Select(date => date.ToString("dd/MM/yyyy")).ToList());
    }
}