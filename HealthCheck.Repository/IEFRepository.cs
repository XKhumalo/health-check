using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace HealthCheck.Repository
{
    public interface IEFRepository<T> where T : class
    {
        Task<T> Get(object id);
        Task<T> SingleOrDefault(Expression<Func<T, bool>> where);
        Task<T> FirstOrDefault(Expression<Func<T, bool>> where);
        Task<ICollection<T>> GetAll();
        Task<T> Create(T entity);
        Task<IEnumerable<T>> CreateMany(IEnumerable<T> entitiesToInsert);
        T Update(T entity);
        void Delete(T entity);
        void SaveChanges();
    }
}