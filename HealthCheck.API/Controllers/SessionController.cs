using HealthCheck.API.Services;
using HealthCheck.Model;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HealthCheck.API.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class SessionController : ControllerBase
    {
        private readonly SessionService sessionService;
        private readonly SessionCategoryService sessionCategoryService;

        public SessionController(SessionService sessionService, SessionCategoryService sessionCategoryService)
        {
            this.sessionService = sessionService;
            this.sessionCategoryService = sessionCategoryService;
        }

        [HttpGet("{id:length(24)}")]
        [Route("[action]")]
        public Session GetById(int id)
        {
            return sessionService.GetById(id);
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
            return await sessionService.SingleOrDefault(s => s.SessionKey.Contains(sessionKey));
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
        public async Task<Session> Create([FromBody] Session session)
        {
            if (!ModelState.IsValid || session == null)
            {
                return null;
            }

            var savedSession = await sessionCategoryService.CreateSession(session);
            var categories = await sessionCategoryService.GetCategories();
            var categoryIds = categories.Select(c => c.CategoryId);
            sessionCategoryService.CreateSessionCategory(savedSession, categoryIds);
            sessionCategoryService.SaveChanges();
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
        public async Task<IActionResult> Delete(int id)
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