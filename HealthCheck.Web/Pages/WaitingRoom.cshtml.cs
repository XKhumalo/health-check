using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using HealthCheck.API.Controllers;
using HealthCheck.API.Services;
using HealthCheck.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HealthCheck.Web.Pages
{
    public class WaitingRoomModel : PageModel
    {
        private readonly SessionController sessionController;
        private readonly CategoryController categoryController;
        private readonly AnswerController answerController;
        
        public Session SessionViewModel;
        public IEnumerable<Category> CategoriesViewModel;
        public IEnumerable<Answer> AnswersViewModel;
        public IEnumerable<GuestUserAnswer> GuestUserAnswersViewModel;
        public IEnumerable<SessionCategory> SessionCategoriesViewModel { get; set; }

        public WaitingRoomModel(SessionController sessionController, CategoryController categoryController, AnswerController answerController)
        {
            this.sessionController = sessionController;
            this.categoryController = categoryController;
            this.answerController = answerController;
        }
        
        public async Task OnGet(string sessionKey)
        {
            var isGuest = User.Claims.FirstOrDefault(c => c.Type.Equals(ClaimTypes.AuthorizationDecision))?.Value ==  "guestUser";
            if (isGuest)
            {
                var guestId = Convert.ToInt32(User.Claims.FirstOrDefault(c => c.Type.Equals(ClaimTypes.Sid)).Value);
                GuestUserAnswersViewModel = answerController.GetGuestAnswers(a => a.SessionId == SessionViewModel.SessionId && a.SessionOnlyUserId == guestId);
                AnswersViewModel = new List<Answer>();
            }
            else
            {
                var userId = Convert.ToInt32(User.Claims.FirstOrDefault(c => c.Type.Equals(ClaimTypes.Sid)).Value);
                AnswersViewModel = answerController.Get(a => a.SessionId == SessionViewModel.SessionId && a.UserId == userId);
                GuestUserAnswersViewModel = new List<GuestUserAnswer>();
            }
            SessionViewModel = await sessionController.GetBySessionKey(sessionKey);
            SessionCategoriesViewModel = sessionController.GetSessionCategories(SessionViewModel.SessionId);
            var categoryIds = SessionCategoriesViewModel.Select(sc => sc.CategoryId);
            CategoriesViewModel = categoryController.GetByIds(categoryIds);

        }
    }
}