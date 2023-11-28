using BusinessLogicLayer.Interfaces.Repositories;
using BusinessLogicLayer.Models;
using System.Reflection.Metadata.Ecma335;

namespace Talpa_UnitTests;

public class SuggestionTestRepository : ISuggestionRepository
{
    private readonly List<Suggestion> suggestions = new List<Suggestion>();

    public void Add(Suggestion suggestion, string userid)
    {
        suggestion.UserId = userid;
        suggestions.Add(suggestion);
    }

    public Suggestion Create(Suggestion suggestion, string userId)
    {
        suggestion.UserId = userId;

        suggestions.Add(suggestion);

        return suggestion;
    }

    public Suggestion? GetById(int id)
    {
        return suggestions
            .FirstOrDefault(o => o.Id == id);
    }

    public List<Suggestion> GetByIds(List<int> ids)
    {
        return suggestions
            .Where(s => ids.Contains(s.Id))
            .ToList();
    }

    public List<Suggestion> GetAll()
    {
        return suggestions
            .ToList();
    }

    public List<Suggestion> GetAllByUserId(string id)
    {
        return suggestions
            .Where(s => s.UserId == id)
            .ToList();
    }

    public bool Update(Suggestion suggestion)
    {
        Suggestion? suggestionDb = suggestions
            .FirstOrDefault(o => o.Id == suggestion.Id);
        if (suggestionDb == null)
        {
            return false;
        }

        suggestionDb.Name = suggestion.Name;
        suggestionDb.Description = suggestion.Description;
        suggestionDb.Restrictions = suggestion.Restrictions;
        suggestionDb.ImageUrl = suggestion.ImageUrl;

        return true;
    }

    public bool Delete(int id)
    {
        Suggestion? suggestion = suggestions
            .FirstOrDefault(o => o.Id == id);

        if (suggestion == null)
        {
            return false;
        }

        suggestions.Remove(suggestion);

        return true;
    }

    public bool Exists(string name, int? id)
    {
        return suggestions.Any(row =>
            row.Name.Replace(" ", "") == name.Replace(" ", "")
            && row.Id != id);
    }
}