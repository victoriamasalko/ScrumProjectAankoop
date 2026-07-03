using Data.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace Web.Models.ViewModels;

public class PersoneelslidaccountDetailsViewModel
{
    public int PersoneelslidAccountId { get; set; }

    [Required(ErrorMessage = "U moet een e-mailadres invullen")]
    public string Emailadres { get; set; }

    [Required(ErrorMessage = "U moet uw wachtwoord invullen")]
    public string Paswoord { get; set; } = null!;

    public int PersoneelslidId { get; set; }

    public string PersoneelslidNaam { get; set; } 
}
