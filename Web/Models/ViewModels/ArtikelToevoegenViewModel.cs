using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace Web.Models.ViewModels
{

    public class ArtikelToevoegenViewModel
    {
        public IFormFile? Foto { get; set; }

        //[Required(ErrorMessage = "Het veld mag niet leeg zijn.")]
        //[StringLength(13)]
        //[Display(Name = "EAN")]
        //public string Ean { get; set; } = string.Empty;

        [Required(ErrorMessage = "Het veld mag niet leeg zijn.")]
        [StringLength(45)]
        [Display(Name = "Naam")]
        public string Naam { get; set; } = string.Empty;

        [Required(ErrorMessage = "Het veld mag niet leeg zijn.")]
        [StringLength(255)]
        [Display(Name = "Beschrijving")]
        public string Beschrijving { get; set; } = string.Empty;

        [Required(ErrorMessage = "Het veld mag niet leeg zijn.")]
        [Range(0, double.MaxValue, ErrorMessage = "De prijs mag niet negatief zijn.")]
        [Display(Name = "Prijs")]
        public decimal Prijs { get; set; }

        [Required(ErrorMessage = "Het veld mag niet leeg zijn.")]
        [Range(0, int.MaxValue)]
        [Display(Name = "Gewicht in gram")]
        public int GewichtInGram { get; set; }

        [Required(ErrorMessage = "Het veld mag niet leeg zijn.")]
        [Range(1, int.MaxValue)]
        [Display(Name = "Levertijd")]
        public int Levertijd { get; set; }

        [Required(ErrorMessage = "Het veld mag niet leeg zijn.")]
        [Range(0, int.MaxValue)]
        [Display(Name = "Max aantal in magazijnplaats")]
        public int MaxAantalInMagazijnPlaats { get; set; }

        [Required(ErrorMessage = "Kies een leverancier.")]
        [Display(Name = "Leverancier")]
        public int? LeverancierId { get; set; }

        [Required(ErrorMessage = "Selecteer ten minste één categorie.")]
        [Display(Name = "Categorieën")]
        public List<int> SelectedCategorieIds { get; set; } = new();

        public IEnumerable<SelectListItem> Leveranciers { get; set; } = new List<SelectListItem>();

        public IEnumerable<SelectListItem> Categorieen { get; set; } = new List<SelectListItem>();
    }
}