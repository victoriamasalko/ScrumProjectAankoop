using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Web.Models.ViewModels
{
    public class AddCategorieViewModel
    {
        [Required]
        public string Naam { get; set; } = null!;
        public int? HoofdCategorieId { get; set; }
        public List<SelectListItem> BeschikbareCategorieen { get; set; } = [];
    }
}
