using Data.Models;

namespace Web.Models.ViewModels;

public class ArtikelOverviewViewModel
{
    public string Naam { get; set; }

    public string Ean { get; set; }

    public IEnumerable<String> Categorieen { get; set; }

    public decimal Prijs { get; set; }

    public int Voorraad { get; set; }

    public int AantalBesteldLeverancier { get; set; }

}
