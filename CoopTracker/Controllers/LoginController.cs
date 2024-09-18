using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using CoopTracker.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Text;

namespace CoopTracker.Controllers;

public class LoginController : ControllerBase42
{

    private readonly CoopTrackerDbContext _context;
    public LoginController(CoopTrackerDbContext context)
    {
        _context = context;
    }
    public async Task<IActionResult> Index()
    {
        return View(new LoginModel { TenantId = GenerateRandomString(10) });
    }
    public async Task<IActionResult> Logout()
    {
        CleanSession();
        return View("Index");
    }
    [HttpPost]
    public async Task<IActionResult> Login([Bind("TenantId")] string tenantId)
    {
        TenantId = tenantId;
        var studentAux = _context.Students.SingleOrDefault(e => e.TenantId == TenantId);
        var student = studentAux == null ? new Student() { Email = "----", FirstName = "", LastName = "", StudentGeorgianCoolegeId = "", TenantId = TenantId } : studentAux;
        if (student.StudentId == 0)
        {
            _context.Add(student);
            await _context.SaveChangesAsync();
        }
        StudentName = student.ToString();
        StudentId = student.StudentId;
        return RedirectToAction("Index", "Home");
    }

    private string GenerateRandomString(int length)
    {
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        var random = new Random();
        var stringBuilder = new StringBuilder();

        for (int i = 0; i < length; i++)
        {
            stringBuilder.Append(chars[random.Next(chars.Length)]);
        }

        return stringBuilder.ToString();
    }

}
