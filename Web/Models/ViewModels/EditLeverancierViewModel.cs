using Data.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace Web.Models.ViewModels
{
    public class EditLeverancierViewModel
    {
        public int LeverancierId { get; set; }

        [Required]
        public string Naam { get; set; } = null!;

        [Required]
        [StringLength(45)]
        public string BtwNummer { get; set; } = null!;

        [Required]
        [StringLength(45)]
        public string Straat { get; set; } = null!;

        [Required]
        [StringLength(5)]
        public string HuisNummer { get; set; } = null!;

        [StringLength(5)]
        public string? Bus { get; set; }

        [Required]
        public int PlaatsId { get; set; }

        public List<SelectListItem> Plaatsen = [];
        
        [Required]
        [StringLength(45)]
        public string VoornaamContactpersoon { get; set; } = null!;
        
        [Required]
        [StringLength(45)]
        public string FamilienaamContactperoon { get; set; } = null!;
    }
}
