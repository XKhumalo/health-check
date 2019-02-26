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
        public async Task<ActionResult<Session>> GetById(string id)
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
            return session;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Session>>> Get()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var sessions = await sessionService.GetAll();

            if (sessions == null)
            {
                return NotFound();
            }

            return sessions.ToList();
        }

        [HttpGet("{key}")]
        [Route("[action]")]
        public async Task<ActionResult<Session>> GetBySessionKey(string sessionKey)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var session = await sessionService.GetBySessionKey(sessionKey);

            if (session == null)
            {
                return NotFound();
            }
            return session;
        }

        [HttpGet("{key}")]
        [Route("[action]")]
        public async Task<ActionResult<Session>> GetByCreatedById(string createdById)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var session = await sessionService.GetByCreatedById(createdById);

            if (session == null)
            {
                return NotFound();
            }
            return session;
        }

        [HttpPost]
        public async Task<ActionResult<Session>> Create([FromBody] Session session)
        {
            if (!ModelState.IsValid || session == null)
            {
                return BadRequest();
            }

            await sessionService.Create(session);

            return session;
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