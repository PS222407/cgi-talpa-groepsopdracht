using DataAccessLayer.Dtos;

namespace DataAccessLayer.Interfaces;

public interface IUserRepository
{
    public Task<UserDto?> GetById(string id);
    
    public Task<List<RoleDto>?> GetRoles(string id);
    
    public Task<UserDto?> GetByIdWithRoles(string id);

    public Task<bool> UpdateTeam(string id, int teamId);
    
    public Task<List<UserDto>?> GetByTeam(int teamId);
}
