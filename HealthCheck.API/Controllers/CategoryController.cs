using HealthCheck.API.Services;
using HealthCheck.Model;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace HealthCheck.API.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class CategoryController : Controller
    {
        private readonly CategoryRepository categoryService;

        public CategoryController(CategoryRepository categoryService)
        {
            this.categoryService = categoryService;
        }

        [HttpGet("{id:length(24)}")]
        [Route("[action]")]
        public Category GetById(int id)
        {
            return categoryService.GetById(id);
        }

        [HttpGet]
        [Route("[action]")]
        public IEnumerable<Category> GetByIds(IEnumerable<int> ids)
        {
            return ids == null ? null : categoryService.GetCategories(ids);
        }

        [HttpGet]
        public IEnumerable<Category> Get()
        {
            return categoryService.GetAll();
        }

        [HttpPut("{id}")]
        public void Update(int id, Category categoryIn)
        {
            var category = categoryService.GetById(id);
            categoryService.Update(categoryIn);
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var category = categoryService.GetById(id);
            categoryService.Delete(category);
        }

        [HttpPost]
        public Category Create([FromBody] Category category)
        {
            if (category == null)
            {
                return null;
            }
            return categoryService.Create(category);
        }
    }
}