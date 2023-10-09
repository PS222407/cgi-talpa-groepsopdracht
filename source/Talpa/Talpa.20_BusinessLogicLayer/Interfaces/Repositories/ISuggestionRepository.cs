using BusinessLogicLayer.Models;

namespace BusinessLogicLayer.Interfaces.Repositories;

public interface ISuggestionRepository
{
    public List<Suggestion> GetAll();
}