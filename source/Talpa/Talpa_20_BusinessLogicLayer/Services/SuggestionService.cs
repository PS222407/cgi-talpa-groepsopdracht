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

    public Suggestion Create(Suggestion suggestion, string userid)
    {
        return _suggestionRepository.Create(suggestion, userid);
    }

    public bool Update(Suggestion newSuggestion, string userid)
    {
        Suggestion suggestion = _suggestionRepository.GetById(newSuggestion.Id);
        if (suggestion.UserId == userid)
        {
            return _suggestionRepository.Update(newSuggestion);
        }
        return false;
    }

    public bool Delete(int id, string userid)
    {
        Suggestion suggestion = _suggestionRepository.GetById(id);
        if (suggestion.UserId == userid)
        {
            return _suggestionRepository.Delete(id);
        }
        return false;
    }

    public Suggestion? GetById(int id, string userid)
    {
        Suggestion suggestion = _suggestionRepository.GetById(id);
        if (suggestion.UserId == userid)
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
}