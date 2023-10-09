namespace DataAccessLayer.Dtos;

public class UserDto
{
    public string email { get; set; }

    public string name { get; set; }
    
    public string nickname { get; set; }

    public List<RoleDto>? roles { get; set; }
}
