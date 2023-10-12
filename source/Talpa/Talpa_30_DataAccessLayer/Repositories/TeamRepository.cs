using BusinessLogicLayer.Interfaces.Repositories;
using BusinessLogicLayer.Models;
using DataAccessLayer.Data;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace DataAccessLayer.Repositories;

public class TeamRepository : ITeamRepository
{
    private readonly DataContext _dataContext;

    public TeamRepository(DataContext dbContext)
    {
        _dataContext = dbContext;
    }

    public Team Create(Team team)
    {
        EntityEntry<Team> entry = _dataContext.Add(team);
        _dataContext.SaveChanges();

        return entry.Entity;
    }

    public Team? GetById(int id)
    {
        return _dataContext.Teams.FirstOrDefault(o => o.Id == id);
    }

    public List<Team> GetAll()
    {
        return _dataContext.Teams.ToList();
    }

    public bool Update(Team team)
    {
        Team? teamDb = _dataContext.Teams.FirstOrDefault(o => o.Id == team.Id);
        if (teamDb == null)
        {
            return false;
        }

        teamDb.Name = team.Name;
        _dataContext.SaveChanges();

        return true;
    }

    public bool Delete(int id)
    {
        Team? team = _dataContext.Teams.FirstOrDefault(o => o.Id == id);

        if (team == null)
        {
            return false;
        }

        _dataContext.Remove(team);

        return _dataContext.SaveChanges() > 0;
    }
}