using HealthCheck.API.Controllers;
using HealthCheck.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace HealthCheck.Web.Pages.Categories
{
    public class ViewSessionCategoryModel : PageModel
    {
        private readonly SessionController sessionController;
        private readonly CategoryController categoryController;
        private readonly AnswerController answerController;
        private readonly UserController userController;

        public Session Session { get; set; }
        public Category Category { get; set; }
        public IEnumerable<Answer> Answers { get; set; }
        public IEnumerable<User> Users { get; set; }
        public bool IsAuthorized { get; set; }

        public ViewSessionCategoryModel(SessionController sessionController, CategoryController categoryController, AnswerController answerController, UserController userController)
        {
            this.sessionController = sessionController;
            this.categoryController = categoryController;
            this.answerController = answerController;
            this.userController = userController;
        }

        public void OnGet(int sessionId, int categoryId)
        {
            var userId = Convert.ToInt32(User.Claims.FirstOrDefault(c => c.Type.Equals(ClaimTypes.Sid)).Value);
            Session = sessionController.GetById(sessionId);
            if (Session.CreatedById == userId)
            {
                IsAuthorized = true;
            }
            Category = categoryController.GetById(categoryId);
            Answers = answerController.Get(a => a.CategoryId == categoryId && a.SessionId == sessionId);
            Users = userController.Get();
        }

        public IActionResult OnPostClose([FromBody] List<Answer> answers)
        {
            var a = answerController.CreateList(answers);

            return RedirectToPage("/ViewSession");
        }
    }
}