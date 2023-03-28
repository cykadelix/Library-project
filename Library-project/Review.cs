using System;
using System.Collections.Generic;

namespace Library_project;

public partial class Review
{
    public int ReviewId { get; set; }

    public string? Evaluation { get; set; }

    public int? Rating { get; set; }

    public string? ObjectType { get; set; }

    public int Mediaid { get; set; }

    public int Authorid { get; set; }

    public virtual Student Author { get; set; } = null!;

    public virtual Medium Media { get; set; } = null!;
}
