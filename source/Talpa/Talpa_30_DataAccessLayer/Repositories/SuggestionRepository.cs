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

    public Suggestion Create(Suggestion suggestion, string userId)
    {
        suggestion.UserId = userId;

        foreach (Restriction inputRestriction in suggestion.Restrictions?.ToList() ?? new List<Restriction>())
        {
            Restriction? existingRestriction = _dataContext.Restrictions.FirstOrDefault(r => r.Id.ToString() == inputRestriction.Name);

            if (existingRestriction != null)
            {
                suggestion.Restrictions.Remove(inputRestriction);
                suggestion.Restrictions.Add(existingRestriction);
            }
        }

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

    public List<Suggestion> GetByIds(List<int> ids)
    {
        return _dataContext.Suggestions
            .Include(s => s.Restrictions)
            .Where(s => ids.Contains(s.Id))
            .ToList();
    }

    public List<Suggestion> GetAll()
    {
        return _dataContext.Suggestions
            .Include(s => s.Restrictions)
            .ToList();
    }

    public List<Suggestion> GetAllByUserId(string id)
    {
        return _dataContext.Suggestions
            .Include(s => s.Restrictions)
            .Where(s => s.UserId == id)
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

        foreach (Restriction inputRestriction in suggestion.Restrictions?.ToList() ?? new List<Restriction>())
        {
            Restriction? existingRestriction = _dataContext.Restrictions.FirstOrDefault(r => r.Id.ToString() == inputRestriction.Name);

            if (existingRestriction != null)
            {
                suggestion.Restrictions.Remove(inputRestriction);
                suggestion.Restrictions.Add(existingRestriction);
            }
        }

        suggestionDb.Name = suggestion.Name;
        suggestionDb.Description = suggestion.Description;
        suggestionDb.Restrictions = suggestion.Restrictions;

        _dataContext.SaveChanges();
        return true;
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

    public bool Exists(string name)
    {
        string formattedName = name.Replace(" ", "");

        return _dataContext.Suggestions
            .Any(row => row.Name.Replace(" ", "") == formattedName);
    }

}