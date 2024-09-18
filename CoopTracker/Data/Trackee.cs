
namespace CoopTracker;

public class Trackee : TenantBaseEntity
{
    public required int TrackeeId { get; set; }
    public required int TrackerId { get; set; }
    public  Tracker? Tracker { get; set; }
    public  Student? Student { get; set; }
    public required string CompanyName { get; set; }
    public required string CompanyCity { get; set; }
    public required string JobTitle { get; set; }
    public required DateTime DateAppliation { get; set; }
    public required string DocumentProvided { get; set; }
    public required string LastUpdate { get; set; }
    public required ICollection<ProffApply> ProffApply { get; set; } = new List<ProffApply>();
    public required int StudentId { get;  set; }
}
