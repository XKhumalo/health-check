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
    public class ViewSessionModel : PageModel
    {
        private readonly SessionController sessionController;
        private readonly UserController userController;
        private readonly CategoryController categoryController;

        [BindProperty]
        public Session SessionViewModel { get; set; }
        [BindProperty]
        public User UserViewModel { get; set; }
        [BindProperty]
        public IEnumerable<Category> CategoriesViewModel { get; set; }

        public bool IsAuthorized { get; set; }

        public ViewSessionModel(SessionController sessionController, CategoryController categoryController, UserController userController)
        {
            this.sessionController = sessionController;
            this.categoryController = categoryController;
            this.userController = userController;
        }

        public async Task OnGet(string userId, string sessionId)
        {
            UserViewModel = await userController.GetById(userId);
            SessionViewModel = await sessionController.GetById(sessionId);
            if (SessionViewModel.CreatedBy.Equals(userId))
            {
                IsAuthorized = true;
                CategoriesViewModel = await categoryController.GetByIds(SessionViewModel.Categories);
            }
            else
            {
                RedirectToPage("/Error");
            }
        }
    }
}