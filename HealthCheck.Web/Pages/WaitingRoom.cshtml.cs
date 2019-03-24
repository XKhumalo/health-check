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

        public WaitingRoomModel(SessionController sessionController, CategoryController categoryController, AnswerController answerController)
        {
            this.sessionController = sessionController;
            this.categoryController = categoryController;
            this.answerController = answerController;
        }
        
        public async Task OnGet(string sessionKey)
        {
            var userId = User.Claims.FirstOrDefault(c => c.Type.Equals(ClaimTypes.Sid)).Value;
            SessionViewModel = await sessionController.GetBySessionKey(sessionKey);
            CategoriesViewModel = await categoryController.GetByIds(SessionViewModel.Categories);
            AnswersViewModel = await answerController.Get(a => a.SessionId.Equals(SessionViewModel._id.ToString()) && a.UserId.Equals(userId));
        }
    }
}