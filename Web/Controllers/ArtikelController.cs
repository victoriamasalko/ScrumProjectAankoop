using Data.Models;
using Microsoft.AspNetCore.Mvc;
using Service;
using Web.Models;
using System.Linq;
using Web.Models.ViewModels;

namespace Web.Controllers
{
    public class ArtikelController : Controller
    {
        private readonly ArtikelService artikelService;

        public ArtikelController(ArtikelService artikelService)
        {
            this.artikelService = artikelService;
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
    }
}
