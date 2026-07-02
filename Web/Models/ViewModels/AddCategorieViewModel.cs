using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Web.Models.ViewModels
{
    public class AddCategorieViewModel: CategorieOverviewViewModel
    {
        [Required(ErrorMessage = "Dit veld is verplicht.")]
        public string NieuweNaam { get; set; } = null!;
        [Display(Name = "Hoofdcategorie")]
        public int? SelectedCategorieId { get; set; }
    }
}
