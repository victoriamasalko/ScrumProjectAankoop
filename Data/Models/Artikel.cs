using System;
using System.Collections.Generic;

namespace Data.Models;

public partial class Artikel
{
    public int ArtikelId { get; set; }

    public string Ean { get; set; } = null!;

    public string Naam { get; set; } = null!;

    public string Beschrijving { get; set; } = null!;

    public decimal Prijs { get; set; }

    public int GewichtInGram { get; set; }

    public int Bestelpeil { get; set; }

    public int Voorraad { get; set; }

    public int MinimumVoorraad { get; set; }

    public int MaximumVoorraad { get; set; }

    public int Levertijd { get; set; }

    public int AantalBesteldLeverancier { get; set; }

    public int MaxAantalInMagazijnPlaats { get; set; }

    public int LeveranciersId { get; set; }

    public virtual Leverancier Leveranciers { get; set; } = null!;

    public virtual ICollection<Categorie> Categories { get; set; } = new List<Categorie>();
}
