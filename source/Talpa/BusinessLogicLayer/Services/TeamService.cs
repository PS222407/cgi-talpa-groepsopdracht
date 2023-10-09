using BusinessLogicLayer.Interfaces;
using BusinessLogicLayer.Models;
using DataAccessLayer.Dtos;
using DataAccessLayer.Interfaces;

namespace BusinessLogicLayer.Services;

public class TeamService : ITeamService
{
    private readonly ITeamRepository _teamRepository;
    
    public TeamService(ITeamRepository teamRepository)
    {
        _teamRepository = teamRepository;
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
}