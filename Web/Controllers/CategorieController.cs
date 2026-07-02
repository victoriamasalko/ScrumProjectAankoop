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
    public CategorieOverviewViewModel MapToOverviewViewModel(Categorie c, int level)
    {
        return new CategorieOverviewViewModel
        {
            CategorieId = c.CategorieId,
            Level = level,
            Naam = c.Naam,
            HoofdCategorieId = c.HoofdCategorieId,
            Subcategorieen = c.SubCategorieen?.Select(c => MapToOverviewViewModel(c, level + 1)).ToList() ?? []
        };
    }
    public async Task<IActionResult>EditCategorie(int id)
    {
        var categorie = await _categorieService.GetCategorieByIdAsync(id);
        if (categorie == null)
        {
            return NotFound();
        }
        var subcategorieen = (await PrepareCategorieen()).ToList();
        var model = new EditCategorieViewModel
        {
            CategorieId = categorie.CategorieId,
            NieuweNaam = categorie.Naam,
            SelectedCategorieId = categorie.HoofdCategorieId,
            Subcategorieen = subcategorieen
        };
        return View(model);
    }
    [HttpPost]
    public async Task<IActionResult>EditCategorie(EditCategorieViewModel model)
    {
        var bestaandeCategorie = await _categorieService.GetCategorieByNaamAsync(model.NieuweNaam);
        if (bestaandeCategorie != null && bestaandeCategorie.CategorieId != model.CategorieId)
        {
            ModelState.AddModelError("NieuweNaam", $"Er bestaat al een categorie met de naam \"{model.NieuweNaam}\"");
        }

        if (!ModelState.IsValid)
        {
            model.Subcategorieen = await PrepareCategorieen();
            return View(model);
        }

        var categorie = await _categorieService.GetCategorieByIdAsync(model.CategorieId);
        if (categorie == null)
        {
            return NotFound();
        }

        categorie.Naam = model.NieuweNaam;
        categorie.HoofdCategorieId = model.SelectedCategorieId == 0
            ? null : model.SelectedCategorieId;
        await _categorieService.UpdateCategorieAsync(categorie);
        return RedirectToAction(nameof(Index));
    }
}
