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
    public class SaveAnswerModel : PageModel
    {
        private readonly AnswerController answerController;
        private readonly SessionController sessionController;

        public SaveAnswerModel(AnswerController answerController, SessionController sessionController)
        {
            this.answerController = answerController;
            this.sessionController = sessionController;
        }
        public async Task<IActionResult> OnGet(string categoryId, string sessionId, string answer)
        {
            var userId = User.Claims.FirstOrDefault(c => c.Type.Equals(ClaimTypes.Sid)).Value;
            var answerToSave = new Answer()
            {
                UserId = userId,
                SessionId = sessionId,
                CategoryId = categoryId,
                CategoryChosen = (Model.Enums.AnswerOption)Enum.Parse(typeof(Model.Enums.AnswerOption), answer)
            };
            await answerController.Create(answerToSave);
            var session = await sessionController.GetById(sessionId);
            return RedirectToPage("/WaitingRoom", new { sessionKey = session.SessionKey });
        }
    }
}