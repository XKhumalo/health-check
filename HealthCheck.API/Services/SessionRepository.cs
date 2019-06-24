using HealthCheck.Model;
using HealthCheck.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace HealthCheck.API.Services
{
    public class SessionRepository
    {
        private readonly IEFRepository<Session> sessionRepository;
        private readonly DatabaseContext databaseContext;

        public SessionRepository(IEFRepository<Session> sessionRepository, DatabaseContext databaseContext)
        {
            this.sessionRepository = sessionRepository;
            this.databaseContext = databaseContext;
        }

        public async Task<Session> GetByIdAsync(int id)
        {
            return await databaseContext.Sessions.SingleOrDefaultAsync(s => s.SessionId == id);
        }

        public async Task<Session> SingleOrDefault(Expression<Func<Session, bool>> where)
        {
            return await sessionRepository.SingleOrDefault(where);
        }

        public async Task<Session> FirstOrDefault(Expression<Func<Session, bool>> where)
        {
            return await sessionRepository.FirstOrDefault(where);
        }

        public async Task<ICollection<Session>> GetAll()
        {
            return await sessionRepository.GetAll();
        }

        public IQueryable<Session> GetSessions(Expression<Func<Session, bool>> where)
        {
            return databaseContext.Sessions.Where(where);
        }

        public async Task<Session> Create(Session session)
        {
            return await sessionRepository.Create(session);
        }

        public async Task<IEnumerable<Session>> Create(IEnumerable<Session> sessions)
        {
            return await sessionRepository.CreateMany(sessions);
        }

        public Session Update(Session session)
        {
            var persistedSession =  databaseContext.Sessions.Update(session);
            databaseContext.SaveChanges();
            return persistedSession.Entity;
        }

        public void Delete(Session session)
        {
            sessionRepository.Delete(session);
        }

        public void SaveChanges()
        {
            sessionRepository.SaveChanges();
        }
    }
}
