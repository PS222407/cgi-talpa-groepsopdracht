using BusinessLogicLayer.Models;

namespace DataAccessLayer.Dtos;

public class OutingSuggestion
{
    public int OutingId { get; set; }

    public Outing Outing { get; set; }

    public int SuggestionId { get; set; }

    public Suggestion Suggestion { get; set; }
}