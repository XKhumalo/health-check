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
        public async Task<ActionResult<Category>> GetById(string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var category = await categoryService.Get(id);

            if (category == null)
            {
                return NotFound();
            }
            return category;
        }

        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> GetByIds(IEnumerable<string> ids)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var categories = await categoryService.Get(ids);

            if (categories == null)
            {
                return NotFound();
            }
            return Ok(categories);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Category>>> Get()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var categories = await categoryService.GetAll();

            if (categories == null)
            {
                return NotFound();
            }

            return categories.ToList();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, Category categoryIn)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var category = categoryService.Get(id);

            if (category == null)
            {
                return NotFound();
            }

            if (!categoryIn._id.ToString().Equals(id))
            {
                categoryIn._id = new MongoDB.Bson.ObjectId(id);
            }

            await categoryService.Update(id, categoryIn);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var category = await categoryService.Get(id);

            if (category == null)
            {
                return NotFound();
            }

            await categoryService.Delete(category);

            return NoContent();

        }

        [HttpPost]
        public async Task<ActionResult<Category>> Create([FromBody] Category category)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            await categoryService.Create(category);

            return Ok();
        }
    }
}