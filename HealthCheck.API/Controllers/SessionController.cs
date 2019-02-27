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
    public class SessionController : ControllerBase
    {
        private readonly SessionService sessionService;

        public SessionController(SessionService sessionService)
        {
            this.sessionService = sessionService;
        }

        [HttpGet("{id:length(24)}")]
        [Route("[action]")]
        public async Task<Session> GetById(string id)
        {
            return await sessionService.Get(id);
        }

        [HttpGet]
        public async Task<IEnumerable<Session>> Get()
        {
            return await sessionService.GetAll();
        }

        [HttpGet("{key}")]
        [Route("[action]")]
        public async Task<Session> GetBySessionKey(string sessionKey)
        {
            return await sessionService.GetBySessionKey(sessionKey);
        }

        [HttpGet("{key}")]
        [Route("[action]")]
        public async Task<Session> GetByCreatedById(string createdById)
        {
            return await sessionService.GetByCreatedById(createdById);
        }

        [HttpPost]
        public async Task<Session> Create([FromBody] Session session)
        {
            if (!ModelState.IsValid || session == null)
            {
                return null;
            }

            return await sessionService.Create(session);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, Session sessionIn)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var session = sessionService.Get(id);

            if (session == null)
            {
                return NotFound();
            }

            if (!sessionIn._id.ToString().Equals(id))
            {
                sessionIn._id = new MongoDB.Bson.ObjectId(id);
            }

            await sessionService.Update(id, sessionIn);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var session = await sessionService.Get(id);

            if (session == null)
            {
                return NotFound();
            }

            await sessionService.Delete(session);

            return NoContent();

        }
    }
}