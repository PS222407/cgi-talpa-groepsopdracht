namespace BusinessLogicLayer.Models;

public class DateVote
{
    public string UserId { get; set; }
    
    public int OutingDateId { get; set; }

    public OutingDate OutingDate { get; set; }
}