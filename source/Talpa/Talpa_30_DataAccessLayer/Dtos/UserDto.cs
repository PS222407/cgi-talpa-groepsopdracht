namespace DataAccessLayer.Dtos;

public class UserDto
{
    public string user_id { get; set; }

    public string email { get; set; }

    public string name { get; set; }

    public string nickname { get; set; }

    public UserMetaData user_metadata { get; set; }

    public List<RoleDto>? roles { get; set; }
}