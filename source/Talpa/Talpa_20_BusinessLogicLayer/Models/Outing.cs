namespace BusinessLogicLayer.Models;

public class Outing
{
    public int? Id { get; set; }

    public string Name { get; set; }

    public string ImageUrl { get; set; }

    public int TeamId { get; set; }

    public Team Team { get; set; }

    public List<Suggestion>? Suggestions { get; set; }

    public List<SuggestionVote>? SuggestionVotes { get; set; }

    public List<OutingDate>? OutingDates { get; set; }
}