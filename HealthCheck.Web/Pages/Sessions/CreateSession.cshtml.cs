using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HealthCheck.API.Controllers;
using HealthCheck.Common;
using HealthCheck.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

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

        public async Task OnGet()
        {
            CategoriesViewModel = await categoryController.Get();
        }

        public async Task<IActionResult> OnPostCreate()
        {
            var categories = await categoryController.Get();
            string userId = Request.Cookies["user"];
            SessionViewModel = new Session()
            {
                Categories = categories.Select(c => c._id.ToString()),
                CreatedBy = userId,
                DateCreated = DateTime.Now,
                IsComplete = false,
                SessionKey = Helpers.RandomString(6, false)
            };
            var createdSession = await sessionController.Create(SessionViewModel);
            return RedirectToPage("/Sessions/ViewSession", new { sessionId = SessionViewModel._id });
        }
        
    }
}