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


        public void OnGet(string userId)
        {
            UserId = userId;
        }
    }
}