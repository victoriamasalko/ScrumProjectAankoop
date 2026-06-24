using Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Service;
using Web.Models.ViewModels;

namespace Web.Controllers
{
    public class LeverancierController(LeverancierService leverancierService, PlaatsService plaatsService) : Controller
    {
        //Overzicht van leveranciers tonen.
        //Je neemt de leveranciers uit de database via de service en stopt deze in de variabele leveranciers.
        //Deze wordt in de Leveranciers property van het ViewModel gestoken.
        public async Task<IActionResult> Index()
        {
            var leveranciers = await leverancierService.GetLeveranciersAsync();

            var viewModel = new LeverancierOverviewViewModel
            {
                Leveranciers = leveranciers
            };

            return View(viewModel);
        }

        //Details van een leverancier tonen.
        //Je neemt de leverancier uit de database via de service en sla het op in de variabele leverancier.
        //Als de leverancier niet wordt gevonden, word je doorgestuurd naar een 'Not Found'-pagina.
        //Als de leverancier wordt gevonden, wordt je doorgestuurd naar de bijbehorende view.
        //[HttpGet("Details/{id:int}")]
        public async Task<IActionResult> Details(int id)
        {
            var leverancier = await leverancierService.GetLeverancierByIdAsync(id);

            if (leverancier is null)
            {
                return NotFound();
            }

            return View(leverancier);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var leverancier = await leverancierService.GetLeverancierByIdAsync(id);

            if (leverancier is null)
                return View(nameof(Details));
            
            var model = new EditLeverancierViewModel
            {
                LeverancierId = leverancier.LeveranciersId,
                Naam = leverancier.Naam,
                BtwNummer = leverancier.BtwNummer,
                Straat = leverancier.Straat,
                HuisNummer = leverancier.HuisNummer,
                Bus = leverancier.Bus,
                PlaatsId = leverancier.PlaatsId,
                Plaats = await GetPlaatsenAsync(),
                VoornaamContactpersoon = leverancier.VoornaamContactpersoon,
                FamilienaamContactperoon = leverancier.FamilienaamContactpersoon
            };
            
            return View(model);
        }

        public async Task<IActionResult> SaveChanges(EditLeverancierViewModel editLeverancierViewModel)
        {
            if (!ModelState.IsValid)
                return RedirectToAction(nameof(Details), new { id = editLeverancierViewModel.LeverancierId });

            Leverancier leverancier = new Leverancier()
            {
                LeveranciersId = editLeverancierViewModel.LeverancierId,
                Naam = editLeverancierViewModel.Naam,
                BtwNummer = editLeverancierViewModel.BtwNummer,
                Straat = editLeverancierViewModel.Straat,
                HuisNummer = editLeverancierViewModel.HuisNummer,
                Bus = editLeverancierViewModel.Bus,
                PlaatsId = editLeverancierViewModel.PlaatsId,
                VoornaamContactpersoon = editLeverancierViewModel.VoornaamContactpersoon,
                FamilienaamContactpersoon = editLeverancierViewModel.FamilienaamContactperoon,
            };
            await leverancierService.UpdateLeverancierAsync(leverancier);

            return RedirectToAction(nameof(Details), new { id = editLeverancierViewModel.LeverancierId });
        }


        // Deze method wordt gebruikt om de plaatsen als een select list te kunnen gebruiken.
        [NonAction]
        private async Task<List<SelectListItem>> GetPlaatsenAsync()
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
                return View("AddLeverancierModal", addLeverancierViewModel);
            }
        }
    }
}