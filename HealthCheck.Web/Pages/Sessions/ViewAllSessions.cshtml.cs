using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using HealthCheck.API.Controllers;
using HealthCheck.Model;

namespace HealthCheck.Web.Pages.Sessions
{
    public class ViewAllSessionsModel : PageModel

    {
        private readonly SessionController sessionController;

        [BindProperty]
        public List<Session> ViewAllSessionModel { get; set; }

        [BindProperty]
        public Session SessionViewModel { get; set; }
              
        public ViewAllSessionsModel(SessionController sessionController)
        {
           this.sessionController = sessionController;
             
        }

        public async Task<IActionResult> OnGetAsync()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var sessions = await sessionController.Get();
            if (sessions != null)
            {
                ViewAllSessionModel = sessions;
                return Page();
            }
            else
            {
                return RedirectToPage("/Error");
            }
        }
    }
}