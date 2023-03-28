using System;
using System.Collections.Generic;

namespace Library_project;

public partial class Journal
{
    public int Journalid { get; set; }

    public bool Isavailable { get; set; }

    public string[]? Reaserchers { get; set; }

    public string[]? Subject { get; set; }

    public int? Length { get; set; }

    public DateOnly? Publishngdate { get; set; }

    public virtual Medium JournalNavigation { get; set; } = null!;
}
