using HealthCheck.API.Controllers;
using HealthCheck.Common;
using HealthCheck.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace HealthCheck.Web.Pages.Sessions
{
    public class CreateSessionModel : PageModel
    {
        private readonly UserController userController;
        private readonly SessionController sessionController;
        private readonly CategoryController categoryController;

        [BindProperty]
        public Session SessionViewModel { get; set; }

        [BindProperty]
        public IEnumerable<Category> CategoriesViewModel { get; set; }

        public bool IsAuthorized { get; set; }

        public CreateSessionModel(UserController userController,
            SessionController sessionController,
            CategoryController categoryController)
        {
            this.userController = userController;
            this.sessionController = sessionController;
            this.categoryController = categoryController;
        }

        public IActionResult OnGet()
        {
            var isGuest = User.Claims.FirstOrDefault(c => c.Type.Equals(ClaimTypes.AuthorizationDecision))?.Value == "guestUser";
            if (isGuest)
            {
                var sessionKey = User.Claims.FirstOrDefault(c => c.Type.Equals("SessionKey"))?.Value;
                return RedirectToPage("/WaitingRoom", new { sessionKey = sessionKey });
            }
            CategoriesViewModel =  categoryController.Get().Result;
            return Page();
        }

        public IActionResult OnPostCreate()
        {
            var isGuest = User.Claims.FirstOrDefault(c => c.Type.Equals(ClaimTypes.AuthorizationDecision))?.Value == "guestUser";
            if (isGuest)
            {
                var sessionKey = User.Claims.FirstOrDefault(c => c.Type.Equals("SessionKey"))?.Value;
                return RedirectToPage("/WaitingRoom", new { sessionKey = sessionKey });
            }
            var userId = Convert.ToInt32(User.Claims.FirstOrDefault(c => c.Type.Equals(ClaimTypes.Sid)).Value);
            SessionViewModel = new Session()
            {
                CreatedById = userId,
                DateCreated = DateTime.Now,
                IsComplete = false,
                SessionKey = Helpers.RandomString(6, false)
            };
            var createdSession = sessionController.CreateSessionCategories(SessionViewModel);
            return RedirectToPage("/Sessions/ViewSession", new { sessionId = SessionViewModel.SessionId });
        }
        
    }
}