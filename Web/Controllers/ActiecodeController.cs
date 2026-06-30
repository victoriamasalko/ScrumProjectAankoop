using Microsoft.AspNetCore.Mvc;
using Service;
﻿using Data.Models;

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

    public async Task<IActionResult> Wijzigen(int id)
    {
        return View();
    }
}