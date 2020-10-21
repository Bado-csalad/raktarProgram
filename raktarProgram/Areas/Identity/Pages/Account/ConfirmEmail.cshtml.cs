using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using raktarProgram.Data.Structs;
using raktarProgram.Interfaces;

namespace raktarProgram.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class ConfirmEmailModel : PageModel
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IUserRoleAdministrationRepository _repo;

        public ConfirmEmailModel(UserManager<IdentityUser> userManager, IUserRoleAdministrationRepository repo)
        {
            _userManager = userManager;
            _repo = repo;
        }

        [TempData]
        public string StatusMessage { get; set; }

        public async Task<IActionResult> OnGetAsync(string userId, string code)
        {
        
            if (userId == null || code == null)
            {
                return RedirectToPage("/Areas/Identity/Pages/Account/Manage");
            }

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return NotFound($"Nincs találat erre a felhasználóra: '{userId}'.");
            }

            code = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(code));
            var result = await _userManager.ConfirmEmailAsync(user, code);
            StatusMessage = result.Succeeded ? "E-mail sikeresen megerősítve" : "Az E-mail cím megerősítése során hiba történet.";

            if(result.Succeeded)
            {

                RoleAndUserStruct newUser = new RoleAndUserStruct(null, user)
                {
                    RoleId = "visitor"
                };
                var p = await _repo.RoleModositas(newUser);

            }

            return Page();
        }
    }
}
