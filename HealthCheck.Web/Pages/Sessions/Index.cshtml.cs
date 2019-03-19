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
    public class IndexModel : PageModel
    {
        private readonly SessionController sessionController;
        private readonly UserController userController;

        [BindProperty]
        public string SessionKey { get; set; }
        [BindProperty]
        public string UserId { get; set; }

        public IndexModel(SessionController sessionController)
        {
            this.sessionController = sessionController;
        }

        public async Task<IActionResult> OnPostLogin()
        {
            var session = await sessionController.GetBySessionKey(SessionKey);
            if (!session.IsOpen && !session.IsComplete) return RedirectToPage("/Error");
            else
            {
                return RedirectToPage("/WaitingRoom", new { sessionId = session._id });
            }
        }
    }
}