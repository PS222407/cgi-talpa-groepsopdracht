using BusinessLogicLayer.Models;

namespace BusinessLogicLayer.Interfaces;

public interface ITeamService
{
    public Team Create(Team team);

    public Team? GetById(int id);
    
    public List<Team> GetAll();
    
    public bool Update(Team team);

    public bool Delete(int id);
}