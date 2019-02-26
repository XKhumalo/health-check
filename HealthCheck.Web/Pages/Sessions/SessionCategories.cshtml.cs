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

        public SessionCategoriesModel(SessionController sessionController, CategoryController categoryController)
        {
            this.sessionController = sessionController;
            this.categoryController = categoryController;
        }

        public async Task OnGet(string sessionId)
        {
            Session = await sessionController.GetById(sessionId).ContinueWith(r => r.Result.Value);
            var categoryIds = Session.Categories;
            //Categories = await categoryController.GetByIds(categoryIds).ContinueWith(r => r.Result.Value);
        }
    }
}