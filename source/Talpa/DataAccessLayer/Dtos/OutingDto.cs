namespace DataAccessLayer.Dtos;

public class OutingDto
{
    public int? Id { get; set; }

    public string Name { get; set; }

    public int TeamId { get; set; }
    
    public TeamDto Team { get; set; }
}