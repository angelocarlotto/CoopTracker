using System;
namespace CoopTracker.Models;

public class ProffApplyModelIndex
{
    public int? TrackerId { get; set; }
    public int? TrackeeId { get; set; }
    public IFormFile? Image { get; set; }
    public string Description { get; set; }
    public string? FileName { get; set; }
    public string? FileType { get; set; }
    public IEnumerable<ProffApplyModelUpdate> ProffApplyModelUpdateList { get;  set; }
}


public class ProffApplyModelCreate
{
    public int ProffApplyId { get; set; }
    public IFormFile Image { get; set; }
    public string Description { get; set; }
    public string? FileName { get; set; }
    public string? FileType { get; set; }
    public int TrackeeId { get; set; }
    public string TenantId { get; set; }
    public string? UserPicture { get;  set; }
}


public class ProffApplyModelUpdate
{
    public int ProffApplyId { get; set; }
    public IFormFile? Image { get; set; }
    public string Description { get; set; }
    public string? FileName { get; set; }
    public string? FileType { get; set; }
    public int TrackeeId { get; set; }
    public string TenantId { get; set; }
    public string? UserPicture { get; set; }
}

