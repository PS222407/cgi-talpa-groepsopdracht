namespace BusinessLogicLayer.Models;

public class User
{
    public string Id { get; set; }
    
    public string Email { get; set; }

    public string Name { get; set; }
    
    public string NickName { get; set; }

    public int? TeamId { get; set; }
    
    public Team? Team { get; set; }

    public List<Role>? Roles { get; set; }
}