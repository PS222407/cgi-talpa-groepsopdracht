namespace Talpa_10_WebApp.ViewModels;

public class SuggestionViewModel
{
    public int? Id { get; set; }

    public string Name { get; set; }

    public List<string> Restrictions { get; set; }

    public SuggestionViewModel(int? id, string name, List<string> restrictions)
    {
        Id = id;
        Name = name;
        Restrictions = restrictions;
    }
}