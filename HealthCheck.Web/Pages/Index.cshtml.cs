using HealthCheck.API.Controllers;
using HealthCheck.Model;
using HealthCheck.Web.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Services = HealthCheck.API.Services;

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
            User activeDirectoryUser = null;
            try
            {
                activeDirectoryUser = authenticationService.GetADUser(LoginUserViewModel.Username, LoginUserViewModel.Password);
            }
            catch (Exception e)
            {
                return RedirectToPage("/Error", new { ReturnUrl = "/Index", ErrorMessage = "Incorrect Email or Password." });
            }
            if (activeDirectoryUser != null)
            {
                User dbUser = await GetDatabaseUser(activeDirectoryUser);
                await SignInClaimsUser(dbUser);
                return RedirectToPage("/Sessions/Index");
            }
            return RedirectToPage("/Error", new { ReturnUrl = "/Index", ErrorMessage = "Something went wrong." });
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
                    new Claim(ClaimTypes.Sid, dbUser.UserId.ToString()),
                    new Claim(ClaimTypes.NameIdentifier, dbUser.UserId.ToString()),
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
