using Microsoft.AspNetCore.Mvc;
using System;
using System.Text;
namespace CoopTracker.Controllers;


public class ControllerBase42 : Controller
{
    public int StudentId { get { return HttpContext.Session.GetInt32("studentId").Value; } set { HttpContext.Session.SetInt32("studentId", value); } }
    public string StudentName { get { return HttpContext.Session.GetString("studentName"); } set { HttpContext.Session.SetString("studentName", value); } }

    public int trackerId { get { return HttpContext.Session.GetInt32("trackerId").Value; } set { HttpContext.Session.SetInt32("trackerId", value); } }
    public string trackerDescription { get { return HttpContext.Session.GetString("trackerDescription"); } set { HttpContext.Session.SetString("trackerDescription", value); } }

    public string TenantId { get { return HttpContext.Session.GetString("TenantId"); } set { HttpContext.Session.SetString("TenantId", value); } }

    public void CleanSession() {
        StudentId = 0;
        StudentName = string.Empty; 
        trackerId = 0;
        trackerDescription = string.Empty;
        TenantId = string.Empty;
    }
}
