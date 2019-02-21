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
    [Route("api/[controller]")]
    [ApiController]
    public class AnswerController : ControllerBase
    {
        private readonly AnswerService _answerService;

        public AnswerController(AnswerService answerService)
        {
            _answerService = answerService;
        }

        [HttpGet("{id:length(24)}")]
        public ActionResult<Answer> Get(string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var answer = _answerService.Get(id);

            if (answer == null)
            {
                return NotFound();
            }
            return answer;
        }

        [HttpGet]
        public ActionResult<List<Answer>> Get()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var answers = _answerService.GetAll();

            if (answers == null)
            {
                return NotFound();
            }

            return answers;
        }

        [HttpPost]
        public IActionResult Create([FromBody] Answer answer)
        {
            if (!ModelState.IsValid || answer == null)
            {
                return BadRequest();
            }

            _answerService.Create(answer);
            return Ok();
        }

        [HttpPost]
        [Route("[action]")]
        public IActionResult CreateList([FromBody] IEnumerable<Answer> answers)
        {
            foreach (var answer in answers)
            {
                Create(answer);
            }
            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult Update(string id, [FromBody] Answer answerIn)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var answer = _answerService.Get(id);
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
            _answerService.Update(id, answerIn);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var answer = _answerService.Get(id);
            if (answer == null)
            {
                return NotFound();
            }

            _answerService.Remove(answer._id);
            return NoContent();
        }

        [HttpDelete]
        public IActionResult Delete(Answer answer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            _answerService.Remove(answer._id);
            return NoContent();
        }
    }
}