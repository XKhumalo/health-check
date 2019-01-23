using HealthCheck.Model;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace HealthCheck.Repository
{
    public class Repository : IRepository
    {
        private readonly IMongoCollection<Session> _session;
        private readonly IMongoDatabase _db;

        public Repository(string connectionString, string databaseName)
        {
            _db = new MongoClient(connectionString).GetDatabase(databaseName);
        }

        private IMongoCollection<T> GetCollection<T>() where T : MongoEntity
        {
            return _db.GetCollection<T>(typeof(T).Name);
        }

        public void Delete<T>(T entity) where T : MongoEntity
        {
            GetCollection<T>().DeleteOne<T>(e => e._id.Equals(entity._id));
        }

        public void Delete<T>(Expression<Func<T, bool>> exp) where T : MongoEntity
        {
            GetCollection<T>().DeleteOne<T>(exp);
        }

        public void Insert<T>(T entity) where T : MongoEntity
        {
            GetCollection<T>().InsertOne(entity);
        }

        public void Insert<T>(ICollection<T> entities) where T : MongoEntity
        {
            GetCollection<T>().InsertMany(entities);
        }

        public IEnumerable<T> List<T>() where T : MongoEntity
        {
            return GetCollection<T>().Find(e => e._id != null).ToEnumerable();
        }

        public IEnumerable<T> List<T>(Expression<Func<T, bool>> exp) where T : MongoEntity
        {
            return GetCollection<T>().Find(exp).ToEnumerable();
        }

        public T Single<T>(Expression<Func<T, bool>> exp) where T : MongoEntity
        {
            return GetCollection<T>().Find(exp).FirstOrDefault();
        }

        public void Update<T>(object id, T entity) where T : MongoEntity
        {
            var _id = new ObjectId((string)id);
            GetCollection<T>().ReplaceOne<T>(e => e._id.Equals(_id), entity);
        }
    }
}
