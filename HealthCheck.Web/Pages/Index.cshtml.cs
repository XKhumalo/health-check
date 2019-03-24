using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using HealthCheck.Model;
using HealthCheck.API.Controllers;
using Services = HealthCheck.API.Services;
using HealthCheck.Web.ViewModels;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;

namespace HealthCheck.Web.Pages
{
    public class IndexModel : PageModel
    {
        private readonly UserController userController;
        private readonly Services.AuthenticationService authenticationService;

        [BindProperty]
        public LoginUserViewModel LoginUserViewModel { get; set; }

        public IndexModel(UserController userController, Services.AuthenticationService authenticationService)
        {
            this.userController = userController;
            this.authenticationService = authenticationService;
        }

        public IActionResult OnGet()
        {
            if (User.Claims.Any())
            {
                return RedirectToPage("/Sessions/Index");
            }
            return Page();
        }

        public async Task<IActionResult> OnPostLogin()
        {
            var activeDirectoryUser = authenticationService.GetADUser(LoginUserViewModel.Username, LoginUserViewModel.Password);
            if (activeDirectoryUser != null)
            {
                User dbUser = await GetDatabaseUser(activeDirectoryUser);
                await SignInClaimsUser(dbUser);
                return RedirectToPage("/Sessions/Index");
            }
            return RedirectToPage("/Error");
        }
        
        private async Task<User> GetDatabaseUser(User activeDirectoryUser)
        {
            var dbUser = await userController.GetByEmail(activeDirectoryUser.Email);
            if (dbUser == null)
            {
                dbUser = await userController.Create(activeDirectoryUser);
            }

            return dbUser;
        }

        private async Task SignInClaimsUser(User dbUser)
        {
            var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Sid, dbUser._id.ToString()),
                    new Claim(ClaimTypes.NameIdentifier, dbUser._id.ToString()),
                    new Claim(ClaimTypes.Name, dbUser.Name),
                    new Claim(ClaimTypes.Email, dbUser.Email)
                };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var authProperties = new AuthenticationProperties
            {
                ExpiresUtc = DateTimeOffset.UtcNow.AddHours(3),
                IssuedUtc = DateTimeOffset.UtcNow,
            };

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProperties);
        }
    }
}
