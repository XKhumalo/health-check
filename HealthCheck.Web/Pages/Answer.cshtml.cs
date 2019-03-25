using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using HealthCheck.API.Controllers;
using HealthCheck.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HealthCheck.Web.Pages
{
    public class AnswerModel : PageModel
    {
        private readonly SessionController sessionController;
        private readonly CategoryController categoryController;
        private readonly UserController userController;

        public AnswerModel(SessionController sessionController, CategoryController categoryController, UserController userController)
        {
            this.sessionController = sessionController;
            this.categoryController = categoryController;
            this.userController = userController;
        }

        public Session SessionViewModel { get; set; }
        public Category CategoryViewModel { get; set; }
        public User UserViewModel { get; set; }
        public string AdminId { get; set; }

        public async Task OnGet(string adminId, string sessionId, string categoryId)
        {
            var userId = User.Claims.FirstOrDefault(c => c.Type.Equals(ClaimTypes.Sid)).Value;
            AdminId = adminId;
            SessionViewModel = await sessionController.GetById(sessionId);
            CategoryViewModel = await categoryController.GetById(categoryId);
            UserViewModel = await userController.GetById(userId);
        }
    }
}