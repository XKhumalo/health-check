using HealthCheck.API.Controllers;
using HealthCheck.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Linq;
using System.Security.Claims;

namespace HealthCheck.Web.Pages
{
    public class SaveAnswerModel : PageModel
    {
        private readonly AnswerController answerController;
        private readonly SessionController sessionController;

        public SaveAnswerModel(AnswerController answerController, SessionController sessionController)
        {
            this.answerController = answerController;
            this.sessionController = sessionController;
        }

        public IActionResult OnGet(int categoryId, int sessionId, int answer)
        {
            var userId = Convert.ToInt32(User.Claims.FirstOrDefault(c => c.Type.Equals(ClaimTypes.Sid)).Value);
            var answerToSave = new Answer()
            {
                UserId = userId,
                SessionId = sessionId,
                CategoryId = categoryId,
                //AnswerOption = (Model.Enums.AnswerOptions)Enum.Parse(typeof(Model.Enums.AnswerOptions), answer)
                AnswerOptionId = answer
            };
            answerController.Create(answerToSave);
            var session = sessionController.GetById(sessionId);
            return RedirectToPage("/WaitingRoom", new { sessionKey = session.SessionKey });
        }
    }
}