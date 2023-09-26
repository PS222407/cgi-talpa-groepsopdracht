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

    public async Task<User?> GetById(string id)
    {
        UserDto? userDto = await _userRepository.GetById(id);
        if (userDto == null)
        {
            return null;
        }
        
        return _userTransformer.User(userDto);
    }
    
    public async Task<List<User>?> GetAll()
    {
        List<UserDto>? userDtos = await _userRepository.GetAll();
        if (userDtos == null)
        {
            return null;
        }
        
        return _userTransformer.Users(userDtos);
    }

    public async Task<List<Role>?> GetRoles(string id)
    {
        List<RoleDto>? roleDtos = await _userRepository.GetRoles(id);
        if (roleDtos == null)
        {
            return null;
        }

        return _userTransformer.Roles(roleDtos);
    }

    public async Task<User?> GetByIdWithRoles(string id)
    {
        UserDto? userDto = await _userRepository.GetByIdWithRoles(id);
        if (userDto == null)
        {
            return null;
        }

        return _userTransformer.User(userDto);
    }

    public async Task<bool> UpdateTeam(string id, int teamId)
    {
        return await _userRepository.UpdateTeam(id, teamId);
    }

    public async Task<List<User>?> GetByTeam(int teamId)
    {
        return _userTransformer.Users(await _userRepository.GetByTeam(teamId));
    }
}