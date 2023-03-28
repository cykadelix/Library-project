using System;
using System.Collections.Generic;

namespace Library_project;

public partial class Employee
{
    public string Firstname { get; set; } = null!;

    public string Middlename { get; set; } = null!;

    public string Lastname { get; set; } = null!;

    public int EmployeeId { get; set; }

    public int SupervisorId { get; set; }

    public string? Phonenumber { get; set; }

    public string? Email { get; set; }

    public string Homeaddress { get; set; } = null!;

    public string Position { get; set; } = null!;

    public int Salary { get; set; }

    public int? Age { get; set; }

    public string? Password { get; set; }

    public virtual ICollection<Employee> InverseSupervisor { get; } = new List<Employee>();

    public virtual Employee Supervisor { get; set; } = null!;
}
