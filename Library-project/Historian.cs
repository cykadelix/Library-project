using System;
using System.Collections.Generic;

namespace Library_project;

public partial class Historian
{
    public string Firstname { get; set; } = null!;

    public string Middlename { get; set; } = null!;

    public string Lastname { get; set; } = null!;

    public int HistorianId { get; set; }

    public string Expertise { get; set; } = null!;

    public string Education { get; set; } = null!;

    public int Age { get; set; }
}
