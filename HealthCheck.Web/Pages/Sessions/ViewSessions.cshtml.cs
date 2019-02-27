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
    public class ViewSessionsModel : PageModel
    {
        private readonly SessionController sessionController;
        private readonly UserController userController;

        [BindProperty]
        public IEnumerable<Session> SessionsViewModel { get; set; }
        [BindProperty]
        public User UserViewModel { get; set; }

        public ViewSessionsModel(SessionController sessionController, UserController userController)
        {
            this.sessionController = sessionController;
            this.userController = userController;
        }

        public async Task OnGet(string userId, string sessionId)
        {
            UserViewModel = await userController.GetById(userId);
            SessionsViewModel = await sessionController.GetByCreatedById(userId);
        }
    }
}