using HealthCheck.API.Services;
using HealthCheck.Model;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HealthCheck.API.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class CategoryController : Controller
    {
        private readonly CategoryService categoryService;

        public CategoryController(CategoryService categoryService)
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
        public async Task<IEnumerable<Category>> Get()
        {
            return await categoryService.GetAll();
        }

        [HttpPut("{id}")]
        public async Task Update(int id, Category categoryIn)
        {
            var category = categoryService.GetById(id);

            await categoryService.Update(categoryIn);
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var category = categoryService.GetById(id);
            categoryService.Delete(category);
        }

        [HttpPost]
        public async Task<Category> Create([FromBody] Category category)
        {
            if (category == null)
            {
                return null;
            }
            return await categoryService.Create(category);
        }
    }
}