using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using HealthCheck.Model;
using HealthCheck.API.Controllers;
using HealthCheck.API.Services;
using HealthCheck.Web.ViewModels;

namespace HealthCheck.Web.Pages
{
    public class IndexModel : PageModel
    {
        private readonly UserController userController;
        private readonly AuthenticationService authenticationService;

        [BindProperty]
        public LoginUserViewModel LoginUserViewModel { get; set; }
        
        public IndexModel(UserController userController, AuthenticationService authenticationService)
        {
            this.userController = userController;
            this.authenticationService = authenticationService;
        }

        public void OnGet()
        {
            
        }

        public async Task<IActionResult> OnPostLogin()
        {
            var activeDirectoryUser = authenticationService.GetADUser(LoginUserViewModel.Username, LoginUserViewModel.Password);
            if (activeDirectoryUser != null)
            {
                var dbUser = await userController.GetByEmail(activeDirectoryUser.Email);
                if (dbUser == null)
                {
                    dbUser = await userController.Create(activeDirectoryUser);
                }
                return RedirectToPage("/Sessions/Index").AddCookie(HttpContext.Response, "user", dbUser._id.ToString());
            }
            return RedirectToPage("/Error");
        }
    }
}
