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
        private readonly CategoryRepository categoryRepository;

        public CategoryController(CategoryRepository categoryRepository)
        {
            this.categoryRepository = categoryRepository;
        }

        [HttpGet("{id}")]
        [Route("[action]")]
        public Category GetById(int id)
        {
            return categoryRepository.GetById(id);
        }

        [HttpGet]
        [Route("[action]")]
        public IEnumerable<Category> GetByIds(IEnumerable<int> ids)
        {
            return ids == null ? null : categoryRepository.GetCategories(ids);
        }

        [HttpGet]
        public IEnumerable<Category> Get()
        {
            return await categoryRepository.GetAll();
        }

        [HttpPut("{id}")]
        public void Update(int id, Category categoryIn)
        {
            var category = categoryRepository.GetById(id);

            await categoryRepository.Update(categoryIn);
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var category = categoryRepository.GetById(id);
            categoryRepository.Delete(category);
        }

        [HttpPost]
        public Category Create([FromBody] Category category)
        {
            if (category == null)
            {
                return null;
            }
            return await categoryRepository.Create(category);
        }
    }
}