using Data.Models;
using System.ComponentModel.DataAnnotations;

namespace Web.Models.ViewModels;

public class ArtikelOverviewViewModel
{
    public int ArtikelId { get; set; }

    public string Naam { get; set; }

    public string Beschrijving { get; set; }

    public string Ean { get; set; }

    public IEnumerable<Categorie> Categorieen { get; set; } = [];

    [DisplayFormat(DataFormatString = "{0:€ #,##0.00}")]
    public decimal Prijs { get; set; }

    public int Bestelpeil { get; set; }

    public int Voorraad { get; set; }

    public int AantalBesteldLeverancier { get; set; }

    public int LeveranciersId { get; set; }

    public bool IsActief { get; set; }

}
