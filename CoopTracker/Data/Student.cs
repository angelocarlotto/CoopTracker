
namespace CoopTracker;

public enum ProgramEnum
{
    NOT_SET = 0,
    CMPG = 1,
    CSTN = 2
}
public class Student : TenantBaseEntity
{
    public int StudentId { get; set; }
    public required string StudentGeorgianCoolegeId { get; set; }
    public required string Email { get; set; }
    public string FirstName { get; set; }
    public string ActualSemester { get; set; }
    public string CoopSemester { get; set; }
    public required string LastName { get; set; }
    public ProgramEnum Program { get; set; } = ProgramEnum.NOT_SET;
    public ICollection<Trackee> Trackee { get; set; } = new List<Trackee>();

    public override string ToString()
    {
        return StudentGeorgianCoolegeId + " - " + FirstName + " " + LastName;
    }
}
