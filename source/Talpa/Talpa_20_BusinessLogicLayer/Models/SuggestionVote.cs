namespace BusinessLogicLayer.Models;

public class SuggestionVote
{
    public string UserId { get; set; }

    public int SuggestionId { get; set; }
    public Suggestion Suggestion { get; set; }

    public int OutingId { get; set; }
    public Outing Outing { get; set; }
}