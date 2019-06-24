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
    public class SessionCategoriesModel : PageModel
    {
        private readonly SessionController sessionController;
        private readonly CategoryController categoryController;

        public IEnumerable<Category> Categories { get; set; }
        public Session Session { get; set; }
        public IEnumerable<SessionCategory> SessionCategoriesViewModel { get; set; }

        public SessionCategoriesModel(SessionController sessionController, CategoryController categoryController)
        {
            this.sessionController = sessionController;
            this.categoryController = categoryController;
        }

        public async Task OnGet(int sessionId)
        {
            Session = await sessionController.GetByIdAsync(sessionId);
            var categoryIds = SessionCategoriesViewModel.Select(sc => sc.CategoryId);
            Categories = categoryController.GetByIds(categoryIds);
        }
    }
}