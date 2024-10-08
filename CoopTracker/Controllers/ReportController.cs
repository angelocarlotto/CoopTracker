﻿using Microsoft.AspNetCore.Mvc;
using CoopTracker.Models;
using Microsoft.EntityFrameworkCore;
using Xceed.Words.NET;
using Xceed.Document.NET;

namespace CoopTracker.Controllers;

public class ReportController : ControllerBase42
{
    private readonly IWebHostEnvironment _hostingEnvironment;
    private readonly CoopTrackerDbContext _context;
    public ReportController(CoopTrackerDbContext context, IWebHostEnvironment hostingEnvironment)
    {
        _context = context;
        _hostingEnvironment = hostingEnvironment;


    }

    // Method to replace placeholders and fit the text as needed
    public IActionResult GenerateDocx(int? Trackers)
    {
        if (Trackers == null)
            ModelState.AddModelError("Trackers", "Select one tracker!");
        if (ModelState.IsValid)
        {
            string templatePath = Path.Combine(Directory.GetCurrentDirectory(), "Templates", "Template2.docx");


            var fileName = "GeneratedDoc_" + TenantId + ".docx";
            string outputFile = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "GeneratedDocuments", fileName);
            var track = _context.Trackers.FirstOrDefault(e => e.TrackerId == Trackers);
            var student = _context.Students.FirstOrDefault(e => e.StudentId == StudentId);
            var trackees = _context.Trackees.Include(e => e.ProffApply)
                                             .Where(e => e.StudentId == StudentId && e.TrackerId == Trackers)
                                             .ToList();

            // Create a new document from the template
            var document = DocX.Load(templatePath);


            document.ReplaceText("{JobTracker}", track.Description);

            document.ReplaceText("{Program}", student.Program.ToString());
            document.ReplaceText("{Name}", student.ToString());
            document.ReplaceText("{ActualSemester}", student.ActualSemester);
            document.ReplaceText("{CoopSemester}", student.CoopSemester);

            // Handle additional trackees information
            foreach (var trackee in trackees)
            {
                foreach (var table in document.Tables.Skip(1))
                {
                    var rows = table.Rows.Skip(1).Zip(trackees, (r, t) => new { row = r, trackee = t });
                    foreach (var obj in rows)
                    {
                        foreach (var cell in obj.row.Cells)
                        {
                            foreach (var paragraph in cell.Paragraphs)
                            {

                                paragraph.ReplaceText("{CompanyName}", obj.trackee.CompanyName);
                                paragraph.ReplaceText("{CompanyCity}", obj.trackee.CompanyCity);
                                paragraph.ReplaceText("{JobTitle}", obj.trackee.JobTitle);
                                paragraph.ReplaceText("{DateApplication}", obj.trackee.DateAppliation.ToShortDateString());
                                paragraph.ReplaceText("{ProvidedDocuments}", obj.trackee.DocumentProvided);
                            }
                        }
                    }
                }
            }

            // Add images and titles
            foreach (var trackee in trackees)
            {

                var pa = document.InsertParagraph(trackee.JobTitle);

                foreach (var proffApply in trackee.ProffApply)
                {
                    if (proffApply.Image != null)
                    {
                        using (var stream = new MemoryStream(proffApply.Image))
                        {
                            var image = document.AddImage(stream, proffApply.FileType);
                            Picture pic = image.CreatePicture(400, 400);
                            pa.AppendPicture(pic);
                            document.InsertParagraph().AppendPicture(pic);
                        }
                    }
                }
            }

            document.SaveAs(outputFile);
            return File(System.IO.File.ReadAllBytes(outputFile), "application/vnd.openxmlformats-officedocument.wordprocessingml.document", fileName);
        }

        return RedirectToAction(nameof(Index));
    }


    public IActionResult Report(int? Trackers)
    {
        var track = _context.Trackers.FirstOrDefault(e => e.TrackerId == Trackers);
        var student = _context.Students.FirstOrDefault(e => e.StudentId == StudentId);
        var trackees = _context.Trackees.Include(e => e.ProffApply)
                                         .Where(e => e.StudentId == StudentId && e.TrackerId == Trackers)
                                         .ToList();

        return View(new ReportHTMLModel { Student = student, Trackers = track, Trackee = trackees });
    }
    public async Task<IActionResult> Index(string? TenantSecret)
    {

        return View(new ReportModel { Trackers = _context.Trackers.Include(e => e.Trackee).ToList() });
    }

    public async Task<IActionResult> GenerateReport(int? Trackers)
    {
        return RedirectToAction("Index");
    }

}
