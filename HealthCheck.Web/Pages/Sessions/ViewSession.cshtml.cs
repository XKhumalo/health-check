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
        public IEnumerable<GuestUserAnswer> GuestAnswers { get; set; }

        public bool IsAuthorized { get; set; }

        public ViewSessionModel(SessionController sessionController, CategoryController categoryController, UserController userController, AnswerController answerController)
        {
            this.sessionController = sessionController;
            this.categoryController = categoryController;
            this.userController = userController;
            this.answerController = answerController;
        }

        public IActionResult OnGet(int sessionId)
        {
            var isGuest = User.Claims.FirstOrDefault(c => c.Type.Equals(ClaimTypes.AuthorizationDecision))?.Value == "guestUser";
            if (isGuest)
            {
                var sessionKey = User.Claims.FirstOrDefault(c => c.Type.Equals("SessionKey"))?.Value;
                return RedirectToPage("/WaitingRoom", new { sessionKey = sessionKey });
            }
            //wait for async answer to complete db persistence on other threads (when closing a category) //TODO optimise this
            System.Threading.Thread.Sleep(1000);
            
            var userId = Convert.ToInt32(User.Claims.FirstOrDefault(c => c.Type.Equals(ClaimTypes.Sid)).Value);
            UserViewModel =  userController.GetByIdAsync(userId).Result;
            SessionViewModel = sessionController.GetByIdAsync(sessionId).Result;
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
            Answers = answerController.Get(a => a.SessionId == sessionId).ToList();
            GuestAnswers = answerController.GetGuestAnswers(x => x.SessionId == sessionId).ToList();
            return Page();
        }

        public async Task<IActionResult> OnPostStartAsync(int sessionId)
        {
            var session = await sessionController.GetByIdAsync(sessionId);
            session.IsOpen = true;
            session.IsComplete = false;
            session.StartTime = DateTime.Now;
            sessionController.Update(session);
            return RedirectToPagePermanent("/Sessions/ViewSession", new { sessionId });
        }

        public IActionResult OnPostExportToExcel(int sessionId)
        {
            return answerController.ExportSessionsAnswersToExcelAsync(sessionId);
        }

        public async Task<IActionResult> OnPostClose(int sessionId)
        {
            var session = await sessionController.GetByIdAsync(sessionId);
            session.IsOpen = false;
            session.IsComplete = true;
            session.EndTime = DateTime.Now;
            sessionController.Update(session);
            return RedirectToPagePermanent("/Sessions/ViewSession", new { sessionId });
        }
    }
}