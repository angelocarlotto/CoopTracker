using System;
namespace CoopTracker;

public class GroupKeyMaster
{

    public  int GroupKeyMasterId { get; set; }
    public required string HashGroup { get; set; }

    public ICollection<ProffApply> ProffApply { get; set; } = new List<ProffApply>();
    public ICollection<Student> Student { get; set; } = new List<Student>();
    public ICollection<Trackee> Trackee { get; set; } = new List<Trackee>();
    public ICollection<Tracker> Tracker { get; set; } = new List<Tracker>();

}

