using System;
using System.Collections.Generic;
using NpgsqlTypes;

namespace Library_project;

public partial class Student
{
    public string? Firstname { get; set; }

    public string? Lastname { get; set; }

    public string? Middlename { get; set; }

    public int Librarycardnumber { get; set; }

    public string? Address { get; set; }

    public string? Phonenumber { get; set; }

    public NpgsqlRange<long>? Overduefees { get; set; }

    public DateOnly? Birthday { get; set; }

    public virtual ICollection<Checkout> Checkouts { get; } = new List<Checkout>();

    public virtual ICollection<Review> Reviews { get; } = new List<Review>();
}
