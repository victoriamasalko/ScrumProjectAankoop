using Data.Models;
using Microsoft.AspNetCore.Mvc;

namespace Web.ViewComponents
{
    public class CategorieBlobViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(IEnumerable<Categorie> categorieen)
        {
            return View(categorieen);
        }
    }
}
