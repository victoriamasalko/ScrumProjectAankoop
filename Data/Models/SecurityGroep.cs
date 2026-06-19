using System;
using System.Collections.Generic;

namespace Data.Models;

public partial class SecurityGroep
{
    public int SecurityGroepId { get; set; }

    public string Naam { get; set; } = null!;

    public virtual ICollection<PersoneelsLid> Personeelslids { get; set; } = new List<PersoneelsLid>();
}
