using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using HealthCheck.Model;
using HealthCheck.API.Controllers;

namespace HealthCheck.Web.Pages
{
    public class IndexModel : PageModel
    {
        private readonly UserController userController;

        [BindProperty]
        public User UserViewModel { get; set; }
        
        public IndexModel(UserController userController)
        {
            this.userController = userController;
        }

        public void OnGet()
        {
            
        }

        public async Task<IActionResult> OnPostLogin()
        {
            var existingUser = await userController.GetByEmail(UserViewModel.Email);
            if (existingUser == null)
            {
                await userController.Create(UserViewModel);
            }
            return RedirectToPage("/Sessions/Index", new { userId = existingUser._id });
        }
    }
}
