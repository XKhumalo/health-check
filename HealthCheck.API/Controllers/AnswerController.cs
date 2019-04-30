using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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
        public async Task<Answer> GetById(string id)
        {
            return await answerService.Get(id);
        }

        [HttpGet]
        public async Task<IEnumerable<Answer>> Get(Expression<Func<Answer, bool>> exp)
        {
            return await answerService.Get(exp);
        }

        [HttpGet]
        [Route("[action]")]
        public async Task<IEnumerable<Answer>> GetAll()
        {
            return await answerService.GetAll();
        }

        [HttpPost]
        public async Task<Answer> Create([FromBody] Answer answer)
        {
            if (answer == null)
            {
                return null;
            }
            var existingAnswer = await answerService.Get(a => a.CategoryId.Equals(answer.CategoryId) && a.SessionId.Equals(answer.SessionId) && a.UserId.Equals(answer.UserId));
            if (existingAnswer != null)
            {
                await answerService.Update(existingAnswer.FirstOrDefault()._id.ToString(), answer);
                answer._id = existingAnswer.FirstOrDefault()._id;
                return answer;
            }
            return await answerService.Create(answer);
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IEnumerable<Answer>> CreateList([FromBody] IEnumerable<Answer> answers)
        {
            if (answers == null)
            {
                return null;
            }

            return await answerService.Create(answers);
        }

        [HttpPut("{id}")]
        public async Task Update(string id, [FromBody] Answer answerIn)
        {
            var answer = await answerService.Get(id);
            answerIn._id = new MongoDB.Bson.ObjectId(id);
            answerIn.SessionId = answer.SessionId;
            await answerService.Update(id, answerIn);
        }

        [HttpDelete("{id}")]
        public async Task Delete(string id)
        {
            var answer = await answerService.Get(id);
            await answerService.Remove(answer._id);
        }
        
    }
}