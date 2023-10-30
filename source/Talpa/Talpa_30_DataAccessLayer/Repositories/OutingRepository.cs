using BusinessLogicLayer.Exceptions;
using DataAccessLayer.Data;
using BusinessLogicLayer.Models;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using BusinessLogicLayer.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Repositories;

public class OutingRepository : IOutingRepository
{
    private readonly DataContext _dataContext;

    public OutingRepository(DataContext dbContext)
    {
        _dataContext = dbContext;
    }

    public Outing Create(Outing outing, int teamId)
    {
        if (!_dataContext.Teams.Any(t => t.Id == teamId))
        {
            throw new TeamNotFoundException($"There is no existing team with id {teamId}");
        }

        outing.TeamId = teamId;
        EntityEntry<Outing> entry = _dataContext.Add(outing);
        _dataContext.SaveChanges();

        return entry.Entity;
    }

    public Outing? GetById(int id)
    {
        return _dataContext.Outings?
            .Include(o => o.Suggestions)?
                .ThenInclude(s => s.Restrictions)
            .Include(o => o.OutingDates)
            .FirstOrDefault(o => o.Id == id);
    }

    public List<Outing> GetAll()
    {
        return _dataContext.Outings.ToList();
    }

    public bool Update(Outing outing)
    {
        Outing? outingDb = _dataContext.Outings
            .Include(o => o.Suggestions)
            .Include(o => o.OutingDates)
            .FirstOrDefault(o => o.Id == outing.Id);
        if (outingDb == null)
        {
            return false;
        }

        outingDb.Name = outing.Name;
        outingDb.Suggestions = outing.Suggestions;
        outingDb.OutingDates = outing.OutingDates;

        _dataContext.SaveChanges();

        return true;
    }

    public bool Delete(int id)
    {
        Outing? outing = _dataContext.Outings.FirstOrDefault(o => o.Id == id);

        if (outing == null)
        {
            return false;
        }

        _dataContext.Remove(outing);

        return _dataContext.SaveChanges() > 0;
    }

    public List<Outing> GetAllFromTeam(int teamId)
    {
        return _dataContext.Outings.Where(o => o.TeamId == teamId).ToList();
    }
}