
using DocumentFormat.OpenXml.Wordprocessing;
using System.ComponentModel.DataAnnotations;

namespace CoopTracker;

public enum ProgramEnum
{
    NOT_SET = 0,
    CMPG = 1,
    CSTN = 2
}
public class Student : ITenantBaseEntity
{
    public int StudentId { get; set; }
    [Display(Name = "Georgian College Student Number")]
    public required string StudentGeorgianCoolegeId { get; set; }
    public required string Email { get; set; }
    [Display(Name = "First Name")]
    public required string FirstName { get; set; }
    [Display(Name = "Current Semester")]
    public required string ActualSemester { get; set; }
    [Display(Name = "Coop Semester")]
    public required string CoopSemester { get; set; }
    [Display(Name = "Last Name")]
    public required string LastName { get; set; }

    public ProgramEnum Program { get; set; } = ProgramEnum.NOT_SET;
    public ICollection<Trackee> Trackee { get; set; } = new List<Trackee>();
    public required string TenantId { get; set; }
    public override string ToString()
    {
        return StudentGeorgianCoolegeId + " - " + FirstName + " " + LastName;
    }
}
