using HealthCheck.API.Controllers;
using HealthCheck.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HealthCheck.Web.Pages.Categories
{
    public class AddCategoryModel : PageModel
    {
        private readonly CategoryController categoryController;

        [BindProperty]
        public Category CategoryViewModel { get; set; }

        public AddCategoryModel(CategoryController categoryController)
        {
            this.categoryController = categoryController;
        }

        public void OnGet()
        {

        }

        public IActionResult OnPostCreate()
        {
            CategoryViewModel.IsDeleted = false;
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var category = categoryController.Create(CategoryViewModel);
            return RedirectToPage("/Categories/Categories");
        }
    }
}