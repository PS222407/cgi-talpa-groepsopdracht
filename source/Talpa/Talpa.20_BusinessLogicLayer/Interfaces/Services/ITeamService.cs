using BusinessLogicLayer.Models;

namespace BusinessLogicLayer.Interfaces.Services;

public interface ITeamService
{
    public Team Create(Team team);

    public Team? GetById(int id);
    
    public List<Team> GetAll();
    
    public bool Update(Team team);

    public Task<bool> Delete(int id);
    
    public Task<bool> SyncUsers(int teamId, List<string> userIds);

    public Task<bool> AttachUsers(int teamId, List<string> userIds);
}