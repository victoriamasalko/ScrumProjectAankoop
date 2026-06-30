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

    public async Task<IActionResult> ActiecodeWijzigen(int id)
    {
        var actiecode = await actiecodeService.GetActiecodeByIdAsync(id);
        
        var model = new ActiecodeViewModel
        {
            ActiecodeId = actiecode.ActiecodeId,
            Naam = actiecode.Naam,
            GeldigVanDatum = actiecode.GeldigVanDatum,
            GeldigTotDatum = actiecode.GeldigTotDatum,
            IsEenmalig = actiecode.IsEenmalig
        };
        
        return View(model);
    }
    
    [HttpPost]
    public async Task<IActionResult> ActiecodeWijzigenUitvoeren(ActiecodeViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(nameof(ActiecodeWijzigen), model);
        }

        var actiecode = new Actiecode()
        {
            ActiecodeId = model.ActiecodeId,
            Naam = model.Naam,
            GeldigVanDatum = model.GeldigVanDatum,
            GeldigTotDatum = model.GeldigTotDatum,
            IsEenmalig = model.IsEenmalig
        };

        await actiecodeService.UpdateActiecodeAsync(actiecode);

        return RedirectToAction(nameof(Index));
    }
}