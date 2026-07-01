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

    // Toont een lijst van alle actiecodes
    public async Task<IActionResult> Index()
    {
        var actiecodes = await actiecodeService.GetActiecodesAsync();

        return View(nameof(Index), actiecodes);
    }

    // Toont het formulier om een nieuwe actiecode toe te voegen
    [HttpGet]
    public async Task<IActionResult> Toevoegen()
    {
        var viewModel = new ActiecodeViewModel { };

        return View(nameof(Toevoegen), viewModel);
    }

    // Verwerkt het formulier voor het toevoegen van een actiecode
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

    // Toont het formulier om een bestaande actiecode te wijzigen
    public async Task<IActionResult> ActiecodeWijzigen(int id)
    {
        var actiecode = await actiecodeService.GetActiecodeByIdAsync(id);
        
        var model = new ActiecodeViewModel
        {
            Id = actiecode.ActiecodeId,
            Naam = actiecode.Naam,
            GeldigVanDatum = actiecode.GeldigVanDatum,
            GeldigTotDatum = actiecode.GeldigTotDatum,
            IsEenmalig = actiecode.IsEenmalig
        };
        
        return View(model);
    }

    // Verwerkt het formulier voor het wijzigen van een actiecode
    [HttpPost]
    public async Task<IActionResult> ActiecodeWijzigenUitvoeren(ActiecodeViewModel model)
    {
        if (!ModelState.IsValid)
            return View(nameof(ActiecodeWijzigen));

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