using DataAccessLayer.Data;
using DataAccessLayer.Dtos;
using DataAccessLayer.Interfaces;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace DataAccessLayer.Repositories;

public class OutingRepository : IOutingRepository
{
    private readonly DataContext _dataContext;
    
    public OutingRepository(DataContext dbContext)
    {
        _dataContext = dbContext;
    }

    public OutingDto Create(OutingDto outingDto, int teamId)
    {
        outingDto.TeamId = teamId;
        EntityEntry<OutingDto> entry = _dataContext.Add(outingDto);
        _dataContext.SaveChanges();

        return entry.Entity;
    }

    public OutingDto? GetById(int id)
    {
        return _dataContext.Outings.FirstOrDefault(o => o.Id == id);
    }

    public List<OutingDto> GetAll()
    {
        return _dataContext.Outings.ToList();
    }

    public bool Update(OutingDto outingDto)
    {
        OutingDto? outing = _dataContext.Outings.FirstOrDefault(o => o.Id == outingDto.Id);
        if (outing == null)
        {
            return false;
        }

        outing.Name = outingDto.Name;

        return _dataContext.SaveChanges() > 0;
    }

    public bool Delete(int id)
    {
        OutingDto? outing = _dataContext.Outings.FirstOrDefault(o => o.Id == id);

        if (outing == null)
        {
            return false;
        }

        _dataContext.Remove(outing);

        return _dataContext.SaveChanges() > 0;
    }

    public List<OutingDto> GetAllFromTeam(int teamId)
    {
        return _dataContext.Outings.Where(o => o.TeamId == teamId).ToList();
    }
}