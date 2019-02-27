using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HealthCheck.API.Services;
using HealthCheck.Model;
using Microsoft.AspNetCore.Mvc;

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
        public async Task<Category> GetById(string id)
        {
            return await categoryService.Get(id);
        }

        [HttpGet]
        [Route("[action]")]
        public async Task<IEnumerable<Category>> GetByIds(IEnumerable<string> ids)
        {
            return await categoryService.Get(ids);
        }

        [HttpGet]
        public async Task<IEnumerable<Category>> Get()
        {
            return await categoryService.GetAll();
        }

        [HttpPut("{id}")]
        public async Task Update(string id, Category categoryIn)
        {
            var category = await categoryService.Get(id);

            if (category != null)
            {
                if (!categoryIn._id.ToString().Equals(id))
                {
                    categoryIn._id = new MongoDB.Bson.ObjectId(id);
                }

                await categoryService.Update(id, categoryIn);
            }
        }

        [HttpDelete("{id}")]
        public async Task Delete(string id)
        {
            var category = await categoryService.Get(id);
            await categoryService.Delete(category);
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