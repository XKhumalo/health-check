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
        private readonly CategoryController categoryController;
        public Session Session { get; set; }
        public IEnumerable<Category> Categories { get; set; }

        public bool IsAuthorized { get; set; }

        public ViewSessionModel(SessionController sessionController, CategoryController categoryController)
        {
            this.sessionController = sessionController;
            this.categoryController = categoryController;
        }

        public async Task OnGet(string sessionId, string userId)
        {
            //Session = await sessionController.GetById(sessionId);
            //if (Session.CreatedBy.Equals(userId))
            //{
            //    IsAuthorized = true;
            //    Categories = await categoryController.GetByIds(Session.Categories);
            //}
            //else
            //{
            //    RedirectToPage("/Error");
            //}
        }
    }
}