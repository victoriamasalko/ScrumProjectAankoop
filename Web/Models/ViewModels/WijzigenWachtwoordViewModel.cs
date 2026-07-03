using Data.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Web.Models.ViewModels;


public class WijzigenWachtwoordViewModel
{
    public int PersoneelslidaccountId { get; set; }

    [Required]
    public string Emailadres { get; set; }

    [Required(ErrorMessage = "U moet uw OUDE wachtwoord invullen")]
    [DisplayName("Oud wachtwoord")]
    public string OudPaswoord { get; set; } = null!;
    [DisplayName("Nieuw wachtwoord")]
    [Required(ErrorMessage = "U moet uw NIEUW wachtwoord invullen")]
    public string NieuwPaswoord { get; set; } = null!;
}
