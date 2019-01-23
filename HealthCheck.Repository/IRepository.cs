using HealthCheck.Model;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace HealthCheck.Repository
{
    interface IRepository
    {
        IEnumerable<T> List<T>() where T : MongoEntity;
        IEnumerable<T> List<T>(Expression<Func<T, bool>> exp) where T : MongoEntity;
        T Single<T>(Expression<Func<T, bool>> exp) where T : MongoEntity;
        void Insert<T>(T entity) where T : MongoEntity;
        void Insert<T>(ICollection<T> entities) where T : MongoEntity;
        void Update<T>(object id, T entity) where T : MongoEntity;
        void Delete<T>(T entity) where T : MongoEntity;
        void Delete<T>(Expression<Func<T, bool>> exp) where T : MongoEntity;
    }
}
