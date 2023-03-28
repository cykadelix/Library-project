using System;
using System.Collections.Generic;

namespace Library_project;

public partial class Medium
{
    public int Mediaid { get; set; }

    public string? MediaType { get; set; }

    public virtual Audiobook? Audiobook { get; set; }

    public virtual ICollection<Book> Books { get; } = new List<Book>();

    public virtual Camera? Camera { get; set; }

    public virtual Checkout? Checkout { get; set; }

    public virtual Computer? Computer { get; set; }

    public virtual Journal? Journal { get; set; }

    public virtual Movie? Movie { get; set; }

    public virtual Projector? Projector { get; set; }

    public virtual ICollection<Review> Reviews { get; } = new List<Review>();
}
