using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace My_Blog_Website.Areas.Identity.Pages.Account
{
    public class LogoutModel : PageModel
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly ILogger<LogoutModel> _logger;

        public LogoutModel(SignInManager<IdentityUser> signInManager, ILogger<LogoutModel> logger)
        {
            _signInManager = signInManager;
            _logger = logger;
        }

        public async Task<IActionResult> OnGet()
        {
            if (User.Identity.IsAuthenticated)
            {
                await _signInManager.SignOutAsync();
                _logger.LogInformation("User logged out.");
            }
            return RedirectToPage("/Login");
        }

        public async Task<IActionResult> OnPost(string returnUrl = null)
        {
            // Sign out
            await _signInManager.SignOutAsync();

            // Delete all cookies (nuclear option)
            foreach (var cookie in Request.Cookies.Keys)
            {
                Response.Cookies.Delete(cookie, new CookieOptions
                {
                    Secure = true,
                    SameSite = SameSiteMode.Lax
                });
            }

            _logger.LogInformation("User logged out.");
            return LocalRedirect(returnUrl ?? "/Login");
        }
    }
}
