using System.ComponentModel.DataAnnotations;

namespace Web.Models.ViewModels
{
    public class EditCategorieViewModel : CategorieOverviewViewModel
    {
        public int CategorieId { get; set; }

        [Required(ErrorMessage = "Dit veld is verplicht.")]
        public string NieuweNaam { get; set; } = null!;

        [Display(Name = "Hoofdcategorie")]
        public int? SelectedCategorieId { get; set; }
    }
}
