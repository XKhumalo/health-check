using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HealthCheck.API.Controllers;
using HealthCheck.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HealthCheck.Web.Pages.Sessions
{
    public class CreateSessionModel : PageModel
    {
        private readonly UserController userController;
        private readonly SessionController sessionController;

        [BindProperty]
        public User UserViewModel { get; set; }

        [BindProperty]
        public Session SessionViewModel { get; set; }

        public CreateSessionModel(UserController userController, SessionController sessionController)
        {
            this.userController = userController;
            this.sessionController = sessionController;
        }

        public void OnGet()
        {

        }

        public async Task<IActionResult> OnPostCreate()
        {
            User newUser = null;
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            if (!UserExists().Result)
            {
                newUser = await userController.Create(UserViewModel).ContinueWith(r => r.Result.Value);
            }
            SessionViewModel.CreatedBy = newUser == null ? UserViewModel._id.ToString() : newUser._id.ToString();
            var createdSession = await sessionController.Create(SessionViewModel);
            if (createdSession != null)
            {
                return RedirectToPage("../Sessions/ViewSession", new { sessionId = createdSession.Value._id, userId = createdSession.Value.CreatedBy });
            }
            else
            {
                return RedirectToPage("/Error");
            }
        }

        private async Task<bool> UserExists()
        {
            var existingUser = await userController.GetByEmail(UserViewModel.Email);
            return existingUser.Value != null;
        }
    }
}