using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using HealthCheck.API.Controllers;
using HealthCheck.Common;
using HealthCheck.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HealthCheck.Web.Pages.Sessions
{
    public class ViewSessionsModel : PageModel
    {
        private readonly SessionController sessionController;
        private readonly UserController userController;
        private readonly CategoryController categoryController;

        [BindProperty]
        public IEnumerable<Session> SessionsViewModel { get; set; }
        [BindProperty]
        public User UserViewModel { get; set; }

        public ViewSessionsModel(SessionController sessionController, 
            UserController userController,
            CategoryController categoryController)
        {
            this.sessionController = sessionController;
            this.userController = userController;
            this.categoryController = categoryController;
        }

        public async Task OnGet(string sessionId)
        {
            var userId = Convert.ToInt32(User.Claims.FirstOrDefault(c => c.Type.Equals(ClaimTypes.Sid)).Value);
            UserViewModel = await userController.GetByIdAsync(userId);
            SessionsViewModel = sessionController.GetByCreatedById(userId);
        }

        public async Task<IActionResult> OnPostCreate()
        {
            var userId = Convert.ToInt32(User.Claims.FirstOrDefault(c => c.Type.Equals(ClaimTypes.Sid)).Value);
            var newSession = new Session()
            {
                CreatedById = userId,
                DateCreated = DateTime.Now,
                IsComplete = false,
                IsOpen = false,
                SessionKey = Helpers.RandomString(6, false)
            };
            var createdSession = await sessionController.Create(newSession);
            return RedirectToPage("/Sessions/ViewSession", new { sessionId = createdSession.SessionId });
        }
    }
}