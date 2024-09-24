using System;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CoopTracker.Models
{
    public class ReportModel
    {
        public ICollection<Tracker> Trackers { get; set; } = new List<Tracker>();
        public List<SelectListItem> ListOfTraker { get { return Trackers.Select(e => new SelectListItem { Value = e.TrackerId.ToString(), Text = e.Description + "(" + e.Trackee.Count() + ")" }).ToList(); } }
    }
    public class ReportHTMLModel
    {
        public Tracker Trackers { get; set; }
        public Student Student { get; set; }
        public ICollection<Trackee> Trackee { get; set; } = new List<Trackee>();
    }
}

