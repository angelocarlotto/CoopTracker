using System;
using System.Collections.Generic;

namespace teste.Data;

public partial class TrackerDetail
{
    public int TrackerDetailId { get; set; }

    public string StudentId { get; set; } = null!;

    public string CompanyName { get; set; } = null!;

    public string CompanyCity { get; set; } = null!;

    public string JobTitle { get; set; } = null!;

    public DateTime DateAppliation { get; set; }

    public string DocumentProvided { get; set; } = null!;

    public string LastUpdate { get; set; } = null!;

    public string GroupKeyMaster { get; set; } = null!;
}
