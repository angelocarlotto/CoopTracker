using System;
using System.Collections.Generic;

namespace teste45.Data;

public partial class Student
{
    public int StudentId { get; set; }

    public string StudentGeorgianCoolegeId { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string TenantId { get; set; } = null!;

    public virtual ICollection<Trackee> Trackees { get; set; } = new List<Trackee>();
}
