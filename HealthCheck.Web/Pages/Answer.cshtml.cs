using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HealthCheck.API.Controllers;
using HealthCheck.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HealthCheck.Web.Pages
{
    public class AnswerModel : PageModel
    {
        private readonly SessionController sessionController;
        private readonly CategoryController categoryController;

        public Session SessionViewModel { get; set; }
        public Category CategoryViewModel { get; set; }
        public string AdminId { get; set; }

        public async Task OnGet(string adminId, string categoryId)
        {
            AdminId = adminId;
            CategoryViewModel = await categoryController.GetById(adminId);
        }
    }
}