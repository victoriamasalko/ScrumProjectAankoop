using Microsoft.AspNetCore.Mvc;
using Service;
﻿using Data.Models;
using Web.Models.ViewModels;

namespace Web.Controllers;

public class ActiecodeController : Controller
{
    private readonly ActiecodeService actiecodeService;

    public ActiecodeController(ActiecodeService actiecodeService)
    {
        this.actiecodeService = actiecodeService;
    }

    public async Task<IActionResult> Index()
    {
        var actiecodes = await actiecodeService.GetActiecodesAsync();

        return View(nameof(Index), actiecodes);
    }

    [HttpGet]
    public async Task<IActionResult> Toevoegen()
    {
        var viewModel = new ActiecodeViewModel { };

        return View(nameof(Toevoegen), viewModel);
    }

    [HttpPost]
    public async Task<IActionResult> ToevoegenUitvoeren(ActiecodeViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(nameof(Toevoegen), model);
        }

        var actiecode = new Actiecode
        {
            Naam = model.Naam,
            GeldigVanDatum = model.GeldigVanDatum,
            GeldigTotDatum = model.GeldigTotDatum,
            IsEenmalig = model.IsEenmalig
        };

        await actiecodeService.AddActiecodeAsync(actiecode);

        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> ActiecodeWijzigen(int id)
    {
        var actiecode = await actiecodeService.GetActiecodeByIdAsync(id);
        
        var model = new ActiecodeViewModel
        {
            Id = actiecode.ActiecodeId,
            Naam = actiecode.Naam,
            GeldigVanDatum = actiecode.GeldigVanDatum,
            MoetGeldigVanDatumGevalideerdWorden = (actiecode.GeldigVanDatum <= DateTime.Today),
            GeldigTotDatum = actiecode.GeldigTotDatum,
            IsEenmalig = actiecode.IsEenmalig
        };
        
        return View(model);
    }
    
    [HttpPost]
    public async Task<IActionResult> ActiecodeWijzigenUitvoeren(ActiecodeViewModel model)
    {
        if (model.MoetGeldigVanDatumGevalideerdWorden && model.GeldigVanDatum <= DateTime.Today)
        {
            ModelState.AddModelError("GeldigVanDatum", "De startdatum mag niet in het verleden liggen.");
        }

        if (!ModelState.IsValid)
            return RedirectToAction(nameof(ActiecodeWijzigen));

        var actiecode = new Actiecode()
        {
            ActiecodeId = model.Id,
            Naam = model.Naam,
            GeldigVanDatum = model.GeldigVanDatum,
            GeldigTotDatum = model.GeldigTotDatum,
            IsEenmalig = model.IsEenmalig
        };
    

        await actiecodeService.UpdateActiecodeAsync(actiecode);

        return RedirectToAction(nameof(Index));
    }
    
    
}