using Identity.API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace Identity.API.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class LogoutModel : PageModel
    {
        private readonly SignInManager<User> signInManager;
        private readonly ILogger<LogoutModel> logger;

        public LogoutModel(SignInManager<User> signInManager, ILogger<LogoutModel> logger)
        {
            this.signInManager = signInManager ?? throw new ArgumentNullException(nameof(signInManager));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPost(string returnUrl = "~/Identity/Account/Login")
        {
            // Ask ASP.NET Core Identity to delete the local and external cookies created
            // when the user agent is redirected from the external identity provider
            // after a successful authentication flow (e.g Google or Facebook).
            await signInManager.SignOutAsync();

            logger.LogInformation("User logged out.");

            return Redirect(returnUrl);
        }
    }
}