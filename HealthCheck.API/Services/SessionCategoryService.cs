using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HealthCheck.Model;
using HealthCheck.Repository;

namespace HealthCheck.API.Services
{
    public class SessionCategoryService
    {
        private readonly IEFRepository<SessionCategory> sessionCategoryRepository;
        private readonly IEFRepository<Category> categoryRepository;
        private readonly IEFRepository<Session> sessionRepository;
        private readonly DatabaseContext databaseContext;

        public SessionCategoryService(IEFRepository<SessionCategory> sessionCategoryRepository, IEFRepository<Category> categoryRepository, 
            IEFRepository<Session> sessionRepository, DatabaseContext databaseContext)
        {
            this.sessionCategoryRepository = sessionCategoryRepository;
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

        public async Task<Session> CreateSession(Session session)
        {
            return await sessionRepository.Create(session);
        }

        public async Task<ICollection<Category>> GetCategories()
        {
            return await categoryRepository.GetAll();
        }

        public IEnumerable<SessionCategory> GetSessionCategoriesBySessionId(int sessionId)
        {
            return databaseContext.SessionCategories.Where(sc => sc.SessionId == sessionId);
        }

        //TODO: NOT BEING USED
        public async void CreateSessionCategory(Session session, IEnumerable<int> categoryIds)
        {
            foreach (var categoryId in categoryIds)
            {
                var sessionCategory = new SessionCategory()
                {
                    SessionId = session.SessionId,
                    CategoryId = categoryId
                };
                await Create(sessionCategory);
            }
        }

        public async Task<SessionCategory> Create(SessionCategory sessionCategory)
        {
            return await sessionCategoryRepository.Create(sessionCategory);
        }

        public void SaveChanges()
        {
            sessionCategoryRepository.SaveChanges();
        }
    }
}
