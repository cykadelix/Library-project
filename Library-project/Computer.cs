using System;
using System.Collections.Generic;

namespace Library_project;

public partial class Computer
{
    public string? Brand { get; set; }

    public int SerialNumber { get; set; }

    public string? Description { get; set; }

    public bool? Availability { get; set; }

    public virtual Medium SerialNumberNavigation { get; set; } = null!;
}
