using BusinessLogicLayer.Interfaces;
using BusinessLogicLayer.Models;
using DataAccessLayer.Dtos;
using DataAccessLayer.Interfaces;

namespace BusinessLogicLayer.Services;

public class TeamService : ITeamService
{
    private readonly ITeamRepository _teamRepository;
    
    private readonly IUserRepository _userRepository;
    
    public TeamService(ITeamRepository teamRepository, IUserRepository userRepository)
    {
        _teamRepository = teamRepository;
        _userRepository = userRepository;
    }

    public Team Create(Team team)
    {
        TeamDto teamDto = _teamRepository.Create(new TeamDto(null, team.Name));

        return new Team(teamDto.Id, teamDto.Name);
    }

    public Team? GetById(int id)
    {
        TeamDto? teamDto = _teamRepository.GetById(id);
        if (teamDto == null)
        {
            return null;
        }
        
        return new Team(teamDto.Id, teamDto.Name);
    }

    public List<Team> GetAll()
    {
        return _teamRepository.GetAll().Select(TeamDto => new Team(TeamDto.Id, TeamDto.Name)).ToList();
    }

    public bool Update(Team team)
    {
        return _teamRepository.Update(new TeamDto(team.Id, team.Name));
    }

    public bool Delete(int id)
    {
        return _teamRepository.Delete(id);
    }

    public async Task<bool> SyncUsers(int teamId, List<string> userIds)
    {
        List<string> oldUserIds = (await _userRepository.GetByTeam(teamId))?.Select(u => u.user_id).ToList() ?? new List<string>();
        List<string> userIdsToRemove = oldUserIds.Except(userIds).ToList();
        List<string> newUserIds = userIds.Except(oldUserIds).ToList();
        
        return await AttachUsers(teamId, newUserIds) && await DetachUsers(userIdsToRemove);
    }

    public async Task<bool> AttachUsers(int teamId, List<string> userIds)
    {
        bool usersTeamIsSuccess = true;
        foreach (string userId in userIds)
        {
            usersTeamIsSuccess = await _userRepository.UpdateTeam(userId, teamId);
        }

        return usersTeamIsSuccess;
    }

    public async Task<bool> DetachUsers(List<string> userIds)
    {
        bool usersTeamIsSuccess = true;
        foreach (string userId in userIds)
        {
            usersTeamIsSuccess = await _userRepository.UpdateTeam(userId, null);
        }

        return usersTeamIsSuccess;
    }
}