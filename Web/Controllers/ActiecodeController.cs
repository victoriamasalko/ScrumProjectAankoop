using Data.Models;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    public class ActiecodeController : Controller
    {
        public IActionResult Index()
        {
            var code1 = new Actiecode
            {
                Naam = "Actie1",
                GeldigVanDatum = DateTime.Today,
                GeldigTotDatum = DateTime.Today.AddMonths(1),
                IsEenmalig = true
            };

            var code2 = new Actiecode
            {
                Naam = "Actie2",
                GeldigVanDatum = DateTime.Today,
                GeldigTotDatum = DateTime.Today.AddMonths(2),
                IsEenmalig = false
            };

            var lijst = new List<Actiecode>();

            if (lijst == null)
                throw new ArgumentNullException(nameof(lijst));

            lijst.Add(code1);
            lijst.Add(code2);


            return View(lijst);
        }
    }
}