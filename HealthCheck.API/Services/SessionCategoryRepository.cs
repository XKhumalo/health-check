using HealthCheck.Model;
using HealthCheck.Repository;
using System.Collections.Generic;
using System.Linq;

namespace HealthCheck.API.Services
{
    public class SessionCategoryRepository
    {
        private readonly CategoryRepository categoryRepository;
        private readonly SessionRepository sessionRepository;
        private readonly DatabaseContext databaseContext;

        public SessionCategoryRepository(CategoryRepository categoryRepository, SessionRepository sessionRepository, DatabaseContext databaseContext)
        {
            this.categoryRepository = categoryRepository;
            this.sessionRepository = sessionRepository;
            this.databaseContext = databaseContext;
        }

        public IEnumerable<SessionCategory> CreateSessionCategory(Session session)
        {
            var persistedSession = databaseContext.Sessions.Add(session);
            var categoryIds = databaseContext.Categories.Where(c => c.IsDeleted == false).Select(c => c.CategoryId);
            var sessionCategories = new List<SessionCategory>();

            foreach (var categoryId in categoryIds)
            {
                var sessionCategory = new SessionCategory()
                {
                    SessionId = persistedSession.Entity.SessionId,
                    CategoryId = categoryId
                };
                var persistedSessionCategory = databaseContext.SessionCategories.Add(sessionCategory);
                sessionCategories.Add(persistedSessionCategory.Entity);
            }

            databaseContext.SaveChanges();
            return sessionCategories;
        }

        public Session CreateSession(Session session)
        {
            return sessionRepository.Create(session);
        }

        public IEnumerable<Category> GetCategories()
        {
            return categoryRepository.GetAll();
        }

        public IEnumerable<SessionCategory> GetSessionCategoriesBySessionId(int sessionId)
        {
            return databaseContext.SessionCategories.Where(sc => sc.SessionId == sessionId);
        }

        public SessionCategory Create(SessionCategory sessionCategory)
        {
            var persistedSessionCategory = databaseContext.Add(sessionCategory);
            SaveChanges();
            return persistedSessionCategory.Entity;
        }

        public void SaveChanges()
        {
            databaseContext.SaveChanges();
        }
    }
}
