using System;
namespace CoopTracker.Models
{
	public class IndexModel
	{
		public IEnumerable<GroupKeyMaster> GroupKeyMasters { get; set; }
        public IEnumerable<Student> Students { get; set; }
        public IEnumerable<Tracker> Trackers { get; set; }
	}
}

