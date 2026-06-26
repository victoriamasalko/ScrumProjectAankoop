using Data.Models;

namespace Web.Models.ViewModels;

public class HoofdCategorieMetSubCategorie
{
    public Categorie? Hoofdcategorie { get; set; }
    public string? Subcategorie { get; set; }
}

public class ArtikelDetailsViewModel
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
    public Leverancier Leveranciers { get; set; } = null!;
    public List<HoofdCategorieMetSubCategorie> CategorieStructuren { get; set; } = [];
}