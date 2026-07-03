using Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Service;
using Web.Models.ViewModels;

namespace Web.Controllers
{
    public class LeverancierController(LeverancierService leverancierService, PlaatsService plaatsService, ArtikelService artikelService) : Controller
    {
        //Overzicht van leveranciers tonen.
        //Je neemt de leveranciers uit de database via de service en stopt deze in de variabele leveranciers.
        //Deze wordt in de Leveranciers property van het ViewModel gestoken.

        
        public async Task<IActionResult> Index()
        {
            var leveranciers = await leverancierService.GetLeveranciersAsync();
            var artikels = await artikelService.GetArtikelsAsync();

            LeverancierIndexViewModel viewModel = new LeverancierIndexViewModel()
            {
                Leveranciers = leveranciers.Select(l => new LeverancierOverviewViewModel()
                {
                    LeveranciersId = l.LeveranciersId,
                    Naam = l.Naam,
                    BtwNummer = l.BtwNummer,
                    VoornaamContactpersoon = l.VoornaamContactpersoon,
                    FamilienaamContactpersoon = l.FamilienaamContactpersoon,
                    Artikels = l.Artikels

                }).ToList(),
                Artikels = artikels.ToList()
            };

            return View(nameof(Index), viewModel);

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

            return PartialView(leverancier);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var leverancier = await leverancierService.GetLeverancierByIdAsync(id);

            if (leverancier is null)
                return PartialView(nameof(Details));
            
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

            return PartialView("Edit", model);
        }

        [HttpPost]
        public async Task<IActionResult> SaveChanges(EditLeverancierViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Plaats = await GetPlaatsenAsync();

                return PartialView("_EditLeverancierForm", model);
            }

            var leverancier = new Leverancier
            {
                LeveranciersId = model.LeverancierId,
                Naam = model.Naam,
                BtwNummer = model.BtwNummer,
                Straat = model.Straat,
                HuisNummer = model.HuisNummer,
                Bus = model.Bus,
                PlaatsId = model.PlaatsId,
                VoornaamContactpersoon = model.VoornaamContactpersoon,
                FamilienaamContactpersoon = model.FamilienaamContactperoon
            };

            await leverancierService.UpdateLeverancierAsync(leverancier);

            return RedirectToAction(nameof(Index));
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
        public async Task<IActionResult> AddLeverancier()
        {
            return PartialView("AddLeverancierModal", new AddLeverancierViewModel()
            {
                Plaatsen = await GetPlaatsenAsync()
            });
        }

        // Deze action method voegt een nieuwe leverancier toe.
        [HttpPost]
        public async Task<IActionResult> AddLeverancier(AddLeverancierViewModel addLeverancierViewModel)
        {
            if (ModelState.IsValid)
            {
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

                await leverancierService.AddLeverancierAsync(leverancier);

                return RedirectToAction(nameof(Index));
            }

            addLeverancierViewModel.Plaatsen = await GetPlaatsenAsync();

            return PartialView("_AddLeverancierForm", addLeverancierViewModel);
        }
    }
}