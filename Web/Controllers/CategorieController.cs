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

    public async Task<IEnumerable<CategorieOverviewViewModel>> PrepareCategorieen()
    {
        // alle categorieën ophalen
        var categorieen = await _categorieService.GetCategorieenAsync();

        // Enkel de "hoofdcategorieën" ophalen (de rest wordt opgehaald in functie "MapToOverviewModel"
        var viewModel = categorieen
            .Where(c => c.HoofdCategorieId == null)
            .Select(c => new CategorieOverviewViewModel
            {
                CategorieId = c.CategorieId,
                Level = 0,
                Naam = c.Naam,
                HoofdCategorieId = null,
                Subcategorieen = c.SubCategorieen?.Select(c => MapToOverviewViewModel(c, 1)).ToList() ?? []
            }).ToList();

        return viewModel;

    }

    public async Task<IActionResult> Index()
    {
        return View(nameof(Index), await PrepareCategorieen());
    }

    [NonAction]
    public CategorieOverviewViewModel MapToOverviewViewModel(Categorie c,int level)
    {
        return new CategorieOverviewViewModel
        {
            CategorieId = c.CategorieId,
            Level = level,
            Naam = c.Naam,
            HoofdCategorieId = c.HoofdCategorieId,
            Subcategorieen = c.SubCategorieen?.Select(c => MapToOverviewViewModel(c,level+1)).ToList() ?? []
        };
    }

    public async Task<IActionResult> Add()
    {
        return View(new AddCategorieViewModel()
        {
            Subcategorieen = await PrepareCategorieen()
        });
    }
}
