using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
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
        private readonly AnswerController answerController;

        [BindProperty]
        public Session SessionViewModel { get; set; }
        [BindProperty]
        public User UserViewModel { get; set; }
        [BindProperty]
        public IEnumerable<Category> CategoriesViewModel { get; set; }
        [BindProperty]
        public IEnumerable<SessionCategory> SessionCategoriesViewModel { get; set; }
        [BindProperty]
        public IEnumerable<Answer> Answers { get; set; }

        public bool IsAuthorized { get; set; }

        public ViewSessionModel(SessionController sessionController, CategoryController categoryController, UserController userController, AnswerController answerController)
        {
            this.sessionController = sessionController;
            this.categoryController = categoryController;
            this.userController = userController;
            this.answerController = answerController;
        }

        public void OnGet(int sessionId)
        {
            var userId = Convert.ToInt32(User.Claims.FirstOrDefault(c => c.Type.Equals(ClaimTypes.Sid)).Value);
            UserViewModel = userController.GetById(userId);
            SessionViewModel = sessionController.GetById(sessionId);
            SessionCategoriesViewModel = sessionController.GetSessionCategories(sessionId);

            if (SessionViewModel.CreatedById == userId)
            {
                IsAuthorized = true;
                var categoryIds = SessionCategoriesViewModel.Select(sc => sc.CategoryId);
                CategoriesViewModel = categoryController.GetByIds(categoryIds);
            }
            else
            {
                RedirectToPage("/Error");
            }
            Answers = answerController.Get(a => a.SessionId == sessionId);
        }

        public IActionResult OnPostStart(int sessionId)
        {
            var session = sessionController.GetById(sessionId);
            session.IsOpen = true;
            session.IsComplete = false;
            session.StartTime = DateTime.Now;
            sessionController.Update(session);
            return RedirectToPagePermanent("/Sessions/ViewSession", new { sessionId });
        }

        public async Task<IActionResult> OnPostExportToExcel(int sessionId)
        {
            return answerController.ExportSessionsAnswersToExcel(sessionId);
        }

        public IActionResult OnPostClose(int sessionId)
        {
            var session = sessionController.GetById(sessionId);
            session.IsOpen = false;
            session.IsComplete = true;
            session.EndTime = DateTime.Now;
            sessionController.Update(session);
            return RedirectToPagePermanent("/Sessions/ViewSession", new { sessionId });
        }
    }
}