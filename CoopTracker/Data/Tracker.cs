
namespace CoopTracker;

public class Tracker : ITenantBaseEntity
{
    public required int TrackerId { get; set; }
    public required DateTime Start { get; set; }
    public required DateTime End { get; set; }
    public required DateTime Submit { get; set; }
    public required string Description { get; set; }
    public ICollection<Trackee>? Trackee { get; set; } = new List<Trackee>();
    public required string TenantId { get; set; }
}
