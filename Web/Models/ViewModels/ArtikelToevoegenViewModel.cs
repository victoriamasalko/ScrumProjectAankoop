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

        [Required(ErrorMessage = "Dit veld mag niet leeg zijn.")]
        [StringLength(45)]
        [Display(Name = "Naam")]
        public string Naam { get; set; } = string.Empty;

        [Required(ErrorMessage = "Dit veld mag niet leeg zijn.")]
        [StringLength(255)]
        [Display(Name = "Beschrijving")]
        public string Beschrijving { get; set; } = string.Empty;

        [Required(ErrorMessage = "Dit veld mag niet leeg zijn.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "De prijs is minstens 0,01.")]
        [Display(Name = "Prijs")]
        public decimal Prijs { get; set; }

        [Required(ErrorMessage = "Dit veld mag niet leeg zijn.")]
        [Range(1, int.MaxValue, ErrorMessage = "Het gewicht is minstens 1 gram.")]
        [Display(Name = "Gewicht in gram")]
        public int GewichtInGram { get; set; }

        [Required(ErrorMessage = "Dit veld mag niet leeg zijn.")] // dit bericht wordt niet getoond
        [Range(1, int.MaxValue, ErrorMessage = "De levertijd is minstens 1 dag.")]
        [Display(Name = "Levertijd")]
        public int Levertijd { get; set; }

        [Required(ErrorMessage = "Dit veld mag niet leeg zijn.")]
        [Range(1, int.MaxValue, ErrorMessage = "Maximaal aantal is minstens 1.")]
        [Display(Name = "Maximaal aantal in magazijnplaats")]
        public int MaxAantalInMagazijnPlaats { get; set; }

        [Required(ErrorMessage = "Kies een leverancier.")]
        [Display(Name = "Leverancier")]
        public int? LeverancierId { get; set; }

        [Required(ErrorMessage = "Selecteer ten minste één categorie.")]
        [Display(Name = "Categorieën")]
        public List<int> SelectedCategorieIds { get; set; } = [];

        public IEnumerable<SelectListItem> Leveranciers { get; set; } = new List<SelectListItem>();

        public IEnumerable<SelectListItem> Categorieen { get; set; } = new List<SelectListItem>();
    }
}