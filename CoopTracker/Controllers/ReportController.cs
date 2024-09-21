using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using Microsoft.AspNetCore.Mvc;
using CoopTracker.Models;
using System.Text;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace CoopTracker.Controllers;

public class ReportController : ControllerBase42
{

    private readonly CoopTrackerDbContext _context;
    public ReportController(CoopTrackerDbContext context)
    {
        _context = context;
    }

    // Method to replace placeholders and fit the text as needed
    private void ReplacePlaceholderInParagraph(Paragraph paragraph, string placeholder, string replacement)
    {
        StringBuilder fullText = new StringBuilder();
        List<Run> runs = new List<Run>();

        // Collect all Run elements
        foreach (var run in paragraph.Elements<Run>())
        {
            runs.Add(run);
            foreach (var text in run.Elements<Text>())
            {
                fullText.Append(text.Text); // Concatenate text from multiple runs
            }
        }

        // Check if the placeholder exists in the concatenated text
        string combinedText = fullText.ToString();
        if (combinedText.Contains(placeholder))
        {
            // Replace the placeholder with the new value
            combinedText = combinedText.Replace(placeholder, replacement);

            // Clear all existing Text elements in the Runs
            foreach (var run in runs)
            {
                run.RemoveAllChildren<Text>();
            }

            // Create a new Text element with the full replaced text and add it to the first Run
            runs[0].AppendChild(new Text(combinedText));

            // If the replacement text is long, it will be placed into a single Text element in the first Run
            // The remaining Runs (if any) will be empty and can be kept or removed based on your needs.
        }
    }
    public IActionResult GenerateDocx(int? Trackers)
    {
        string templatePath = Path.Combine(Directory.GetCurrentDirectory(), "Templates", "Template2.docx");
        string outputFile = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "GeneratedDocuments", "GeneratedDoc.docx");
        var track = _context.Trackers.FirstOrDefault(e => e.TrackerId == Trackers);
        var student = _context.Students.FirstOrDefault(e => e.StudentId == StudentId);
        var trakees = _context.Trackees.Where(e => e.StudentId == StudentId && e.TrackerId == Trackers).ToList();
        // Copy the template to the output path
        System.IO.File.Copy(templatePath, outputFile, true);

        // Open the copied document
        using (WordprocessingDocument wordDoc = WordprocessingDocument.Open(outputFile, true))
        {
            // Get the main document part
            MainDocumentPart mainPart = wordDoc.MainDocumentPart;

            // Get the document body (content)
            var body = mainPart.Document.Body;


            foreach (var paragraph in body.Elements<Paragraph>())
            {
                ReplacePlaceholderInParagraph(paragraph, "{JobTracker}", track.Description);
            }


            // Traverse through tables, rows, and cells
            foreach (var table in body.Elements<Table>())
            {
                foreach (var row in table.Elements<TableRow>())
                {
                    foreach (var cell in row.Elements<TableCell>())
                    {
                        // Replace placeholders in paragraphs within table cells
                        foreach (var paragraph in cell.Elements<Paragraph>())
                        {
                            ReplacePlaceholderInParagraph(paragraph, "{Program}", student.Program.ToString());
                            ReplacePlaceholderInParagraph(paragraph, "{Name}", student.ToString());
                            ReplacePlaceholderInParagraph(paragraph, "{ActualSemester}", student.ActualSemester);
                            ReplacePlaceholderInParagraph(paragraph, "{CoopSemester}", student.CoopSemester);
                        }
                    }
                }
            }

            foreach (var table in body.Elements<Table>().Skip(1))
            {
                var tt = table.Elements<TableRow>().Skip(1).Zip(trakees, (r, t) => new { row = r, trakee = t });
                foreach (var obj in tt)
                {
                    foreach (var cell in obj.row.Elements<TableCell>())
                    {
                        // Replace placeholders in paragraphs within table cells
                        foreach (var paragraph in cell.Elements<Paragraph>())
                        {
                            ReplacePlaceholderInParagraph(paragraph, "{CompanyName}", obj.trakee.CompanyName);
                            ReplacePlaceholderInParagraph(paragraph, "{CompanyCity}", obj.trakee.CompanyCity);
                            ReplacePlaceholderInParagraph(paragraph, "{JobTitle}", obj.trakee.JobTitle);
                            ReplacePlaceholderInParagraph(paragraph, "{DateApplication}", obj.trakee.DateAppliation.ToShortDateString());
                            ReplacePlaceholderInParagraph(paragraph, "{ProvidedDocuments}", obj.trakee.DocumentProvided);
                        }
                    }
                }
            }

            foreach (var r in body.Elements<Table>().Skip(1).FirstOrDefault().Elements<TableRow>().Skip(trakees.Count + 1))
            {
                foreach (var cell in r.Elements<TableCell>())
                {
                    foreach (var run in cell.Elements<Run>())
                    {

                        run.RemoveAllChildren<Text>();
                    }
                }
            }
            // Save the changes to the document
            mainPart.Document.Save();
        }

        // Return the document as a file download
        return File(System.IO.File.ReadAllBytes(outputFile), "application/vnd.openxmlformats-officedocument.wordprocessingml.document", "GeneratedDoc.docx");
    }

    public IActionResult GenerateDocx2()
    {
        string templatePath = Path.Combine(Directory.GetCurrentDirectory(), "Templates", "Template2.docx");
        string outputFile = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "GeneratedDocuments", "GeneratedDoc.docx");

        // Copy the template to the output path
        System.IO.File.Copy(templatePath, outputFile, true);

        // Open the copied document
        using (WordprocessingDocument wordDoc = WordprocessingDocument.Open(outputFile, true))
        {
            // Get the main document part
            MainDocumentPart mainPart = wordDoc.MainDocumentPart;

            // Get the document body (content)
            var body = mainPart.Document.Body;

            // Iterate through all paragraphs and runs to replace placeholders
            foreach (var paragraph in body.Elements<Paragraph>())
            {
                foreach (var run in paragraph.Elements<Run>())
                {
                    foreach (var text in run.Elements<Text>())
                    {
                        // Replace placeholders with actual values
                        if (text.Text.Contains("{Name}"))
                        {
                            text.Text = text.Text.Replace("{Name}", "Angelo Carlotto");
                        }
                        if (text.Text.Contains("{Date}"))
                        {
                            text.Text = text.Text.Replace("{Date}", DateTime.Now.ToString("MMMM dd, yyyy"));
                        }
                    }
                }
            }

            // Save the changes
            mainPart.Document.Save();
        }

        // Return the document as a file download
        return File(System.IO.File.ReadAllBytes(outputFile), "application/vnd.openxmlformats-officedocument.wordprocessingml.document", "GeneratedDoc.docx");
    }


    public async Task<IActionResult> Index(string? TenantSecret)
    {

        return View(new ReportModel { Trackers = _context.Trackers.Include(e=>e.Trackee).ToList() });
    }

    public async Task<IActionResult> GenerateReport(int? Trackers)
    {
        return RedirectToAction("Index");
    }


}
