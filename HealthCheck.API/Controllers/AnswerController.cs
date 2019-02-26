using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HealthCheck.API.Services;
using HealthCheck.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HealthCheck.API.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class AnswerController : Controller
    {
        private readonly AnswerService answerService;

        public AnswerController(AnswerService answerService)
        {
            this.answerService = answerService;
        }

        [HttpGet("{id:length(24)}")]
        [Route("[action]")]
        public async Task<ActionResult<Answer>> GetById(string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var answer = await answerService.Get(id);

            if (answer == null)
            {
                return NotFound();
            }
            return answer;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Answer>>> Get()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var answers = await answerService.GetAll();

            if (answers == null)
            {
                return NotFound();
            }

            return answers.ToList();
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Answer answer)
        {
            if (!ModelState.IsValid || answer == null)
            {
                return BadRequest();
            }

            await answerService.Create(answer);
            return Ok();
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> CreateList([FromBody] IEnumerable<Answer> answers)
        {
            foreach (var answer in answers)
            {
                await Create(answer);
            }
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, [FromBody] Answer answerIn)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var answer = await answerService.Get(id);
            if (answer == null)
            {
                return BadRequest();
            }
            answerIn._id = new MongoDB.Bson.ObjectId(id);
            answerIn.SessionId = answer.SessionId;
            if (answerIn.Equals(answer))
            {
                return NoContent();
            }
            await answerService.Update(id, answerIn);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var answer = await answerService.Get(id);
            if (answer == null)
            {
                return NotFound();
            }

            await answerService.Remove(answer._id);
            return NoContent();
        }
        
    }
}