﻿using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using CoopTracker.Models;
using Microsoft.EntityFrameworkCore;

namespace CoopTracker.Controllers;

public class HomeController : ControllerBase42
{

    private readonly CoopTrackerDbContext _context;
    public HomeController(ILogger<HomeController> logger, CoopTrackerDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="customSelectTracker"></param>
    /// <returns></returns>
    [TypeFilter(typeof(GroupHashFilter))]
    public async Task<IActionResult> Index()
    //public async Task<IActionResult> Index(bool? customSelectTracker)
    {
        var tracker = await _context.Trackers.Include(e => e.Trackee).OrderBy(e => e.Submit).ToListAsync();
        var students = await _context.Students.ToListAsync();

        var currentTracker = tracker.Where(e => (IndexModel.CurrentTime >= e.Start.ToUniversalTime() && IndexModel.CurrentTime <= e.End.ToUniversalTime())).FirstOrDefault();
        if (!IsUserSelectedTracker)
        {
            if (currentTracker != null)
            {
                trackerId = currentTracker.TrackerId;
                trackerDescription = currentTracker.Description;
                UserSelectedTrackerTrakeeCount = currentTracker.Trackee.Count();
            }
        }
        return View(new IndexModel { Students = students, Trackers = tracker, TenantId = this.TenantId, TrackerIdCalculatedBySystem = currentTracker?.TrackerId });
    }


    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}

