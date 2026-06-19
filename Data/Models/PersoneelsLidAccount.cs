using System;
using System.Collections.Generic;

namespace Data.Models;

public partial class Personeelslidaccount
{
    public int PersoneelslidAccountId { get; set; }

    public string Emailadres { get; set; } = null!;

    public string Paswoord { get; set; } = null!;

    public bool Disabled { get; set; }

    public virtual ICollection<PersoneelsLid> Personeelsledens { get; set; } = new List<PersoneelsLid>();
}
