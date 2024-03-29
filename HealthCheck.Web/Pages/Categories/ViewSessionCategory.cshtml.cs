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
        private readonly UserController userController;

        public Session Session { get; set; }
        public Category Category { get; set; }
        public IEnumerable<Answer> Answers { get; set; }
        public IEnumerable<GuestUserAnswer> GuestAnswers { get; set; }
        public IEnumerable<User> Users { get; set; }
        public bool IsAuthorized { get; set; }
        public int numberOfAnswersCaptured { get; set; }

        public ViewSessionCategoryModel(SessionController sessionController, CategoryController categoryController, AnswerController answerController, UserController userController)
        {
            this.sessionController = sessionController;
            this.categoryController = categoryController;
            this.answerController = answerController;
            this.userController = userController;
        }

        public async Task OnGet(int sessionId, int categoryId)
        {
            numberOfAnswersCaptured = 0;
            var userId = Convert.ToInt32(User.Claims.FirstOrDefault(c => c.Type.Equals(ClaimTypes.Sid)).Value);
            Session = await sessionController.GetByIdAsync(sessionId);
            var isGuest = User.Claims.FirstOrDefault(c => c.Type.Equals(ClaimTypes.AuthorizationDecision))?.Value == "guestUser";
            if (Session.CreatedById == userId)
            {
                IsAuthorized = true;
            }
            Category = await categoryController.GetById(categoryId);
            Answers = answerController.Get(a => a.CategoryId == categoryId && a.SessionId == sessionId);
            GuestAnswers =
                answerController.GetGuestAnswers(a => a.CategoryId == categoryId && a.SessionId == sessionId);
            numberOfAnswersCaptured = Answers.Count() + GuestAnswers.Count();
            Users = userController.Get();
        }

        public async Task<IActionResult> OnPostClose([FromBody] List<Answer> answers)
        {
            var answerList = await answerController.CreateList(answers);

            return RedirectToPage("/ViewSession");
        }
    }
}