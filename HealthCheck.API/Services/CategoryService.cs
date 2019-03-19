using HealthCheck.Model;
using Microsoft.Extensions.Configuration;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace HealthCheck.API.Services
{
    public class CategoryService
    {
        private readonly Repository.Repository repository;

        public CategoryService(IConfiguration config)
        {
            repository = new Repository.Repository(config.GetConnectionString("HealthCheckDB"), "healthcheck");
        }

        public async Task<Category> Create(Category category)
        {
            await repository.Insert<Category>(category);
            return category;
        }

        public async Task<IEnumerable<Category>> GetAll()
        {
            return await repository.List<Category>();
        }

        public async Task<Category> Get(string id)
        {
            var docId = new ObjectId(id);
            return await repository.Single<Category>(category => category._id.Equals(docId));
        }

        public async Task<IEnumerable<Category>> Get(IEnumerable<string> ids)
        {
            var docIds = ids.Select(i => new ObjectId(i));
            return await repository.List<Category>(c => docIds.Contains(c._id));
        }

        public async Task Delete(Category category)
        {
            await repository.Delete<Category>(category);
        }

        public async Task Update(string id, Category category)
        {
            var docId = new ObjectId(id);
            await repository.Update<Category>(id, category);
        }
    }
}
