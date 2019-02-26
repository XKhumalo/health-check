﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HealthCheck.API.Controllers;
using HealthCheck.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HealthCheck.Web.Pages.Sessions
{
    public class AddCategorySessionModel : PageModel
    {
        private readonly SessionController sessionController;
        private readonly CategoryController categoryController;

        public IEnumerable<Category> AvailableCategories { get; set; }
        public IEnumerable<Category> ChosenCategories { get; set; }
        public Session Session { get; set; }

        public AddCategorySessionModel(SessionController sessionController, CategoryController categoryController)
        {
            this.sessionController = sessionController;
            this.categoryController = categoryController;
        }

        public async Task OnGet(string sessionId)
        {
            var allCategories = await categoryController.Get().ContinueWith(r => r.Result.Value);
            Session = await sessionController.GetById(sessionId).ContinueWith(r => r.Result.Value);
            ChosenCategories = allCategories.Where(c => Session.Categories.Contains(c._id.ToString()));
            AvailableCategories = allCategories.Where(c => !Session.Categories.Contains(c._id.ToString()));
        }
    }
}