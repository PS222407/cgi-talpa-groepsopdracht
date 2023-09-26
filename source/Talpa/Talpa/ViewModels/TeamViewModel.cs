namespace Talpa.ViewModels;

public class TeamViewModel
{
    public int? Id { get; private set; }

    public string Name { get; private set; }
    
    public TeamViewModel(int? id, string name)
    {
        Id = id;
        Name = name;
    }
}