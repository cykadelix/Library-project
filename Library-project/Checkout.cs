using System;
using System.Collections.Generic;

namespace Library_project;

public partial class Checkout
{
    public int CheckoutId { get; set; }

    public DateOnly? Checkoutdt { get; set; }

    public DateOnly? ReTurn { get; set; }

    public string? ObjectType { get; set; }

    public int? LateFee { get; set; }

    public int? Studentid { get; set; }

    public virtual Medium CheckoutNavigation { get; set; } = null!;

    public virtual Student? Student { get; set; }
}
