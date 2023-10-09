namespace BusinessLogicLayer.Models;

public class Restriction
{
    public int Id { get; set; }

    public string Name { get; set; }

    public List<Suggestion> Suggestions { get; set; }
}