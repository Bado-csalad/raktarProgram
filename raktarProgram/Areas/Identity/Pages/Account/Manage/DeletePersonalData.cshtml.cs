using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Serilog;

namespace raktarProgram.Areas.Identity.Pages.Account.Manage
{
    public class DeletePersonalDataModel : PageModel
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly ILogger logger;

        public DeletePersonalDataModel(
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            ILogger logger)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            this.logger = logger;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Required]
            [DataType(DataType.Password)]
            public string Password { get; set; }
        }

        public bool RequirePassword { get; set; }

        public async Task<IActionResult> OnGet()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                logger.Warning("{user} - adatait nem lehetett betölteni, DeletePersonalData --OnGet", _userManager.GetUserId(User));

                return NotFound($"Felhasználói adatokat nem tudtuk betölteni '{_userManager.GetUserId(User)}'.");
            }

            RequirePassword = await _userManager.HasPasswordAsync(user);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                logger.Warning("{user} - adatait nem lehetett betölteni, DeletePersonalData --OnPost", _userManager.GetUserId(User));
                return NotFound($"Felhasználói adatokat nem tudtuk betölteni D '{_userManager.GetUserId(User)}'.");
            }

            RequirePassword = await _userManager.HasPasswordAsync(user);
            if (RequirePassword)
            {
                if (!await _userManager.CheckPasswordAsync(user, Input.Password))
                {
                    ModelState.AddModelError(string.Empty, "Hibás jelszó");
                    return Page();
                }
            }

            var result = await _userManager.DeleteAsync(user);
            var userId = await _userManager.GetUserIdAsync(user);
            if (!result.Succeeded)
            {
                logger.Warning("{user} - adatait nem lehetett törölni, DeletePersonalData --OnGet", _userManager.GetUserId(User));
                throw new InvalidOperationException($"Felhasználói adatokat nem tudtuk betölteni '{userId}'.");
            }

            await _signInManager.SignOutAsync();

            logger.Information("{user} törölve lett", _userManager.GetUserId(User));

            return Redirect("~/");
        }
    }
}
