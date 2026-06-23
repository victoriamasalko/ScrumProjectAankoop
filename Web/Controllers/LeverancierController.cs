using Data.Models;
using Microsoft.AspNetCore.Mvc;
using Web.Models.ViewModels;
using Service;

namespace Web.Controllers
{
    public class LeverancierController : Controller
    {
        private readonly LeverancierService leverancierService;
        public LeverancierController(LeverancierService leverancierService)
        {
            this.leverancierService = leverancierService;
        }
        //Overzicht van leveranciers tonen.
        //Je neemt de leveranciers uit de database via de service en stopt deze in de variabele leveranciers.
        //Deze wordt in de Leveranciers property van het ViewModel gestoken.
        public async Task<IActionResult> Index()
        {
            var leveranciers = await leverancierService.GetLeveranciersAsync();

            var viewModel = new LeverancierOverviewViewModel
            {
                Leveranciers = leveranciers
            };

            return View(viewModel);

        }
    }
}
