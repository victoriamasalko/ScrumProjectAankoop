using System.ComponentModel.DataAnnotations;

namespace Web.Models.ViewModels;

public class ActiecodeToevoegenViewModel : IValidatableObject
{
    [Required(ErrorMessage = "Dit veld is verplicht")]
    [StringLength(45)]
    public string Naam { get; set; } = string.Empty;

    [Required(ErrorMessage = "Dit veld is verplicht")]
    [Display(Name = "Geldig Van Datum")]
    public DateTime? GeldigVanDatum { get; set; }

    [Required(ErrorMessage = "Dit veld is verplicht")]
    [Display(Name = "Geldig Tot Datum")]
    public DateTime? GeldigTotDatum { get; set; }

    public bool IsEenmalig { get; set; }

    // Extra validatie voor de datums
    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        var today = DateTime.Today;

        //Controleert of de startdatum niet in het verleden ligt
        if (GeldigVanDatum < today)
        {
            yield return new ValidationResult(
                "Startdatum mag niet in het verleden liggen!",
                [nameof(GeldigVanDatum)]);
        }

        //Controleert of de einddatum na de startdatum ligt
        if (GeldigTotDatum < GeldigVanDatum)
        {
            yield return new ValidationResult(
                "Einddatum mag niet voor startdatum liggen!",
                [nameof(GeldigTotDatum)]);
        }
    }
}