using DataAccessLayer.Dtos;

namespace DataAccessLayer.Interfaces;

public interface IUserRepository
{
    public Task<UserDto?> GetUserById(string id);
    
    public Task<List<RoleDto>?> GetUserRoles(string id);
    
    public Task<UserDto?> GetUserByIdWithRoles(string id);
}
