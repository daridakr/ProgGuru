using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Identity;

namespace Daridakr.ProgGuru.Web.Pages.Account
{
    [AllowAnonymous]
    public class ConfirmEmailModel : PageModel
    {
        private readonly IdentityUserManager _userManager;
        public ConfirmEmailModel(IdentityUserManager userManager) { _userManager = userManager; }

        public async Task<IActionResult> OnGetAsync(string userId, string code)
        {
            if (userId == null || code == null) { return RedirectToPage("/Index"); }
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) { return NotFound($"Unable to load user with ID '{userId}'."); }
            code = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(code));
            var result = await _userManager.ConfirmEmailAsync(user, code); return Page(); }
    }
}
