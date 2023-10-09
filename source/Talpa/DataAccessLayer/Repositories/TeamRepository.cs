using DataAccessLayer.Data;
using DataAccessLayer.Dtos;
using DataAccessLayer.Interfaces;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace DataAccessLayer.Repositories;

public class TeamRepository : ITeamRepository
{
    private readonly DataContext _dataContext;

    public TeamRepository(DataContext dbContext)
    {
        _dataContext = dbContext;
    }

    public TeamDto Create(TeamDto teamDto)
    {
        EntityEntry<TeamDto> entry = _dataContext.Add(teamDto);
        _dataContext.SaveChanges();

        return entry.Entity;
    }

    public TeamDto? GetById(int id)
    {
        return _dataContext.Teams.FirstOrDefault(o => o.Id == id);
    }

    public List<TeamDto> GetAll()
    {
        return _dataContext.Teams.ToList();
    }

    public bool Update(TeamDto teamDto)
    {
        TeamDto? team = _dataContext.Teams.FirstOrDefault(o => o.Id == teamDto.Id);
        if (team == null)
        {
            return false;
        }

        team.Name = teamDto.Name;

        return _dataContext.SaveChanges() > 0;
    }

    public bool Delete(int id)
    {
        TeamDto? team = _dataContext.Teams.FirstOrDefault(o => o.Id == id);

        if (team == null)
        {
            return false;
        }

        _dataContext.Remove(team);

        return _dataContext.SaveChanges() > 0;
    }
}