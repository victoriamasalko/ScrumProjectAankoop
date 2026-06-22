using Data.Models;
using Microsoft.AspNetCore.Mvc;
using Web.Models.ViewModels;

namespace Web.Controllers
{
    public class LeverancierController : Controller
    {
        private readonly LeverancierService leverancierService;
        public LeverancierController(LeverancierService leverancierService)
        {
            this.leverancierService = leverancierService;
        }
        //Overzicht van de leveranciers tonen
        public async Task<IActionResult> Index()
        {
            LeverancierOverviewViewModel viewModel = await GetLeveranciers();
            return View(viewModel);
        }
        //Methode GetLeveranciers met lijst van Leveranciers
        public async Task <LeverancierOverviewViewModel> GetLeveranciers()
        {
            IEnumerable<Leverancier> leveranciers = await leverancierService.GetLeveranciers();

            return new LeverancierOverviewViewModel
            {
                Leveranciers = leveranciers
            };
        }

    }
}
