using Data.Models;

namespace Web.Models.ViewModels
{
    public class LeverancierOverviewViewModel
    { 
        //Navigatie naar Leverancier voor de properties Naam, VoornaamContactpersoon en FamilienaamContactpersoon.
        public IEnumerable<Leverancier> Leveranciers { get; set; }
        //Navigatie naar Artikel voor de artikelnaam.
        public IEnumerable<Artikel> Artikelen { get; set; }
        public string FilterOpArtikel { get; set; }
    }
}
