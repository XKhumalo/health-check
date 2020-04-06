using System.Linq;
using System.Security.Claims;
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

        public IActionResult OnGet()
        {
            var isGuest = User.Claims.FirstOrDefault(c => c.Type.Equals(ClaimTypes.AuthorizationDecision))?.Value == "guestUser";
            if (isGuest)
            {
                var sessionKey = User.Claims.FirstOrDefault(c => c.Type.Equals("SessionKey"))?.Value;
                return RedirectToPage("/WaitingRoom", new { sessionKey = sessionKey });
            }

            return Page();
        }

        public IActionResult OnPostCreate()
        {
            var isGuest = User.Claims.FirstOrDefault(c => c.Type.Equals(ClaimTypes.AuthorizationDecision))?.Value == "guestUser";
            if (isGuest)
            {
                var sessionKey = User.Claims.FirstOrDefault(c => c.Type.Equals("SessionKey"))?.Value;
                return RedirectToPage("/WaitingRoom", new { sessionKey = sessionKey });
            }
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