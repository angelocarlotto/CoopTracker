using System;
using System.Collections.Generic;

namespace teste45.Data;

public partial class ProffApply
{
    public int ProffApplyId { get; set; }

    public byte[] Image { get; set; } = null!;

    public string Description { get; set; } = null!;

    public string FileName { get; set; } = null!;

    public string FileType { get; set; } = null!;

    public int TrackeeId { get; set; }

    public string TenantId { get; set; } = null!;

    public virtual Trackee Trackee { get; set; } = null!;
}
