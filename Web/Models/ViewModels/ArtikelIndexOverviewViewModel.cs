using Data.Models;
namespace Web.Models.ViewModels
{
    public class ArtikelIndexOverviewViewModel
    {
        public List<ArtikelOverviewViewModel> Artikels { get; set; } = [];

        public List<Categorie> Categorieën { get; set; } = [];

    }
}
