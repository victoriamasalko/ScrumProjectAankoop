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
        private readonly IWebHostEnvironment webHostEnvironment;

        public ArtikelController(ArtikelService artikelService,IWebHostEnvironment webHostEnvironment)
        {
            this.artikelService = artikelService;
            this.webHostEnvironment = webHostEnvironment;
        }

        [HttpPost]
        public async Task<IActionResult> FotoUpload(IFormFile file,int artikelId,string beschrijving)
        {
            try
            {

                if (file != null && Path.GetExtension(file.FileName) == ".jpg")
                {
                    // Stel de naam van de file in.
                    var fileName = artikelId + "_" + beschrijving + ".jpg";

                    // Stel in waar de file moet worden opgeslagen => ...wwwroot/images/artikels
                    var uploadFolder = Path.Combine(webHostEnvironment.WebRootPath, "images", "artikels");
                    var filePath = Path.Combine(uploadFolder, fileName);

                    using var fs = new FileStream(filePath, FileMode.Create);

                    await file.CopyToAsync(fs);

                    return Ok(filePath);
                }

                return Problem();
            }
            catch (Exception ex)
            {
                return Problem();
            }
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
