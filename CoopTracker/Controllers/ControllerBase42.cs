using Microsoft.AspNetCore.Mvc;
using System;
using System.Text;
namespace CoopTracker.Controllers;


public class ControllerBase42 : Controller
{
    public static string hashtest { get; set; }
    public int StudentId { get { return HttpContext.Session.GetInt32("studentId").Value; } set { HttpContext.Session.SetInt32("studentId", value); } }
    public string StudentName { get { return HttpContext.Session.GetString("studentName"); } set { HttpContext.Session.SetString("studentName", value); } }

    public int trackerId { get { return HttpContext.Session.GetInt32("trackerId").Value; } set { HttpContext.Session.SetInt32("trackerId", value); } }
    public string trackerDescription { get { return HttpContext.Session.GetString("trackerDescription"); } set { HttpContext.Session.SetString("trackerDescription", value); } }

    public int groupkey { get { return HttpContext.Session.GetInt32("groupkey").Value; } set { HttpContext.Session.SetInt32("groupkey", value); } }
    public string groupHashGroup { get { return HttpContext.Session.GetString("groupHashGroup"); } set { HttpContext.Session.SetString("groupHashGroup", value); } }
}
