using System.ComponentModel.DataAnnotations;

namespace Web.Models.ViewModels;

public class ActiecodeViewModel : IValidatableObject
{
    public int? Id { get; set; }

    [Required]
    [StringLength(45)]
    public string Naam { get; set; } = string.Empty;

    [Required(ErrorMessage = "Dit veld is verplicht")]
    public DateTime GeldigVanDatum { get; set; }

    [Required(ErrorMessage = "Dit veld is verplicht")]
    public DateTime GeldigTotDatum { get; set; }

    [Required(ErrorMessage = "Dit veld is verplicht")]
    public bool IsEenmalig { get; set; }


    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        var today = DateTime.Today;

        if (GeldigVanDatum < today)
        {
            yield return new ValidationResult(
                "Startdatum mag niet in het verleden liggen!",
                [nameof(GeldigVanDatum)]);
        }

        if (GeldigTotDatum <= GeldigVanDatum)
        {
            yield return new ValidationResult(
                "End date must be after the start date.",
                [nameof(GeldigTotDatum)]);
        }
    }
}