using System;
using System.Collections.Generic;

namespace Library_project;

public partial class Room
{
    public int RoomNumber { get; set; }

    public string Capacity { get; set; } = null!;

    public string? Features { get; set; }

    public string Availability { get; set; } = null!;

    public virtual ICollection<Event> Events { get; } = new List<Event>();
}
