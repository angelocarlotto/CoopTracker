
namespace CoopTracker;

public class Student
{
    public  GroupKeyMaster? GroupKeyMaster { get; set; }
    public required int StudentId { get; set; }
    public required string StudentGeorgianCoolegeId { get; set; }
    public required string Email { get; set; }
    public  string FirstName { get; set; }
    public required string LastName { get; set; }
    public ICollection<Trackee> Trackee { get; set; } = new List<Trackee>();
    public required int GroupKeyMasterId { get; set; }
}
