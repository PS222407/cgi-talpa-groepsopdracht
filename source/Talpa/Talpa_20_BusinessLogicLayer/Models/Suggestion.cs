namespace BusinessLogicLayer.Models;

public class Suggestion
{
    public int Id { get; set; }

    public string UserId { get; set; }

    public string Name { get; set; }

    public string? ImageUrl { get; set; }

    public string? Description { get; set; }

    public List<Outing>? Outing { get; set; }

    public List<Restriction>? Restrictions { get; set; }

    public List<SuggestionVote>? SuggestionVotes { get; set; }
}