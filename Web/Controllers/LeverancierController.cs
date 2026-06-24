using Data.Models;
using Microsoft.AspNetCore.Mvc;
using Service;
using Web.Models.ViewModels;
using Service;

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

        //Details van een leverancier tonen.
        //Je neemt de leverancier uit de database via de service en sla het op in de variabele leverancier.
        //Als de leverancier niet wordt gevonden, word je doorgestuurd naar een 'Not Found'-pagina.
        //Als de leverancier wordt gevonden, wordt je doorgestuurd naar de bijbehorende view.
        public async Task<IActionResult> Details(int id)
        {
            var leverancier = await leverancierService.GetLeverancierByIdAsync(id);

            if (leverancier is null)
            {
                return NotFound();
            }

            return View(leverancier);
        }
    }
}
