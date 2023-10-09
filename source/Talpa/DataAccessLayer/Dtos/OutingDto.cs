namespace DataAccessLayer.Dtos;

public class OutingDto
{
    public int? Id { get; set; }

    public string Name { get; set; }
    
    public OutingDto(int? id, string name)
    {
        Id = id;
        Name = name;
    }
}