using Data.Models;
using System.ComponentModel.DataAnnotations;

namespace Web.Models.ViewModels;

public class CategorieOverviewViewModel
{
    public int CategorieId { get; set; }

    public string Naam { get; set; } = null!;

    public int? HoofdCategorieId { get; set; }

    public string? HoofdCategorieNaam { get; set; }

    public IEnumerable<CategorieOverviewViewModel> Subcategorieen { get; set; }
}
