using Data.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace Web.Models.ViewModels;

public class AanmeldenViewModel
{
    [Required(ErrorMessage = "U moet een e-mailadres invullen")]
    public string Emailadres { get; set; }

    [Required(ErrorMessage = "U moet uw wachtwoord invullen")]
    public string Paswoord { get; set; } = null!;
}
