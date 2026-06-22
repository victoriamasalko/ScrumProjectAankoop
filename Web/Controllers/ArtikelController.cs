using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    public class ArtikelController : Controller
    {

        public IActionResult Index()
        {
            return View();
        }
    }
}
