using Data.Models;
using Microsoft.AspNetCore.Mvc;
using Service;
using Web.Models;
using Web.Models.ViewModels;

namespace Web.Controllers
{
    public class ArtikelController(ArtikelService artikelService, CategorieService categorieService) : Controller
    {
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

        public async Task<IActionResult> Details(int id)
        {
            var artikel = await artikelService.GetArtikelAsync(id);
            var viewmodel = new ArtikelDetailsViewModel
            {
                ArtikelId = artikel.ArtikelId,
                Ean = artikel.Ean,
                Naam = artikel.Naam,
                Beschrijving = artikel.Beschrijving,
                Prijs = artikel.Prijs,
                GewichtInGram = artikel.GewichtInGram,
                Bestelpeil = artikel.Bestelpeil,
                Voorraad = artikel.Voorraad,
                MaximumVoorraad = artikel.MaximumVoorraad,
                MinimumVoorraad = artikel.MinimumVoorraad,
                Levertijd = artikel.Levertijd,
                AantalBesteldLeverancier = artikel.AantalBesteldLeverancier,
                MaxAantalInMagazijnPlaats = artikel.MaxAantalInMagazijnPlaats,
                LeveranciersId = artikel.LeveranciersId,
                Leveranciers = artikel.Leveranciers
            };
            
            foreach (var categorie in artikel.Categories)
            {
                var hoofdCategorieMetSubCategorie = new HoofdCategorieMetSubCategorie
                {
                    Hoofdcategorie = await categorieService.GetHoofdcategorieByCategorieIdAsync(categorie.CategorieId),
                    Subcategorie = categorie.Naam
                };
                viewmodel.CategorieStructuren.Add(hoofdCategorieMetSubCategorie);
            }
            
            return View(viewmodel);
        }
    }
}