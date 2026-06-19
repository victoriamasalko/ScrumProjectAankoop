using System;
using System.Collections.Generic;

namespace Data.Models;

public partial class PersoneelsLid
{
    public int PersoneelslidId { get; set; }

    public string Voornaam { get; set; } = null!;

    public string Familienaam { get; set; } = null!;

    public bool? InDienst { get; set; }

    public int PersoneelslidAccountId { get; set; }

    public virtual Personeelslidaccount PersoneelslidAccount { get; set; } = null!;

    public virtual ICollection<SecurityGroep> SecurityGroeps { get; set; } = new List<SecurityGroep>();
}
