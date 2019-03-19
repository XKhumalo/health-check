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
    public class ViewSessionCategoryAnswersModel : PageModel
    {
        private readonly SessionController sessionController;
        private readonly CategoryController categoryController;
        private readonly UserController userController;

        public Session SessionViewModel { get; set; }
        public Category CategoryViewModel { get; set; }
        public bool IsAuthorized { get; set; }

        public ViewSessionCategoryAnswersModel(SessionController sessionController, CategoryController categoryController, UserController userController)
        {
            this.sessionController = sessionController;
            this.categoryController = categoryController;
            this.userController = userController;
        }

        public async Task OnGet(string sessionId, string categoryId)
        {
            var userId = Request.Cookies["user"];
            SessionViewModel = await sessionController.GetById(sessionId);
            if (SessionViewModel.CreatedBy.Equals(userId))
            {
                IsAuthorized = true;
            }
            CategoryViewModel = await categoryController.GetById(categoryId);
        }
    }
}