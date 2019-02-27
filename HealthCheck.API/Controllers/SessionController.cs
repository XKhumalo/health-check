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
        public async Task<List<Session>> Get()
        {
            var sessionsEnum = await sessionService.GetAll();
            return  sessionsEnum.ToList();
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

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Session session)
        {
            if (!ModelState.IsValid || session == null)
            {
                return BadRequest();
            }

            await sessionService.Create(session);

            return Ok();
        }
    }
}