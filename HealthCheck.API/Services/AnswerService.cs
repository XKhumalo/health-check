using HealthCheck.Model;
using Microsoft.Extensions.Configuration;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace HealthCheck.API.Services
{
    public class AnswerService
    {
        private readonly Repository.Repository repository;

        public AnswerService(IConfiguration config)
        {
            repository = new Repository.Repository(config.GetConnectionString("HealthCheckDB"), "healthcheck");
        }

        public async Task<Answer> Get(string id)
        {
            var docId = new ObjectId(id);
            return await repository.Single<Answer>(a => a._id == docId);
        }

        public async Task<Answer> Get(Answer answer)
        {
            return await repository.Single<Answer>(a =>
                        a.CategoryChosen.Equals(answer.CategoryChosen) &&
                        a.CategoryId.Equals(answer.CategoryId) &&
                        a.SessionId.Equals(answer.SessionId) &&
                        a.UserId.Equals(answer.UserId)
                    );
        }

        public async Task<IEnumerable<Answer>> Get(List<string> ids)
        {
            return await repository.List<Answer>(a => ids.Contains(a._id.ToString()));
        }

        public async Task<IEnumerable<Answer>> Get(Expression<Func<Answer, bool>> exp)
        {
            return await repository.List<Answer>(exp);
        }

        public async Task<IEnumerable<Answer>> GetAll()
        {
            return await repository.List<Answer>(a => a._id != null);
        }

        public async Task<Answer> Create(Answer answer)
        {
            await repository.Insert<Answer>(answer);
            return answer;
        }

        public async Task<IEnumerable<Answer>> Create(IEnumerable<Answer> answers)
        {
            await repository.InsertMany<Answer>(answers);
            return answers;
        }

        public async Task Update(string id, Answer answer)
        {
            var docId = new ObjectId(id);
            await repository.Update<Answer>(id, answer);
        }

        public async Task Remove(Answer answer)
        {
            var docId = new ObjectId(answer._id.ToString());
            answer.IsDeleted = true;
            await repository.Update<Answer>(docId, answer);
        }

        public async Task Remove(ObjectId id)
        {
            var answer = await repository.Single<Answer>(a => a._id == id);
            answer.IsDeleted = true;
            await repository.Update<Answer>(id, answer);
        }
    }
}
