﻿using System;
using System.Collections.Generic;

namespace teste45.Data;

public partial class Trackee
{
    public int TrackeeId { get; set; }

    public int TrackerId { get; set; }

    public string CompanyName { get; set; } = null!;

    public string CompanyCity { get; set; } = null!;

    public string JobTitle { get; set; } = null!;

    public DateTime DateAppliation { get; set; }

    public string DocumentProvided { get; set; } = null!;

    public string LastUpdate { get; set; } = null!;

    public int StudentId { get; set; }

    public string TenantId { get; set; } = null!;

    public virtual ICollection<ProffApply> ProffApplies { get; set; } = new List<ProffApply>();

    public virtual Student Student { get; set; } = null!;

    public virtual Tracker Tracker { get; set; } = null!;
}