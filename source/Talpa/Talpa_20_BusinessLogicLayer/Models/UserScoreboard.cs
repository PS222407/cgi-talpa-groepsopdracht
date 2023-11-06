namespace BusinessLogicLayer.Models;

public class UserScoreboard
{
    public string UserId { get; set; }
    
    public string UserName { get; set; }

    public int SuggestionId { get; set; }

    public string SuggestionName { get; set; }

    public int VoteCount { get; set; }
}