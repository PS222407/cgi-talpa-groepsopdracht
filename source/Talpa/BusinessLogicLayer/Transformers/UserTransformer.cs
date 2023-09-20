using BusinessLogicLayer.Models;
using DataAccessLayer.Dtos;

namespace BusinessLogicLayer.Transformers;

public class UserTransformer
{
    public Role Role(RoleDto roleDto)
    {
        return new Role
        {
            Id = roleDto.id,
            Name = roleDto.name,
            Description = roleDto.description,
        };
    }

    public List<Role>? Roles(List<RoleDto>? roleDtos)
    {
        List<Role>? roles = null;
        if (roleDtos != null)
        {
            roles = roleDtos.Select(Role).ToList();
        }

        return roles;
    }

    public User User(UserDto userDto)
    {
        return new User
        {
            Name = userDto.name,
            NickName = userDto.nickname,
            Email = userDto.email,
            Roles = Roles(userDto.roles),
        };
    }
}