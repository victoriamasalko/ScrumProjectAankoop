using Microsoft.AspNetCore.Mvc;

namespace Web.Models.ViewModels
{
    public class ArtikelRowViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(ArtikelOverviewViewModel artikel)
        {
            return View(artikel);
        }
    }
}
