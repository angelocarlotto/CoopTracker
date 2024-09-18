
namespace CoopTracker;

public class Student : TenantBaseEntity
{
    public int StudentId { get; set; }
    public required string StudentGeorgianCoolegeId { get; set; }
    public required string Email { get; set; }
    public string FirstName { get; set; }
    public required string LastName { get; set; }
    public ICollection<Trackee> Trackee { get; set; } = new List<Trackee>();

    public override string ToString()
    {
        return StudentGeorgianCoolegeId + " - " + FirstName + " " + LastName;
    }
}
