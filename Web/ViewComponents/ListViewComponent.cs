using Microsoft.AspNetCore.Mvc;


namespace Web.ViewComponents
{
    public class ListViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(IEnumerable<object> model)
        {
            return View(model);
        }
    }
}
