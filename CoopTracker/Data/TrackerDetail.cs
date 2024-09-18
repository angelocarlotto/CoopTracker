
namespace CoopTracker;

public class TrackerDetail
{
    public required string GroupKeyMaster { get; set; }
    public required int TrackerDetailId { get; set; }
    public required string StudentId { get; set; }
    public required string CompanyName { get; set; }
    public required string CompanyCity { get; set; }
    public required string JobTitle { get; set; }
    public required DateTime DateAppliation { get; set; }
    public required string DocumentProvided { get; set; }
    public required string LastUpdate { get; set; }
}
