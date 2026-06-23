using System.ComponentModel.DataAnnotations;

namespace Web.Models.ViewModels
{
    public class AddLeverancierViewModel
    {
        [Required]
        [MaxLength(45)]
        public string Naam { get; set; }
        [Required]
        [MaxLength(45)]
        public string BtwNummer { get; set; }
        [Required]
        [MaxLength(45)]
        public string VoornaamContactpersoon { get; set; }
        [Required]
        [MaxLength(45)]
        public string FamilienaamContactperoon { get; set; }
        [Required]
        [MaxLength(45)]
        public string Straat {  get; set; }
        [Required]
        [MaxLength(5)]
        public string HuisNummer { get; set; }
        [MaxLength(5)]
        public string Bus {  get; set; }

    }
}
