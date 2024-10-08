using System;
namespace CoopTracker.Models
{
    public class IndexModel
    {
       public static DateTime CurrentTime { get{return DateTime.Now;} }//new DateTime(2024,10,11,23,58,0,DateTimeKind.Utc);// DateTime.Now;
        public string? TenantIdAdmin { get; set; } = "YTZJCBF8A8";
        public string? TenantId { get; set; }
        public int? TrackerIdCalculatedBySystem { get; set; }

        public IEnumerable<Student> Students { get; set; } = new List<Student>();
        public IEnumerable<Tracker> Trackers { get; set; } = new List<Tracker>();
    }
}

