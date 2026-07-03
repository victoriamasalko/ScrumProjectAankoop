using Data.Models;

namespace Web.Models.ViewModels
{
    public class LeverancierOverviewViewModel
    {
        public int LeveranciersId { get; set; }
        public string Naam { get; set; }
        public string BtwNummer { get; set; }
        public string FamilienaamContactpersoon { get; set; }
        public string VoornaamContactpersoon { get; set; }
        public string FilterOpArtikel { get; set; }
        public virtual ICollection<Artikel> Artikels { get; set; } = new List<Artikel>();
    }
}
