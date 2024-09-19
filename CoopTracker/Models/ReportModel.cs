using System;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CoopTracker.Models
{
	public class ReportModel
	{
		public ICollection<Tracker> Trackers { get; set; } = new List<Tracker>();
        public List<SelectListItem> Countries { get { return Trackers.Select(e => new SelectListItem { Value = e.TrackerId.ToString(), Text = e.Description }).ToList(); } } 
    }
}

