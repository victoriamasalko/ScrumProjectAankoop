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
}
    