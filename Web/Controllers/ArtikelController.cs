using Microsoft.AspNetCore.Mvc;
using Service;

namespace Web.Controllers
{
    public class ArtikelController : Controller
    {
        private readonly ArtikelService artikelService;

        public ArtikelController(ArtikelService artikelService)
        {
            this.artikelService = artikelService;
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
