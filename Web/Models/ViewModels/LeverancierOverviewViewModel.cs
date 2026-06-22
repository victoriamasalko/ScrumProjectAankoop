using Data.Models;

namespace Web.Models.ViewModels
{
    public class LeverancierOverviewViewModel
    { 
        //Navigatie naar Leverancier voor de properties Naam, VoornaamContactpersoon en FamilienaamContactpersoon, Artikel.
        public IEnumerable<Leverancier> Leveranciers { get; set; }
        public string FilterOpArtikel { get; set; }
    }
}
