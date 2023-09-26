using DataAccessLayer.Dtos;

namespace DataAccessLayer.Interfaces;

public interface ITeamRepository
{
    public TeamDto Create(TeamDto teamDto);

    public TeamDto? GetById(int id);

    public List<TeamDto> GetAll();

    public bool Update(TeamDto teamDto);

    public bool Delete(int id);
}