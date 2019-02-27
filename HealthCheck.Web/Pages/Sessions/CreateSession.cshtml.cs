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
        private readonly CategoryController categoryController;


        [BindProperty]
        public User UserViewModel { get; set; }

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

        public async Task OnGet(string userId)
        {
            UserViewModel = await userController.GetById(userId);
            CategoriesViewModel = await categoryController.Get();
        }

        public async Task<IActionResult> OnPostCreate()
        {
            var user = await userController.GetByEmail(UserViewModel.Email);
            if (user == null)
            {
                return RedirectToPage("/Error");
            }
            return RedirectToPage("/Error");
            //SessionViewModel.CreatedBy = newUser == null ? UserViewModel._id.ToString() : newUser._id.ToString();
            //var createdSession = await sessionController.Create(SessionViewModel);
            //if (createdSession != null)
            //{
            //    return RedirectToPage("../Sessions/ViewSession", new { sessionId = createdSession.Value._id, userId = createdSession.Value.CreatedBy });
            //}
            //else
            //{
            //    return RedirectToPage("/Error");
            //}
        }
        
    }
}