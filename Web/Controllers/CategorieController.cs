using Microsoft.AspNetCore.Mvc;
using Data.Models;
using Service;
using Web.Models.ViewModels;

namespace Web.Controllers;


public class CategorieController : Controller
{
    private readonly CategorieService _categorieService;
    public CategorieController(CategorieService categorieService)
    {
        _categorieService = categorieService;
    }

    public async Task<IActionResult> Index()
    {
        // alle categorieën ophalen
        var categorieen = await _categorieService.GetCategorieenAsync();

        // Enkel de "hoofdcategorieën" ophalen (de rest wordt opgehaald in functie "MapToOverviewModel"
        var viewModel = categorieen
            .Where(c => c.HoofdCategorieId == null)
            .Select(c => new CategorieOverviewViewModel
            {
                CategorieId = c.CategorieId,
                Naam = c.Naam,
                HoofdCategorieId = null,
                Subcategorieen = c.SubCategorieen?.Select(MapToOverviewViewModel).ToList() ?? []
            }).ToList();

        return View(nameof(Index), viewModel);
    }

    [NonAction]
    public CategorieOverviewViewModel MapToOverviewViewModel(Categorie c)
    {
        return new CategorieOverviewViewModel
        {
            CategorieId = c.CategorieId,
            Naam = c.Naam,
            HoofdCategorieId = c.HoofdCategorieId,
            Subcategorieen = c.SubCategorieen?.Select(MapToOverviewViewModel).ToList() ?? []
        };
    }
}
