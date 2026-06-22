using Data.Models;

namespace Web.Models
{
    public class ArtikelOverviewViewModel
    {
        public int ArtikelId { get; set; }

        public string Ean { get; set; } = null!;

        public string Naam { get; set; } = null!;

        public decimal Prijs { get; set; }

        public int Voorraad { get; set; }

        public int AantalBesteldLeverancier { get; set; }

        public string Leverancier { get; set; } = null!;

        public List<string> Categories { get; set; } = new List<string>();
    }
}
