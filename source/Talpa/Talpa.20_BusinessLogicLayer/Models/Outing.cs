namespace BusinessLogicLayer.Models;

public class Outing
{
    public int? Id { get; set; }

    public string? Name { get; set; }

    public int TeamId { get; set; }
    
    public Team Team { get; set; }
}