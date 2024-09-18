using System;
namespace CoopTracker.Models
{
	public class IndexModel
	{
		public IEnumerable<Student> Students { get; set; } = new List<Student>();
        public IEnumerable<Tracker> Trackers { get; set; } = new List<Tracker>();
    }
}

