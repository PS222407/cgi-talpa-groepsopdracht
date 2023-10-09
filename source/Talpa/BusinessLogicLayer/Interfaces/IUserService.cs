using BusinessLogicLayer.Models;

namespace BusinessLogicLayer.Interfaces;

public interface IUserService
{
    public Task<User?> GetUserById(string id);
    
    public Task<List<Role>?> GetUserRoles(string id);
    
    public Task<User?> GetUserByIdWithRoles(string id);
}