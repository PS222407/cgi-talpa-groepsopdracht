using BusinessLogicLayer.Models;

namespace BusinessLogicLayer.Interfaces.Services;

public interface IUserService
{
    public Task<User?> GetById(string id);

    public Task<List<User>?> GetAll();

    public Task<List<Role>?> GetRoles(string id);

    public Task<User?> GetByIdWithRoles(string id);

    public Task<bool> UpdateTeam(string id, int teamId);

    public Task<List<User>?> GetByTeam(int teamId);
}