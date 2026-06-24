using Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Service;
using Web.Models.ViewModels;

namespace Web.Controllers
{
    public class LeverancierController : Controller
    {
        private readonly LeverancierService leverancierService;
        private readonly PlaatsService plaatsService;
        public LeverancierController(LeverancierService leverancierService,PlaatsService plaatsService)
        {
            this.leverancierService = leverancierService;
            this.plaatsService = plaatsService;
        }
        
        public async Task<IActionResult> Index()
        {
            var leveranciers = await leverancierService.GetLeveranciersAsync();

            var viewModel = leveranciers.Select(l => new LeverancierOverviewViewModel
            {
                LeveranciersId = l.LeveranciersId,
                Naam = l.Naam,
                BtwNummer = l.BtwNummer,
                VoornaamContactpersoon = l.VoornaamContactpersoon,
                FamilienaamContactpersoon = l.FamilienaamContactpersoon
            }).ToList();

            return View(nameof(Index), viewModel);

        }

        // Deze method wordt gebruikt om de plaatsen als een select list te kunnen gebruiken.
        [NonAction]
        public async Task<List<SelectListItem>> GetPlaatsenAsync()
        {
            var plaatsen = await plaatsService.GetPlaatsenAsync();

            return plaatsen.Select(plaats => new SelectListItem()
            {
                Text = plaats.Naam,
                Value = plaats.PlaatsId.ToString()
            }).ToList();

        }

        // Deze action method geeft het formulier terug waarin een nieuwe leverancier aangemaakt wordt.
        public async Task<IActionResult> AddLeverancierAsync()
        {
            return View("AddLeverancierModal", new AddLeverancierViewModel()
            {
                Plaatsen = await GetPlaatsenAsync()
            });
        }
        public async Task<IActionResult> Details(int id)
        {
            var leverancier = await leverancierService.GetLeverancierByIdAsync(id);

            if (leverancier is null)
            {
                return NotFound();
            }

            return PartialView(leverancier);
        }
            // Deze action method voegt een nieuwe leverancier toe.
        [HttpPost]
        public async Task<IActionResult> AddLeverancierAsync(AddLeverancierViewModel addLeverancierViewModel)
        {
            if (ModelState.IsValid)
            {
                // Maak een Leverancier object op basis van de form.
                Leverancier leverancier = new Leverancier()
                {
                    Naam = addLeverancierViewModel.Naam,
                    BtwNummer = addLeverancierViewModel.BtwNummer,
                    Straat = addLeverancierViewModel.Straat,
                    HuisNummer = addLeverancierViewModel.HuisNummer,
                    Bus = addLeverancierViewModel.Bus,
                    VoornaamContactpersoon = addLeverancierViewModel.VoornaamContactpersoon,
                    FamilienaamContactpersoon = addLeverancierViewModel.FamilienaamContactperoon,
                    PlaatsId = addLeverancierViewModel.PlaatsId
                };

                // Roep de service op.
                await leverancierService.AddLeverancierAsync(leverancier);

                // Redirect naar de overzichtpagina voor de leveranciers.
                return RedirectToAction(nameof(Index));
            }
            else
            {
                addLeverancierViewModel.Plaatsen = await GetPlaatsenAsync();
                return View("AddLeverancierModal",addLeverancierViewModel);
            }
        }
    }
}
