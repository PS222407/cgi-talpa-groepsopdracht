namespace Talpa.RequestModels;

public class OutingRequest
{
    public string Name { get; set; }

    public List<DateTime> Datetimes { get; set; }
}