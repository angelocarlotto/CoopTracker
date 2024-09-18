using System;
using System.Collections.Generic;

namespace teste.Data;

public partial class GroupKeyMaster
{
    public int GroupKeyMasterId { get; set; }

    public string HashGroup { get; set; } = null!;

    public virtual ICollection<ProffApply> ProffApplies { get; set; } = new List<ProffApply>();

    public virtual ICollection<Student> Students { get; set; } = new List<Student>();

    public virtual ICollection<Trackee> Trackees { get; set; } = new List<Trackee>();

    public virtual ICollection<Tracker> Trackers { get; set; } = new List<Tracker>();
}
