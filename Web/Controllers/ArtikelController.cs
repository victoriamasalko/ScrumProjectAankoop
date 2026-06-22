using Data.Models;
using Microsoft.AspNetCore.Mvc;
using Service;
using Web.Models;

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
            var artikelen = await artikelService.GetArtikelsAsync();
            return View(artikelen.Select(artikel => new ArtikelOverviewViewModel()
            {
                ArtikelId = artikel.ArtikelId,
                Ean = artikel.Ean,
                AantalBesteldLeverancier = artikel.AantalBesteldLeverancier,
                Naam = artikel.Naam,
                Voorraad = artikel.Voorraad,
                Prijs = artikel.Prijs,
                Leverancier = artikel.Leveranciers.Naam,
                Categories = artikel.Categories.Select(c => c.Naam).ToList()
            }));
        }
    }
}
