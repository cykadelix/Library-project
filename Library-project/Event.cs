using System;
using System.Collections.Generic;

namespace Library_project;

public partial class Event
{
    public DateOnly Date { get; set; }

    public int EventId { get; set; }

    public string? Description { get; set; }

    public TimeOnly Length { get; set; }

    public string EventType { get; set; } = null!;

    public int RoomNumber { get; set; }

    public virtual Room RoomNumberNavigation { get; set; } = null!;
}
