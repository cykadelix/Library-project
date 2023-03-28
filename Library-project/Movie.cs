using System;
using System.Collections.Generic;

namespace Library_project;

public partial class Movie
{
    public string Director { get; set; } = null!;

    public double Rating { get; set; }

    public DateOnly Releasedate { get; set; }

    public int Movieid { get; set; }

    public bool Availability { get; set; }

    public int Length { get; set; }

    public int? Genre { get; set; }

    public virtual Medium MovieNavigation { get; set; } = null!;
}
