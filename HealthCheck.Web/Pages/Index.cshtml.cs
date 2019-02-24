using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using HealthCheck.Model.Models;
using HealthCheck.API.Controllers;

namespace HealthCheck.Web.Pages
{
    public class IndexModel : PageModel
    {
        private readonly UserController _userController;

        [BindProperty]
        public User UserViewModel { get; set; }

        [BindProperty]
        public string SessionCode { get; set; }

        public IndexModel(UserController userController)
        {
            _userController = userController;
        }

        public void OnGet()
        {

        }

        public async Task<IActionResult> OnPostJoin()
        {
            var existingUser = await _userController.GetByEmail(UserViewModel.Email);
            if (existingUser.Value == null)
            {
                await _userController.Create(UserViewModel);
            }
            return RedirectToPage("/Answer");
        }
    }
}
