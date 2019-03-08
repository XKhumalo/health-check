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
            var existingUser = await authenticationService.GetUserAsync(LoginUserViewModel.Username, LoginUserViewModel.Password);
            return RedirectToPage("/Sessions/Index", new { userId = existingUser._id }).AddCookie(HttpContext.Response, "user", existingUser._id.ToString());
        }

        //public async Task<IActionResult> OnPostLogin()
        //{
        //    var existingUser = await userController.GetByEmail(UserViewModel.Email);
        //    if (existingUser == null)
        //    {
        //        await userController.Create(UserViewModel);
        //    }
        //    return RedirectToPage("/Sessions/Index", new { userId = existingUser._id });
        //}
    }
}
