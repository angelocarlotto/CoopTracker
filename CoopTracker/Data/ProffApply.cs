
namespace CoopTracker;

public class ProffApply
{
    public  GroupKeyMaster? GroupKeyMaster { get; set; }
    public required int ProffApplyId { get; set; }
    public  Trackee? Trackee { get; set; }
    public required byte[] Image { get; set; }
    public required string Description { get; set; }
    public required string FileName { get; set; }
    public required string FileType { get; set; }
    public required int TrackeeId { get;  set; }
    public required int GroupKeyMasterId { get; set; }       
}
