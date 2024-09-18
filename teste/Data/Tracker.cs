using System;
using System.Collections.Generic;

namespace teste.Data;

public partial class Tracker
{
    public int TrackerId { get; set; }

    public DateTime Start { get; set; }

    public DateTime End { get; set; }

    public DateTime Submit { get; set; }

    public string Description { get; set; } = null!;

    public int GroupKeyMasterId { get; set; }

    public virtual GroupKeyMaster GroupKeyMaster { get; set; } = null!;

    public virtual ICollection<Trackee> Trackees { get; set; } = new List<Trackee>();
}
