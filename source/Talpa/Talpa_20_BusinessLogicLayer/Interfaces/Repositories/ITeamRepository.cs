using BusinessLogicLayer.Models;

namespace BusinessLogicLayer.Interfaces.Repositories;

public interface ITeamRepository
{
    public Team Create(Team team);

    public Team? GetById(int id);

    public List<Team> GetAll();

    public bool Update(Team team);

    public bool Delete(int id);
}