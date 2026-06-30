using Data.Models;

namespace Web.Models.ViewModels
{
    public class LeverancierIndexViewModel
    {
        public List<LeverancierOverviewViewModel> Leveranciers { get; set; } = [];

        public List<Artikel> Artikels { get; set; } = [];
    }
}
