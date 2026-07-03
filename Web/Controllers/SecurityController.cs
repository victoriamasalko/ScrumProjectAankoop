using BCrypt;
using Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.View;
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
        var viewModel = new AanmeldenViewModel();
        return View(viewModel);
    }

    [HttpPost]
    public async Task<IActionResult> Aanmelden(AanmeldenViewModel aanmeldenViewModel)
    {
        if (ModelState.IsValid)
        {
            var account = await _securityService.GetPersoneelslidaccountByEmailAsync(aanmeldenViewModel.Emailadres)!;
            if (account == null)
            {
                return NotFound();
            }


            var wachtwoord = aanmeldenViewModel.Paswoord;
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
                ModelState.AddModelError("Paswoord", "Het e-mailadres of wachtwoord is ongeldig");
                return View(aanmeldenViewModel);
            }
        }
        else
        {
            return View(aanmeldenViewModel);
        }
    }

    [HttpPost]
    public async Task<IActionResult> Afmelden()
    {
        HttpContext.Response.Cookies.Delete("AangemeldPersoneel");
        return RedirectToAction("Index", "Home");
    }

    public async Task<IActionResult> WachtwoordWijzigen()
    {
        var modelView = new WijzigenWachtwoordViewModel();
        return View(modelView);
    }

    [HttpPost]
    public async Task<IActionResult> WachtwoordWijzigen(WijzigenWachtwoordViewModel wijzigenWachtwoordViewModel)
    {
        try
        {
            if (ModelState.IsValid)
            {
                var account = await _securityService.GetPersoneelslidaccountByEmailAsync(wijzigenWachtwoordViewModel.Emailadres)!;
                if (account == null)
                {
                    ModelState.AddModelError("Emailadres", "Ongeldig e-mailadres");
                    return View(wijzigenWachtwoordViewModel);
                }

                bool isOudWachtwoordCorrect = BCrypt.Net.BCrypt.Verify(wijzigenWachtwoordViewModel.OudPaswoord, account.Paswoord);
                bool isNieuwWachtwoordCorrect = await this.ValidateNieuwWachtwoord(wijzigenWachtwoordViewModel.NieuwPaswoord, account);

                if (isOudWachtwoordCorrect && isNieuwWachtwoordCorrect)
                {
                    account.Paswoord = BCrypt.Net.BCrypt.HashPassword(wijzigenWachtwoordViewModel.NieuwPaswoord);
                    await _securityService.UpdatePersoneelslidaccountAsync(account);

                    // Oud en nieuw wachtwoord geldig, update wachtwoord gelukt
                    TempData["SuccessUpdatePasswordMessage"] = "Uw wachtwoord werd met succes gewijzigd !";
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    if (!isOudWachtwoordCorrect)
                    {
                        ModelState.AddModelError("OudPaswoord", "Oud wachtwoord is ongeldig !");
                    }
                    if (!isNieuwWachtwoordCorrect)
                    {
                        ModelState.AddModelError("NieuwPaswoord", "Nieuw wachtwoord voldoet niet aan de voorwaarden !");
                    }

                }

                // Oud of niew wachtwoord is ongeldig
                return View(wijzigenWachtwoordViewModel);
            }
            else
            {
                return View(wijzigenWachtwoordViewModel);
            }
        }
        catch(Exception)
        {
            ModelState.AddModelError("NiewPaswoord", "Er ging iets fout bij het updaten van het nieuwe paswoord, wijziging is NIET uitgevoerd !");
            return View(wijzigenWachtwoordViewModel);
        }
    }

    [NonAction]
    public async Task<bool> ValidateNieuwWachtwoord(string wachtwoord, Personeelslidaccount account)
    {
        bool result = true;

        if ((wachtwoord == null) || (account == null))
        {
            result = false;
        }
        else
        {
            result = (wachtwoord == null) ? false : result;
            result = (account == null) ? false : result;

            result = (BCrypt.Net.BCrypt.Verify(wachtwoord, account.Paswoord) == false) && result;
        }
        return result;
    }
}
