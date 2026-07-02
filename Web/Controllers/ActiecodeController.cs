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
        var viewModel = new ActiecodeToevoegenViewModel { };

        return View(nameof(Toevoegen), viewModel);
    }

    // Verwerkt het formulier voor het toevoegen van een actiecode
    [HttpPost]
    public async Task<IActionResult> ToevoegenUitvoeren(ActiecodeToevoegenViewModel model)
    {
        if (ModelState.IsValid)
        {
            var actiecode = new Actiecode
            {
                Naam = model.Naam,
                GeldigVanDatum = model.GeldigVanDatum!.Value,
                GeldigTotDatum = model.GeldigTotDatum!.Value,
                IsEenmalig = model.IsEenmalig
            };

            await actiecodeService.AddActiecodeAsync(actiecode);

            return RedirectToAction(nameof(Index));
        }

        return PartialView("_AddActiecodeForm", model);
    }

    // Toont het formulier om een bestaande actiecode te wijzigen
    public async Task<IActionResult> ActiecodeWijzigen(int id)
    {
        var actiecode = await actiecodeService.GetActiecodeByIdAsync(id);

        var model = new ActiecodeWijzigenViewModel
        {
            Id = actiecode.ActiecodeId,
            Naam = actiecode.Naam,
            GeldigVanDatum = actiecode.GeldigVanDatum,
            GeldigTotDatum = actiecode.GeldigTotDatum,
            IsActief = (actiecode.GeldigVanDatum <= DateTime.Today),
            IsEenmalig = actiecode.IsEenmalig
        };

        return PartialView(model);
    }

    // Verwerkt het formulier voor het wijzigen van een actiecode
    [HttpPost]
    public async Task<IActionResult> ActiecodeWijzigenUitvoeren(ActiecodeWijzigenViewModel model)
    {
        if (model.GeldigVanDatum.HasValue && model.GeldigTotDatum.HasValue)
        {
            if (model.GeldigTotDatum.Value < model.GeldigVanDatum.Value)
            {
                ModelState.AddModelError(
                    nameof(model.GeldigTotDatum),
                    "Einddatum mag niet voor startdatum liggen!");
            }

            if (!model.IsActief && model.GeldigVanDatum.Value < DateTime.Today)
            {
                ModelState.AddModelError(
                    nameof(model.GeldigVanDatum),
                    "Startdatum mag niet in het verleden liggen!");
            }

            if (model.GeldigTotDatum.Value < DateTime.Today)
            {
                ModelState.AddModelError(
                    nameof(model.GeldigTotDatum),
                    "Einddatum mag niet in het verleden liggen!");
            }
        }

        if (ModelState.IsValid)
        {
            var actiecode = new Actiecode()
            {
                ActiecodeId = model.Id,
                Naam = model.Naam,
                GeldigVanDatum = model.GeldigVanDatum!.Value,
                GeldigTotDatum = model.GeldigTotDatum!.Value,
                IsEenmalig = model.IsEenmalig
            };
            
            await actiecodeService.UpdateActiecodeAsync(actiecode);

            return RedirectToAction(nameof(Index));
        }

        return PartialView("_EditActiecodeForm", model);
    }
}