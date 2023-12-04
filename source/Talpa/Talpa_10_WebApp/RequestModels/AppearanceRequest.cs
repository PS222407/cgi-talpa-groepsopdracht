namespace Talpa_10_WebApp.RequestModels;

public class AppearanceRequest
{
    public string Main { get; set; }
    
    public string Secondary { get; set; }

    public string Background { get; set; }
    
    public string? ImageUrl { get; set; }
    
    public IFormFile? Image { get; set; }
}