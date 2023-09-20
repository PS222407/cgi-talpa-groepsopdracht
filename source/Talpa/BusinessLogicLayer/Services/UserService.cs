using BusinessLogicLayer.Interfaces;
using BusinessLogicLayer.Models;
using BusinessLogicLayer.Transformers;
using DataAccessLayer.Dtos;
using DataAccessLayer.Interfaces;

namespace BusinessLogicLayer.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    
    private readonly UserTransformer _userTransformer;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
        _userTransformer = new UserTransformer();
    }

    public async Task<User?> GetUserById(string id)
    {
        UserDto? userDto = await _userRepository.GetUserById(id);
        if (userDto == null)
        {
            return null;
        }
        
        return _userTransformer.User(userDto);
    }

    public async Task<List<Role>?> GetUserRoles(string id)
    {
        List<RoleDto>? roleDtos = await _userRepository.GetUserRoles(id);
        if (roleDtos == null)
        {
            return null;
        }

        return _userTransformer.Roles(roleDtos);
    }

    public async Task<User?> GetUserByIdWithRoles(string id)
    {
        UserDto? userDto = await _userRepository.GetUserByIdWithRoles(id);
        if (userDto == null)
        {
            return null;
        }

        return _userTransformer.User(userDto);
    }
}