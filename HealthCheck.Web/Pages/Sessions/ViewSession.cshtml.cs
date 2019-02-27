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
    public class ViewSessionModel : PageModel
    {
        private readonly SessionController sessionController;

        [BindProperty]
        public Session ViewAllSessionModel { get; set; }

        [BindProperty]
        public Session SessionViewModel { get; set; }

        public ViewSessionModel(SessionController sessionController)
        {
            this.sessionController = sessionController;
        }

        public void OnGet()
        {

        }
    }
}








