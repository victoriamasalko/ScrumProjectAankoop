using Microsoft.AspNetCore.Mvc;
using Web.Models.ViewModels;

namespace Web.ViewComponents
{
    public class LeverancierRowViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(LeverancierOverviewViewModel leverancier)
        {
            return View(leverancier);
        }
    }
}
