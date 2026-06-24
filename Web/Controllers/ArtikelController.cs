using Data.Models;
using Microsoft.AspNetCore.Mvc;
using Service;
using Web.Models;
using System.Linq;
using Web.Models.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Web.Controllers
{
    public class ArtikelController : Controller
    {
        private readonly ArtikelService artikelService;
        private readonly LeverancierService leverancierService;
        private readonly CategorieService categorieService;

        public ArtikelController(ArtikelService artikelService, LeverancierService leverancierService, CategorieService categorieService)
        {
            this.artikelService = artikelService;
            this.leverancierService = leverancierService;
            this.categorieService = categorieService;
        }

        public async Task<IActionResult> Index()
        {
            var artikels = await artikelService.GetArtikelsAsync();

            var viewModel = artikels.Select(a => new ArtikelOverviewViewModel
            {
                ArtikelId = a.ArtikelId,
                Naam = a.Naam,
                Beschrijving = a.Beschrijving,
                Ean = a.Ean,
                Categorieen = a.Categorieen.Select(c => c.Naam).ToList(),
                Prijs = a.Prijs,
                Voorraad = a.Voorraad
            }).ToList();

            return View(nameof(Index), viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> ArtikelToevoegen()
        {
            var viewModel = new ArtikelToevoegenViewModel
            {
                Leveranciers = await GetLeveranciersSelectListAsync(),
                Categorieen = await GetCategorieenSelectLystAsync()

            };

            return View(nameof(ArtikelToevoegen), viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> ArtikelToevoegenUitvoeren(ArtikelToevoegenViewModel model)
        {
            if(!ModelState.IsValid)
            {
                model.Leveranciers = await GetLeveranciersSelectListAsync();
                model.Categorieen = await GetCategorieenSelectLystAsync();

                return View(nameof(ArtikelToevoegen), model);
            }

            var artikel = new Artikel
            {
                Naam = model.Naam,
                Beschrijving = model.Beschrijving,
                Prijs = model.Prijs,
                GewichtInGram = model.GewichtInGram,
                Levertijd = model.Levertijd,
                MaxAantalInMagazijnPlaats = model.MaxAantalInMagazijnPlaats,
                LeveranciersId = model.LeverancierId.Value,

                Bestelpeil = 0,
                Voorraad = 0,
                MinimumVoorraad = 0,
                MaximumVoorraad = 0,
                AantalBesteldLeverancier = 0
            };

            await artikelService.AddArtikelAsync(artikel, model.SelectedCategorieIds);

            return RedirectToAction(nameof(Index));
        }

        public async Task<IEnumerable<SelectListItem>> GetLeveranciersSelectListAsync()
        {
            var leveranciers = await leverancierService.GetLeveranciersAsync();

            return leveranciers.Select(l => new SelectListItem
            {
                Value = l.LeveranciersId.ToString(),
                Text = l.Naam
            });

        }

        public async Task<IEnumerable<SelectListItem>> GetCategorieenSelectLystAsync()
        {
            var categorieen = await categorieService.GetCategorieenAsync();

            return categorieen.Select(c => new SelectListItem
            {
                Value = c.CategorieId.ToString(),
                Text = c.Naam
            });
        }
    }
}
