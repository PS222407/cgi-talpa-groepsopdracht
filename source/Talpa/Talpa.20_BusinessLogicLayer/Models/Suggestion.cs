namespace BusinessLogicLayer.Models;

public class Suggestion
{
    public int Id { get; set; }
    
    public string UserId { get; set; }
    
    public Outing Outing { get; set; }

    public string Name { get; set; }

    public List<Restriction> Restrictions { get; set; }

    public List<SuggestionVote> SuggestionVotes { get; set; }
}