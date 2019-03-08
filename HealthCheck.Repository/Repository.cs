using HealthCheck.Model;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace HealthCheck.Repository
{
    public class Repository : IRepository
    {
        private readonly IMongoDatabase _db;

        public Repository(string connectionString, string databaseName)
        {
            _db = new MongoClient(connectionString).GetDatabase(databaseName);
        }

        private IMongoCollection<T> GetCollection<T>() where T : MongoEntity
        {
            return _db.GetCollection<T>(typeof(T).Name);
        }

        public async Task Delete<T>(T entity) where T : MongoEntity
        {
            await GetCollection<T>().DeleteOneAsync<T>(e => e._id.Equals(entity._id));
        }

        public async Task Delete<T>(Expression<Func<T, bool>> exp) where T : MongoEntity
        {
            await GetCollection<T>().DeleteOneAsync<T>(exp);
        }

        public async Task Insert<T>(T entity) where T : MongoEntity
        {
            await GetCollection<T>().InsertOneAsync(entity);
        }

        public async Task InsertMany<T>(IEnumerable<T> entities) where T : MongoEntity
        {
            await GetCollection<T>().InsertManyAsync(entities);
        }

        public async Task<IEnumerable<T>> List<T>() where T : MongoEntity
        {
            return await GetCollection<T>().Find(e => e._id != null).ToListAsync();
        }

        public async Task<IEnumerable<T>> List<T>(Expression<Func<T, bool>> exp) where T : MongoEntity
        {
            return await GetCollection<T>().Find(exp).ToListAsync();
        }

        public async Task<T> Single<T>(Expression<Func<T, bool>> exp) where T : MongoEntity
        {
            return await GetCollection<T>().Find<T>(exp).FirstOrDefaultAsync();
        }

        public async Task Update<T>(object id, T entity) where T : MongoEntity
        {
            var _id = new ObjectId((string)id);
            await GetCollection<T>().ReplaceOneAsync<T>(e => e._id.Equals(_id), entity);
        }
    }
}
