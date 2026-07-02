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
                Level = 0,
                Naam = c.Naam,
                HoofdCategorieId = null,
                Subcategorieen = c.SubCategorieen?.Select(c => MapToOverviewViewModel(c,1)).ToList() ?? []
            }).ToList();

        return View(nameof(Index), viewModel);
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

    public async Task<IActionResult> Delete(int id)
    {
        // Vraag de categorie die je wilt verwijderen op.
        Categorie? categorie = await _categorieService.GetCategorieByIdAsync(id);

        // Return NotFound als de categorie niet bestaat.
        if (categorie == null)
            return NotFound();

        // Roep de delete method op.
        var deletedCategorie = await _categorieService.RemoveCategorieAsync(categorie);

        return RedirectToAction(nameof(Index));
    }
}
