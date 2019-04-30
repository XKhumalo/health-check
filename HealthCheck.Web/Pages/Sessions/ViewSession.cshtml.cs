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
        public IEnumerable<Answer> Answers { get; set; }

        public bool IsAuthorized { get; set; }

        public ViewSessionModel(SessionController sessionController, CategoryController categoryController, UserController userController, AnswerController answerController)
        {
            this.sessionController = sessionController;
            this.categoryController = categoryController;
            this.userController = userController;
            this.answerController = answerController;
        }

        public async Task OnGet(string sessionId)
        {
            var userId = User.Claims.FirstOrDefault(c => c.Type.Equals(ClaimTypes.Sid)).Value;
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
            Answers = await answerController.Get(a => a.SessionId == sessionId);
        }

        public async Task<IActionResult> OnPostStart(string sessionId)
        {
            var session = await sessionController.GetById(sessionId);
            session.IsOpen = true;
            session.IsComplete = false;
            session.StartTime = DateTime.Now;
            await sessionController.Update(sessionId, session);
            return RedirectToPagePermanent("/Sessions/ViewSession", new { sessionId });
        }

        public async Task<IActionResult> OnPostClose(string sessionId)
        {
            var session = await sessionController.GetById(sessionId);
            session.IsOpen = false;
            session.IsComplete = true;
            session.EndTime = DateTime.Now;
            await sessionController.Update(sessionId, session);
            return RedirectToPagePermanent("/Sessions/ViewSession", new { sessionId });
        }
    }
}