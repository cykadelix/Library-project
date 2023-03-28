using System;
using System.Collections.Generic;

namespace Library_project;

public partial class Audiobook
{
    public int AudioBookId { get; set; }

    public string[]? Genre { get; set; }

    public string Narrator { get; set; } = null!;

    public string Author { get; set; } = null!;

    public int? Length { get; set; }

    public bool? Availability { get; set; }

    public virtual Medium AudioBook { get; set; } = null!;
}
