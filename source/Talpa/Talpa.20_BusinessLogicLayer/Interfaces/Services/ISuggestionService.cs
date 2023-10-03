using BusinessLogicLayer.Models;

namespace BusinessLogicLayer.Interfaces.Services;

public interface ISuggestionService
{
    public List<Suggestion> GetAll();
}