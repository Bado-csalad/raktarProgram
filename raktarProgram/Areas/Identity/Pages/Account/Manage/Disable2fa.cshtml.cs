using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Serilog;

namespace raktarProgram.Areas.Identity.Pages.Account.Manage
{
    public class Disable2faModel : PageModel
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ILogger logger;

        public Disable2faModel(
            UserManager<IdentityUser> userManager,
            ILogger logger)
        {
            _userManager = userManager;
            this.logger = logger;
        }

        [TempData]
        public string StatusMessage { get; set; }

        public async Task<IActionResult> OnGet()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                logger.Warning("{userId} felhasználót nem lehetett betöltenim Disable2Fa--OnGet", _userManager.GetUserId(User));
                return NotFound($"Felhasználói adatokat nem tudtuk betölteni '{_userManager.GetUserId(User)}'.");
            }

            if (!await _userManager.GetTwoFactorEnabledAsync(user))
            {
                throw new InvalidOperationException($"Nem tudod kikapcsolni a két lépcsőa zonosítást mivel nincs bekapcsolva '{_userManager.GetUserId(User)}'");
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                logger.Warning("{userId} felhasználót nem lehetett betöltenim Disable2Fa--OnPost", _userManager.GetUserId(User));
                return NotFound($"Felhasználói adatokat nem tudtuk betölteni '{_userManager.GetUserId(User)}'.");
            }

            var disable2faResult = await _userManager.SetTwoFactorEnabledAsync(user, false);
            if (!disable2faResult.Succeeded)
            {
                logger.Error("{userId} nem tudta kikapcsolni, ismeretlen hiba Disable2Fa--OnPost", _userManager.GetUserId(User));
                throw new InvalidOperationException($"Egy előre nem láthatő hiba történt a 2FA kikapcsolásánál '{_userManager.GetUserId(User)}'.");
            }

            logger.Information("2Fa sikeresen kikapcsolva", _userManager.GetUserId(User));
            StatusMessage = "2FA sikeresen kikapcsolva, ahhoz";
            return RedirectToPage("./TwoFactorAuthentication");
        }
    }
}