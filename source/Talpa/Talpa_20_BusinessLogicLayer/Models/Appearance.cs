namespace BusinessLogicLayer.Models;

public class Appearance
{
    public int Id { get; set; }
    
    public string Main { get; set; }
    
    public string Secondary { get; set; }

    public string Background { get; set; }
    
    public string? ImageUrl { get; set; }
}