﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using HealthCheck.API.Controllers;
using HealthCheck.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HealthCheck.Web.Pages.Categories
{
    public class ViewSessionCategoryModel : PageModel
    {
        private readonly SessionController sessionController;
        private readonly CategoryController categoryController;
        private readonly AnswerController answerController;

        public Session SessionViewModel { get; set; }
        public Category CategoryViewModel { get; set; }
        public bool IsAuthorized { get; set; }

        public ViewSessionCategoryModel(SessionController sessionController, CategoryController categoryController, AnswerController answerController)
        {
            this.sessionController = sessionController;
            this.categoryController = categoryController;
            this.answerController = answerController;
        }

        public async Task OnGet(string sessionId, string categoryId)
        {
            var userId = User.Claims.FirstOrDefault(c => c.Type.Equals(ClaimTypes.Sid)).Value;
            SessionViewModel = await sessionController.GetById(sessionId);
            if (SessionViewModel.CreatedBy.Equals(userId))
            {
                IsAuthorized = true;
            }
            CategoryViewModel = await categoryController.GetById(categoryId);
        }

        public async Task<IActionResult> OnPostClose([FromBody] List<Answer> answers)
        {
            var a = await answerController.CreateList(answers);


            return RedirectToPage("/ViewSession");
        }
    }
}