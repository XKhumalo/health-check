using HealthCheck.Model;
using HealthCheck.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace HealthCheck.API.Services
{
    public class SessionService
    {
        private readonly IEFRepository<Session> repository;
        private readonly DatabaseContext databaseContext;

        public SessionService(IEFRepository<Session> repository, DatabaseContext databaseContext)
        {
            this.repository = repository;
            this.databaseContext = databaseContext;
        }

        public Session GetById(int id)
        {
            return databaseContext.Sessions.SingleOrDefault(s => s.SessionId == id);
        }

        public async Task<Session> SingleOrDefault(Expression<Func<Session, bool>> where)
        {
            return await repository.SingleOrDefault(where);
        }

        public async Task<Session> FirstOrDefault(Expression<Func<Session, bool>> where)
        {
            return await repository.FirstOrDefault(where);
        }

        public async Task<ICollection<Session>> GetAll()
        {
            return await repository.GetAll();
        }

        public IQueryable<Session> GetSessions(Expression<Func<Session, bool>> where)
        {
            return databaseContext.Sessions.Where(where);
        }

        public async Task<Session> Create(Session session)
        {
            return await repository.Create(session);
        }

        public async Task<IEnumerable<Session>> Create(IEnumerable<Session> sessions)
        {
            return await repository.CreateMany(sessions);
        }

        public Session Update(Session session)
        {
            var persistedSession =  databaseContext.Sessions.Update(session);
            databaseContext.SaveChanges();
            return persistedSession.Entity;
        }

        public void Delete(Session session)
        {
            repository.Delete(session);
        }

        public void SaveChanges()
        {
            repository.SaveChanges();
        }
    }
}
