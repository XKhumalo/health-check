using HealthCheck.Model;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace HealthCheck.Repository
{
    interface IRepository
    {
        Task<IEnumerable<T>> List<T>() where T : MongoEntity;
        Task<IEnumerable<T>> List<T>(Expression<Func<T, bool>> exp) where T : MongoEntity;
        Task<T> Single<T>(Expression<Func<T, bool>> exp) where T : MongoEntity;
        Task Insert<T>(T entity) where T : MongoEntity;
        Task Insert<T>(ICollection<T> entities) where T : MongoEntity;
        Task Update<T>(object id, T entity) where T : MongoEntity;
        Task Delete<T>(T entity) where T : MongoEntity;
        Task Delete<T>(Expression<Func<T, bool>> exp) where T : MongoEntity;
    }
}
