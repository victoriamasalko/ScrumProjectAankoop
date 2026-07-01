using Microsoft.AspNetCore.Mvc;
using Data.Models;
using Service;
using Web.Models.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;

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

    //GET-method met dropdown van alle beschikbare categorieën en een geen hoofdcategorieoptie. 
    public async Task<IActionResult> AddCategorie()
    {
        var subcategorieen = await PrepareCategorieen();

        var model = new AddCategorieViewModel
        {
            Subcategorieen = subcategorieen
        };

        //model.BeschikbareCategorieen.Insert(0, new SelectListItem
        //{
        //    Value = "",
        //    Text = "Geen"
        //});

        return View(model);
    }

    [HttpPost]
    public async Task<ActionResult> AddCategorie(AddCategorieViewModel model)
    {
        if (ModelState.IsValid)
        {
            /* model.BeschikbareCategorieen = (await _categorieService.GetCategorieenAsync())
                 .Select(c => new SelectListItem
                 {
                     Value = c.CategorieId.ToString(),
                     Text = c.Naam
                 })
                 .ToList();
             return View(model);*/
        

            var categorie = new Categorie
            {
                Naam = model.NieuweNaam,
                HoofdCategorieId = model.SelectedCategorieId
            };
        
            await _categorieService.AddCategorieAsync(categorie);
        }
        return RedirectToAction(nameof(Index));
    }


}
