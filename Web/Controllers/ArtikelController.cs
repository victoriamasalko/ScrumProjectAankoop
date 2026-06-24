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

        public ArtikelController(ArtikelService artikelService, LeverancierService leverancierService)
        {
            this.artikelService = artikelService;
            this.leverancierService = leverancierService;
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
                Leveranciers = await GetLeveranciersSelectListAsync()
            };

            return View(nameof(ArtikelToevoegen), viewModel);
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
    }
}
