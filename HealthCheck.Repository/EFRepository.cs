using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace HealthCheck.Repository
{
    public class EFRepository<T> : IEFRepository<T> where T : class
    {
        private readonly DatabaseContext databaseContext;
        private DbSet<T> entities;

        public EFRepository(DatabaseContext databaseContext)
        {
            this.databaseContext = databaseContext;
            entities = databaseContext.Set<T>();
        }

        public async Task<T> Create(T entity)
        {
            await entities.AddAsync(entity);
            return entity;
        }

        public async Task<IEnumerable<T>> CreateMany(IEnumerable<T> entitiesToInsert)
        {
            await entities.AddRangeAsync(entitiesToInsert);
            return entitiesToInsert;
        }

        public void Delete(T entity)
        {
            entities.Remove(entity);
        }

        public async Task<T> FirstOrDefault(Expression<Func<T, bool>> where)
        {
            return await entities.FirstOrDefaultAsync(where);
        }

        public async Task<T> Get(object id)
        {
            return await entities.FindAsync(id);
        }

        public async Task<ICollection<T>> GetAll()
        {
            return await entities.ToListAsync();
        }

        public void SaveChanges()
        {
            databaseContext.SaveChanges();
        }

        public async Task<T> SingleOrDefault(Expression<Func<T, bool>> where)
        {
            return await entities.SingleOrDefaultAsync(where);
        }

        public T Update(T entity)
        {
            entities.Update(entity);
            return entity;
        }
    }
}
