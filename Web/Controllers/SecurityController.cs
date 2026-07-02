using BCrypt;
using Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using Service;
using System.Security.Principal;
using Web.Models.ViewModels;

namespace Web.Controllers;

public class SecurityController : Controller
{
    private readonly SecurityService _securityService;

    public SecurityController(SecurityService securityService)
    {
        _securityService = securityService;
    }

    public async Task<IActionResult> Details(int id)
    {
        var account = await _securityService.GetPersoneelslidaccountAsync(id);
        
        if (account == null)
        {
            return NotFound();
        }

        var viewModel = new PersoneelslidaccountDetailsViewModel
        {
            PersoneelslidAccountId = account.PersoneelslidAccountId,
            PersoneelslidId = account.Personeelslid.PersoneelslidId,
            PersoneelslidNaam = string.Join(" ", account.Personeelslid.Voornaam, account.Personeelslid.Familienaam),
            Emailadres = account.Emailadres,
            Paswoord = account.Paswoord
        };

        return View(viewModel);
    }

    public async Task<IActionResult> Aanmelden()
    {
        var viewModel = new PersoneelslidaccountDetailsViewModel();
        return View(viewModel);
    }

    [HttpPost]
    public async Task<IActionResult> Aanmelden(PersoneelslidaccountDetailsViewModel personeelslidaccountDetailsViewModel)
    {
        var account = await _securityService.GetPersoneelslidaccountByEmailAsync(personeelslidaccountDetailsViewModel.Emailadres);
        if (account == null)
        {
            return NotFound();
        }


        var wachtwoord = personeelslidaccountDetailsViewModel.Paswoord;
        bool isWachtwoordCorrect = BCrypt.Net.BCrypt.Verify(wachtwoord, account.Paswoord);


        if (isWachtwoordCorrect && account != null)
        {
            var viewModel = new PersoneelslidaccountDetailsViewModel
            {
                PersoneelslidAccountId = account.PersoneelslidAccountId,
                PersoneelslidId = account.Personeelslid.PersoneelslidId,
                PersoneelslidNaam = string.Join(" ", account.Personeelslid.Voornaam, account.Personeelslid.Familienaam),
                Emailadres = account.Emailadres,
                Paswoord = account.Paswoord
            };

            // Maak handmatig de cookie aan
            CookieOptions opties = new CookieOptions
            {
                HttpOnly = true, // Zorgt dat JavaScript de cookie niet kan stelen
                Secure = true,   // Alleen over HTTPS verzenden
                Expires = DateTimeOffset.UtcNow.AddMinutes(20) // 20 minuten geldig
            };

            HttpContext.Response.Cookies.Append("AangemeldPersoneel", account.Emailadres, opties);

            return RedirectToAction("Index", "Home");
        }
        else
        {
            return NotFound();
        }
    }

    [HttpPost]
    public async Task<IActionResult> Afmelden()
    {
        HttpContext.Response.Cookies.Delete("AangemeldPersoneel");
        return RedirectToAction("Index", "Home");
    }
}
