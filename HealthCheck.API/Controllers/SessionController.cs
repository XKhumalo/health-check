using HealthCheck.API.Services;
using HealthCheck.Model;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HealthCheck.API.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class SessionController : ControllerBase
    {
        private readonly SessionRepository sessionService;
        private readonly SessionCategoryRepository sessionCategoryService;

        public SessionController(SessionRepository sessionService, SessionCategoryRepository sessionCategoryService)
        {
            this.sessionService = sessionService;
            this.sessionCategoryService = sessionCategoryService;
        }

        [Route("[action]")]
        public Session GetById(int id)
        {
            return sessionService.GetById(id);
        }

        [Route("[action]")]
        public async Task<Session> GetByIdAsync(int id)
        {
            return await sessionService.SingleOrDefaultAsync(s => s.SessionId == id);
        }

        [HttpGet]
        public IEnumerable<Session> Get()
        {
            return sessionService.GetAll();
        }

        [HttpGet("{key}")]
        [Route("[action]")]
        public async Task<Session> GetBySessionKey(string sessionKey)
        {
            return await sessionService.SingleOrDefaultAsync(s => s.SessionKey.Contains(sessionKey));
        }

        [HttpGet("{key}")]
        [Route("[action]")]
        public IEnumerable<Session> GetByCreatedById(int createdById)
        {
            return sessionService.GetSessions(s => s.CreatedById == createdById);
        }

        [HttpGet("{key}")]
        [Route("[action]")]
        public IEnumerable<SessionCategory> GetSessionCategories(int sessionId)
        {
            return sessionCategoryService.GetSessionCategoriesBySessionId(sessionId);
        }

        [HttpPost]
        public Session Create([FromBody] Session session)
        {
            if (!ModelState.IsValid || session == null)
            {
                return null;
            }

            sessionCategoryService.CreateSessionCategory(session);
            return session;
        }

        [HttpPost]
        public IEnumerable<SessionCategory> CreateSessionCategories([FromBody] Session session)
        {
            if (!ModelState.IsValid || session == null)
            {
                return null;
            }

            return sessionCategoryService.CreateSessionCategory(session);
        }

        [HttpPut("{id}")]
        public IActionResult Update(Session sessionIn)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            sessionService.Update(sessionIn);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var session = sessionService.GetById(id);

            if (session == null)
            {
                return NotFound();
            }

            sessionService.Delete(session);

            return NoContent();

        }
    }
}