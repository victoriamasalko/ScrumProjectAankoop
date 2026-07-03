using Data.Models;
using System.ComponentModel.DataAnnotations;

namespace Web.Models.ViewModels;

public class CategorieOverviewViewModel
{
    public int CategorieId { get; set; }
    public int Level { get; set; }

    public string? Naam { get; set; } = null!;

    public int? HoofdCategorieId { get; set; }

    public string? HoofdCategorieNaam { get; set; }

    public List<Artikel> Artikelen { get; set; } = new List<Artikel>();
    public IEnumerable<CategorieOverviewViewModel> Subcategorieen { get; set; } = [];
}
