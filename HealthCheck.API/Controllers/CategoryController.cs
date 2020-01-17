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
        private readonly CategoryRepository categoryRepository;

        public CategoryController(CategoryRepository categoryRepository)
        {
            this.categoryRepository = categoryRepository;
        }

        [HttpGet("{id}")]
        [Route("[action]")]
        public async Task<Category> GetById(int id)
        {
            return await categoryRepository.GetByIdAsync(id);
        }

        [HttpGet]
        [Route("[action]")]
        public IEnumerable<Category> GetByIds(IEnumerable<int> ids)
        {
            return ids == null ? null : categoryRepository.GetCategories(ids);
        }

        [HttpGet]
        public async Task<IEnumerable<Category>> Get()
        {
            return await categoryRepository.GetAll();
        }

        [HttpPut("{id}")]
        public async Task Update(int id, Category categoryIn)
        {
            var category = await categoryRepository.GetByIdAsync(id);
            categoryRepository.Update(categoryIn);
        }

        [HttpDelete("{id}")]
        public async Task DeleteAsync(int id)
        {
            var category = await categoryRepository.GetByIdAsync(id);
            categoryRepository.Delete(category);
        }

        [HttpPost]
        public async Task<Category> Create([FromBody] Category category)
        {
            if (category == null)
            {
                return null;
            }
            return await categoryRepository.Create(category);
        }
    }
}