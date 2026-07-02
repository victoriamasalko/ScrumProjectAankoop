using Microsoft.AspNetCore.Mvc;
using Web.Models.ViewModels;

namespace Web.ViewComponents
{
    public class ArtikelRowViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(ArtikelOverviewViewModel artikel)
        {
            return View(artikel);
        }
    }
}
