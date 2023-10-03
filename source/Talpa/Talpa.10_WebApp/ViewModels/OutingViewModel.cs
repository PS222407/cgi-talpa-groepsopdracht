using BusinessLogicLayer.Models;

namespace Talpa.ViewModels;

public class OutingViewModel
{
    public int? Id { get; private set; }

    public string? Name { get; private set; }
    
    public List<Suggestion>? Suggestions { get; set; } 
    
    public OutingViewModel(int? id = null, string? name = null)
    {
        Id = id;
        Name = name;
    }
}