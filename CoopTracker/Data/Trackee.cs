
using DocumentFormat.OpenXml.Wordprocessing;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace CoopTracker;

public class Trackee : ITenantBaseEntity
{
    public required int TrackeeId { get; set; }
    public required int TrackerId { get; set; }
    [JsonIgnore]
    public  Tracker? Tracker { get; set; }
    public  Student? Student { get; set; }
    [Display(Name = "Company Name")]
    public required string CompanyName { get; set; }
    [Display(Name = "Company City")]
    public required string CompanyCity { get; set; }
    [Display(Name = "Job Title")]
    public required string JobTitle { get; set; }
    [Display(Name = "Date Application")]
    public required DateTime DateAppliation { get; set; }
    public  DateTime? DateCreated { get; set; } = DateTime.Now;
    [Display(Name = "Documents Provided")]
    public required string DocumentProvided { get; set; }
    [Display(Name = "Last Update")]
    public required string LastUpdate { get; set; }
    public  string? UrlLink { get; set; }
    public required ICollection<ProffApply> ProffApply { get; set; } = new List<ProffApply>();
    public required int StudentId { get;  set; }
    public required string TenantId { get; set; }
}
