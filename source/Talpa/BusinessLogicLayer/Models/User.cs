namespace BusinessLogicLayer.Models;

public class User
{
    public string Email { get; set; }

    public string Name { get; set; }
    
    public string NickName { get; set; }

    public List<Role>? Roles { get; set; }
}
