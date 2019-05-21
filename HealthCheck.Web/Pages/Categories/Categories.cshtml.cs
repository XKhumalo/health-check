using HealthCheck.API.Controllers;
using HealthCheck.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Linq;
using HealthCheck.Web.Extensions;
using Microsoft.AspNetCore.Antiforgery.Internal;

namespace HealthCheck.Web.Pages.Categories
{
    public class CategoriesModel : PageModel
    {
        private readonly CategoryController categoryController;

        [BindProperty]
        public IEnumerable<Category> CategoriesViewModel { get; set; }

        public CategoriesModel(CategoryController categoryController)
        {
            this.categoryController = categoryController;
        }

        public void OnGet()
        {
            CategoriesViewModel = categoryController.Get();
        }

        public IActionResult OnPostCreate()
        {
            return RedirectToPage("/Categories/AddCategory");
        }
    }
}