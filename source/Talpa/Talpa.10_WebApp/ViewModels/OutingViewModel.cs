namespace Talpa.ViewModels;

public class OutingViewModel
{
    public int? Id { get; private set; }

    public string Name { get; private set; }
    
    public OutingViewModel(int? id, string name)
    {
        Id = id;
        Name = name;
    }
}