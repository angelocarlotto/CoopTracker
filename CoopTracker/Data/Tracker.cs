
namespace CoopTracker;

public class Tracker
{
    public  GroupKeyMaster? GroupKeyMaster { get; set; }
    public required int TrackerId { get; set; }
    public required DateTime Start { get; set; }
    public required DateTime End { get; set; }
    public required DateTime Submit { get; set; }
    public required string Description { get; set; }
    public ICollection<Trackee>? Trackee { get; set; } = new List<Trackee>();
    public required int GroupKeyMasterId { get; set; }
}
