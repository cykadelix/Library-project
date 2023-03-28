using System;
using System.Collections.Generic;

namespace Library_project;

public partial class Book
{
    public string Author { get; set; } = null!;

    public int BookId { get; set; }

    public bool Availability { get; set; }

    public string[]? Genre { get; set; }

    public string Isbn { get; set; } = null!;

    public int? Length { get; set; }

    public DateOnly? YearPublished { get; set; }

    public virtual Medium BookNavigation { get; set; } = null!;
}
