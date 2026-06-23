using Data.Models;
using Microsoft.AspNetCore.Mvc;
using Web.Models.ViewModels;

namespace Web.Controllers
{
    public class LeverancierController : Controller
    {
        private readonly LeverancierService leverancierService;
        public LeverancierController(LeverancierService leverancierService)
        {
            this.leverancierService = leverancierService;
        }
        
        public async Task<IActionResult> Index()
        {
            var leveranciers = await leverancierService.GetLeveranciersAsync();

            var viewModel = leveranciers.Select(l => new LeverancierOverviewViewModel
            {
                Naam = l.Naam,
                BtwNummer = l.BtwNummer,
                VoornaamContactpersoon = l.VoornaamContactpersoon,
                FamilienaamContactpersoon = l.FamilienaamContactpersoon
            }).ToList();

            return View(nameof(Index), viewModel);

        }
    }
}
