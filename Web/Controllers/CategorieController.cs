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

    public async Task<IActionResult> Index()
    {
        var categorieen = await _categorieService.GetCategorieenAsync();

        var viewModel = categorieen.Select(c => new CategorieOverviewViewModel
        {
            CategorieId = c.CategorieId,
            Naam = c.Naam,
            HoofdCategorieId = c.HoofdCategorieId,
            HoofdCategorieNaam = categorieen.FirstOrDefault(hc => hc.CategorieId == c.HoofdCategorieId)?.Naam
        }).ToList();   

        return View(nameof(Index), viewModel);
    }

    //GET-method met dropdown van alle beschikbare categorieën en een geen hoofdcategorieoptie. 
    public async Task<IActionResult> AddCategorie()
    {
        var categorieen = await _categorieService.GetCategorieenAsync();
        var model = new AddCategorieViewModel
        {
            BeschikbareCategorieen = categorieen
            .Select(c => new SelectListItem
            {
                Value = c.CategorieId.ToString(),
                Text = c.Naam
            })
            .ToList()
        };
        model.BeschikbareCategorieen.Insert(0, new SelectListItem
        {
            Value = "",
            Text = "Geen"
        });

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
                Naam = model.Naam,
                HoofdCategorieId = model.HoofdCategorieId
            };
        
            await _categorieService.AddCategorieAsync(categorie);
        }
        return RedirectToAction(nameof(Index));
    }


}
