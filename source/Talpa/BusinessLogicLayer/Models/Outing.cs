namespace BusinessLogicLayer.Models;

public class Outing
{
    public int? Id { get; private set; }

    public string Name { get; private set; }
    
    public Outing(int? id, string name)
    {
        Id = id;
        Name = name;
    }
}