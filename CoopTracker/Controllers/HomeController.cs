using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using CoopTracker.Models;
using Microsoft.EntityFrameworkCore;

namespace CoopTracker.Controllers;

public class HomeController : Controller
{

    private readonly CoopTrackerDbContext _context;
    public HomeController(ILogger<HomeController> logger, CoopTrackerDbContext context)
    {
        _context = context;
    }
    [TypeFilter(typeof(GroupHashFilter))]
    public async Task<IActionResult> Index()
    {
        var trakeers = await _context.Trackers.OrderBy(e=>e.Submit).ToListAsync();
        var students = await _context.Students.ToListAsync();

        return View(new IndexModel {Students=students,Trackers=trakeers });
    }


    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}

