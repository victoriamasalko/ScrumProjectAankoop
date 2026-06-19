using System;
using System.Collections.Generic;

namespace Data.Models;

public partial class Plaats
{
    public int PlaatsId { get; set; }

    public string Postcode { get; set; } = null!;

    public string Naam { get; set; } = null!;

    public virtual ICollection<Leverancier> Leveranciers { get; set; } = new List<Leverancier>();
}
