namespace BusinessLogicLayer.Models;

public class OutingDate
{
    public int Id { get; set; }

    public Outing Outing { get; set; }

    public DateTime Date { get; set; }

    public List<DateVote> DateVotes { get; set; }
}