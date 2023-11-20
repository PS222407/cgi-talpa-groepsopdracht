using System.ComponentModel.DataAnnotations.Schema;

namespace BusinessLogicLayer.Models;

public class Outing
{
    public int? Id { get; set; }

    public string Name { get; set; }

    public int TeamId { get; set; }

    public Team Team { get; set; }

    public List<Suggestion>? Suggestions { get; set; }

    public List<SuggestionVote>? SuggestionVotes { get; set; }

    [NotMapped] public OutingDate? ConfirmedOutingDate { get; set; }

    public int? ConfirmedOutingDateId { get; set; }

    [NotMapped] public Suggestion? ConfirmedSuggestion { get; set; }

    public int? ConfirmedSuggestionId { get; set; }

    [NotMapped] public int? SuggestionVoteCount { get; set; }

    public DateTime? DeadLine { get; set; }

    public List<OutingDate>? OutingDates { get; set; }

    [NotMapped] public int? OutingDateVoteCount { get; set; }
}