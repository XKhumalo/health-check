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
            LoginUserViewModel = new LoginUserViewModel();

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
            catch (Exception)
            {
                LoginUserViewModel.IsCredentialsIncorrect = true;
                return Page();
            }
            if (activeDirectoryUser != null)
            {
                LoginUserViewModel.IsCredentialsIncorrect = false;

                User dbUser = await GetDatabaseUser(activeDirectoryUser);
                await SignInClaimsUser(dbUser);
                return RedirectToPage("/Sessions/Index");
            }
            return RedirectToPage("/Error", new { ReturnUrl = "/Index", ErrorMessage = "Something went wrong." });
        }

        public async Task<IActionResult> OnPostGuestLogin()
        {
            var guestName = LoginUserViewModel.GuestName;
            var guestSessionKey = LoginUserViewModel.SessionKey;
            var guestUser = new SessionOnlyUser(guestName, guestSessionKey);
            User activeDirectoryFakeUser = null;
            try
            {
                activeDirectoryFakeUser = new User()
                {
                    Email = guestName.Replace(" ","") + "@guestUser.com",
                    Name = guestName
                };
            }
            catch (Exception)
            {
                LoginUserViewModel.IsCredentialsIncorrect = true;
                return Page();
            }
            if (activeDirectoryFakeUser != null)
            {
                LoginUserViewModel.IsCredentialsIncorrect = false;

                User dbUser = activeDirectoryFakeUser;
                await SignInGuestUser(guestUser);
                return RedirectToPage("/Sessions/Index");
            }
            return RedirectToPage("/Error", new { ReturnUrl = "/Index", ErrorMessage = "Something went wrong." });
        }

        public async Task<IActionResult> OnPostLoginAsTemp()
        {
            User activeDirectoryUser = null;
            try
            {
                activeDirectoryUser = authenticationService.GetADUser(LoginUserViewModel.Username, LoginUserViewModel.Password);
            }
            catch (Exception)
            {
                LoginUserViewModel.IsCredentialsIncorrect = true;
                return Page();
            }
            if (activeDirectoryUser != null)
            {
                LoginUserViewModel.IsCredentialsIncorrect = false;

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

        private async Task SignInGuestUser(SessionOnlyUser sessionUser)
        {
            var random = new Random();
            var fakeUserId = random.Next(1, 999);
            var fakeEmail = sessionUser.UserName.Replace(" ","") + fakeUserId.ToString() + "@" + sessionUser.SessionKey + ".com";
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Sid, sessionUser.UserName.Replace(" ","") +"_" + fakeUserId.ToString() + "_guest" + sessionUser.SessionKey ),
                new Claim(ClaimTypes.NameIdentifier, fakeUserId.ToString()),
                new Claim(ClaimTypes.Name, sessionUser.UserName),
                new Claim(ClaimTypes.Email, fakeEmail),
                new Claim(ClaimTypes.AuthorizationDecision, "guestUser")
            };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var authProperties = new AuthenticationProperties
            {
                ExpiresUtc = DateTimeOffset.UtcNow.AddHours(3),
                IssuedUtc = DateTimeOffset.UtcNow,
            };

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProperties);
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
