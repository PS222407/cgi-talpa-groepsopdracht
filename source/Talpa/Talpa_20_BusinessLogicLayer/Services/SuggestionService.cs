using BusinessLogicLayer.Interfaces.Repositories;
using BusinessLogicLayer.Interfaces.Services;
using BusinessLogicLayer.Models;

namespace BusinessLogicLayer.Services;

public class SuggestionService : ISuggestionService
{
    private readonly ISuggestionRepository _suggestionRepository;

    public SuggestionService(ISuggestionRepository suggestionRepository)
    {
        _suggestionRepository = suggestionRepository;
    }

    public Suggestion Create(Suggestion newSuggestion, string userid)
    {
        if (newSuggestion == null)
        {
            throw new ArgumentOutOfRangeException("Suggestion can't be null");
        }

        if (Exists(newSuggestion.Name))
        {
            return null;
        }

        return _suggestionRepository.Create(newSuggestion, userid);
    }

    public bool Update(Suggestion newSuggestion, string userId)
    {
        if (newSuggestion == null)
        {
            throw new ArgumentOutOfRangeException("Suggestion can't be null");
        }

        Suggestion? suggestion = _suggestionRepository.GetById(newSuggestion.Id);

        if (suggestion == null)
        {
            throw new ArgumentNullException("Suggestion doesn't exist");
        }

        if (suggestion?.UserId == userId)
        {
            return _suggestionRepository.Update(newSuggestion);
        }

        return false;
    }

    public bool Delete(int id, string userId)
    {
        Suggestion? suggestion = _suggestionRepository.GetById(id);

        if (suggestion == null)
        {
            throw new ArgumentNullException("Suggestion doesn't exist");
        }

        if (suggestion?.UserId == userId)
        {
            return _suggestionRepository.Delete(id);
        }

        return false;
    }

    public Suggestion? GetById(int id, string userid)
    {
        Suggestion? suggestion = _suggestionRepository.GetById(id);

        if (suggestion == null)
        {
            throw new ArgumentNullException("Suggestion doesn't exist");
        }

        if (suggestion?.UserId == userid)
        {
            return suggestion;
        }

        return null;
    }

    public List<Suggestion> GetByIds(List<int> ids)
    {
        return _suggestionRepository.GetByIds(ids);
    }

    public List<Suggestion> GetAll()
    {
        return _suggestionRepository.GetAll();
    }

    public List<Suggestion> GetAllByUserId(string id)
    {
        return _suggestionRepository.GetAllByUserId(id);
    }

    public bool Exists(string name, int? id = null)
    {
        return _suggestionRepository.Exists(name, id);
    }
}