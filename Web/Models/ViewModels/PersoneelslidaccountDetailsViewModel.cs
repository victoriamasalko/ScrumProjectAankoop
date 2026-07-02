using Data.Models;

namespace Web.Models.ViewModels;

public class PersoneelslidaccountDetailsViewModel
{
    public int PersoneelslidAccountId { get; set; }

    public string Emailadres { get; set; }

    public string Paswoord { get; set; } = null!;

    public int PersoneelslidId { get; set; }

    public string PersoneelslidNaam { get; set; } 
}
