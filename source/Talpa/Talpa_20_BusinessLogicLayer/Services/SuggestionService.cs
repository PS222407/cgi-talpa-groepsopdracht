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

    public bool Update(Suggestion suggestion)
    {
        return _suggestionRepository.Update(suggestion);
    }

    public bool Delete(int id)
    {
        return _suggestionRepository.Delete(id);
    }

    public Suggestion? GetById(int id)
    {
        return _suggestionRepository.GetById(id);
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