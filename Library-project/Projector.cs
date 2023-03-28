using System;
using System.Collections.Generic;

namespace Library_project;

public partial class Projector
{
    public string? Brand { get; set; }

    public int Serialnumber { get; set; }

    public string? Description { get; set; }

    public int? Lumens { get; set; }

    public bool? Availability { get; set; }

    public virtual Medium SerialnumberNavigation { get; set; } = null!;
}
