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
        private readonly DatabaseContext databaseContext;

        public SessionRepository(DatabaseContext databaseContext)
        {
            this.databaseContext = databaseContext;
        }

        public Session GetById(int id)
        {
            return databaseContext.Sessions.SingleOrDefault(s => s.SessionId == id);
        }

        public Session SingleOrDefault(Expression<Func<Session, bool>> where)
        {
            return databaseContext.Sessions.SingleOrDefault(where);
        }

        public async Task<Session> SingleOrDefaultAsync(Expression<Func<Session, bool>> where)
        {
            return await databaseContext.Sessions.SingleOrDefaultAsync(where);
        }

        public Session FirstOrDefault(Expression<Func<Session, bool>> where)
        {
            return databaseContext.Sessions.FirstOrDefault(where);
        }

        public async Task<Session> FirstOrDefaultAsync(Expression<Func<Session, bool>> where)
        {
            return await databaseContext.Sessions.FirstOrDefaultAsync(where);
        }

        public IEnumerable<Session> GetAll()
        {
            return databaseContext.Sessions.ToList();
        }

        public IQueryable<Session> GetSessions(Expression<Func<Session, bool>> where)
        {
            return databaseContext.Sessions.Where(where);
        }

        public Session Create(Session session)
        {
            var persistedSession = databaseContext.Sessions.Add(session);
            SaveChanges();
            return persistedSession.Entity;
        }

        public Session Update(Session session)
        {
            var persistedSession = databaseContext.Sessions.Update(session);
            databaseContext.SaveChanges();
            return persistedSession.Entity;
        }

        public void Delete(Session session)
        {
            databaseContext.Sessions.Remove(session);
        }

        public void SaveChanges()
        {
            databaseContext.SaveChanges();
        }
    }
}
