using System;
namespace CoopTracker.Models
{
	public class IndexModel
	{
        public string? TenantIdAdmin { get; set; } = "YTZJCBF8A8";
        public string? TenantId { get; set; }
        public int? TrackerIdCalculatedBySystem { get; set; }

        public IEnumerable<Student> Students { get; set; } = new List<Student>();
        public IEnumerable<Tracker> Trackers { get; set; } = new List<Tracker>();
    }
}

