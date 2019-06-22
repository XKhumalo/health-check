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
        private readonly SessionRepository sessionRepository;
        private readonly SessionCategoryRepository sessionCategoryRepository;

        public SessionController(SessionRepository sessionRepository, SessionCategoryRepository sessionCategoryRepository)
        {
            this.sessionRepository = sessionRepository;
            this.sessionCategoryRepository = sessionCategoryRepository;
        }

        [Route("[action]")]
        public Session GetById(int id)
        {
            return sessionRepository.GetById(id);
        }

        [HttpGet]
        public async Task<IEnumerable<Session>> Get()
        {
            return await sessionRepository.GetAll();
        }

        [HttpGet("{key}")]
        [Route("[action]")]
        public async Task<Session> GetBySessionKey(string sessionKey)
        {
            return await sessionRepository.SingleOrDefault(s => s.SessionKey.Contains(sessionKey));
        }

        [HttpGet("{key}")]
        [Route("[action]")]
        public IEnumerable<Session> GetByCreatedById(int createdById)
        {
            return sessionRepository.GetSessions(s => s.CreatedById == createdById);
        }

        [HttpGet("{key}")]
        [Route("[action]")]
        public IEnumerable<SessionCategory> GetSessionCategories(int sessionId)
        {
            return sessionCategoryRepository.GetSessionCategoriesBySessionId(sessionId);
        }

        [HttpPost]
        public async Task<Session> Create([FromBody] Session session)
        {
            if (!ModelState.IsValid || session == null)
            {
                return null;
            }

            var savedSession = await sessionCategoryRepository.CreateSession(session);
            var categories = await sessionCategoryRepository.GetCategories();
            var categoryIds = categories.Select(c => c.CategoryId);
            sessionCategoryRepository.CreateSessionCategory(savedSession, categoryIds);
            sessionCategoryRepository.SaveChanges();
            return session;
        }

        [HttpPost]
        public IEnumerable<SessionCategory> CreateSessionCategories([FromBody] Session session)
        {
            if (!ModelState.IsValid || session == null)
            {
                return null;
            }

            return sessionCategoryRepository.CreateSessionCategory(session);
        }

        [HttpPut("{id}")]
        public IActionResult Update(Session sessionIn)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            sessionRepository.Update(sessionIn);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var session = sessionRepository.GetById(id);

            if (session == null)
            {
                return NotFound();
            }

            sessionRepository.Delete(session);

            return NoContent();

        }
    }
}