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
}
