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

        [BindProperty]
        public string SessionCode { get; set; }

        public IndexModel(UserController _userController)
        {
            userController = _userController;
        }

        public void OnGet()
        {

        }

        public async Task<IActionResult> OnPostJoin()
        {
            var existingUser = await userController.GetByEmail(UserViewModel.Email);
            if (existingUser.Value == null)
            {
                await userController.Create(UserViewModel);
            }
            return RedirectToPage("/Answer");
        }
    }
}
