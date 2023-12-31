﻿namespace Talpa_10_WebApp.ViewModels;

public class SuggestionViewModel
{
    public int? Id { get; set; }

    public string Name { get; set; }

    public string ImageUrl { get; set; }

    public List<string> Restrictions { get; set; }

    public int Votes { get; set; }

    public SuggestionViewModel(int? id, string name, List<string>? restrictions)
    {
        Id = id;
        Name = name;
        Restrictions = restrictions ?? new List<string>();
    }

    public SuggestionViewModel()
    {
    }
}