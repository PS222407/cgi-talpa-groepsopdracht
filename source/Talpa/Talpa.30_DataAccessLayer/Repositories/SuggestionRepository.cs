using BusinessLogicLayer.Exceptions;
using DataAccessLayer.Data;
using BusinessLogicLayer.Models;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using BusinessLogicLayer.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Repositories;

public class SuggestionRepository : ISuggestionRepository
{
    private readonly DataContext _dataContext;
    
    public SuggestionRepository(DataContext dbContext)
    {
        _dataContext = dbContext;
    }

    public Suggestion Create(Suggestion suggestion, string userid)
    {
        //if (!_dataContext.User.Any(u => u.Id == userid))
        //{
        //    throw new TeamNotFoundException($"There is no existing team with id {userid}");
        //}
        
        suggestion.UserId = userid;
        EntityEntry<Suggestion> entry = _dataContext.Add(suggestion);
        _dataContext.SaveChanges();

        return entry.Entity;
    }

    public Suggestion? GetById(int id)
    {
        return _dataContext.Suggestions
            .Include(s => s.Restrictions)
            .FirstOrDefault(o => o.Id == id);
    }

    public List<Suggestion> GetAll()
    {
        return _dataContext.Suggestions
            .Include(s => s.Restrictions)
            .ToList();
    }

    public bool Update(Suggestion suggestion)
    {
        Suggestion? suggestionDb = _dataContext.Suggestions
            .Include(s => s.Restrictions)
            .FirstOrDefault(o => o.Id == suggestion.Id);
        if (suggestionDb == null)
        {
            return false;
        }

        suggestionDb.Name = suggestion.Name;
        suggestionDb.Restrictions = suggestion.Restrictions;

        return _dataContext.SaveChanges() > 0;
    }

    public bool Delete(int id)
    {
        Suggestion? suggestion = _dataContext.Suggestions
            .Include(s => s.Restrictions)
            .FirstOrDefault(o => o.Id == id);

        if (suggestion == null)
        {
            return false;
        }

        _dataContext.Remove(suggestion);

        return _dataContext.SaveChanges() > 0;
    }
}