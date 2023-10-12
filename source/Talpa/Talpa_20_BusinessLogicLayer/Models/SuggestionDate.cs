namespace BusinessLogicLayer.Models;

public class SuggestionDate
{
    public int Id { get; set; }

    public Suggestion Suggestion { get; set; }

    public DateTime Date { get; set; }
}