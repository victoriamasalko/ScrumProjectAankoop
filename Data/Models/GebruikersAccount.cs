using System;
using System.Collections.Generic;

namespace Data.Models;

public partial class Gebruikersaccount
{
    public int GebruikersAccountId { get; set; }

    public string Emailadres { get; set; } = null!;

    public string Paswoord { get; set; } = null!;

    public bool Disabled { get; set; }
}
