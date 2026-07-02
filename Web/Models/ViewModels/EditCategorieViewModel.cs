using System.ComponentModel.DataAnnotations;

namespace Web.Models.ViewModels
{
    public class EditCategorieViewModel
    {
        public int CategorieId { get; set; }

        [Required(ErrorMessage = "Dit veld is verplicht.")]
        public string NieuweNaam { get; set; } = string.Empty;

        [Display(Name = "Hoofdcategorie")]
        public int? SelectedCategorieId { get; set; }
        public IEnumerable<CategorieOverviewViewModel> Subcategorieen { get; set; }
        = Enumerable.Empty<CategorieOverviewViewModel>();
    }
}
