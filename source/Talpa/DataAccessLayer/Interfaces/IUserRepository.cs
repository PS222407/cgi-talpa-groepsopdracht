using DataAccessLayer.Dtos;

namespace DataAccessLayer.Interfaces;

public interface IUserRepository
{
    public Task<UserDto?> GetUserById(string id);
}
