using BusinessLogicLayer.Interfaces.Repositories;
using BusinessLogicLayer.Interfaces.Services;
using BusinessLogicLayer.Models;

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
        return _teamRepository.Create(team);
    }

    public Team? GetById(int id)
    {        
        return _teamRepository.GetById(id);
    }

    public List<Team> GetAll()
    {
        return _teamRepository.GetAll();
    }

    public bool Update(Team team)
    {
        return _teamRepository.Update(team);
    }

    public async Task<bool> Delete(int id)
    {
        return await SyncUsers(id, new List<string>()) && _teamRepository.Delete(id);
    }

    public async Task<bool> SyncUsers(int teamId, List<string> userIds)
    {
        List<string> oldUserIds = (await _userRepository.GetByTeam(teamId))?.Select(u => u.Id).ToList() ?? new List<string>();
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