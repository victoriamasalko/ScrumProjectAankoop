using System.ComponentModel.DataAnnotations;
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

    [DisplayFormat(DataFormatString = "€ {0:N2}")]
    public decimal Prijs { get; set; }
    
    [DisplayFormat(DataFormatString = "{0:N0}")]
    public int GewichtInGram { get; set; }
    
    public int Bestelpeil { get; set; }
    public int Voorraad { get; set; }
    public int MinimumVoorraad { get; set; }
    public int MaximumVoorraad { get; set; }
    public int Levertijd { get; set; }
    public int AantalBesteldLeverancier { get; set; }
    public int MaxAantalInMagazijnPlaats { get; set; }
    public int LeveranciersId { get; set; }
    public Leverancier Leverancier { get; set; } = null!;
    public List<HoofdCategorieMetSubCategorie> CategorieStructuren { get; set; } = [];
}