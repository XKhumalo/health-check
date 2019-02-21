using HealthCheck.Model;
using Microsoft.Extensions.Configuration;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public Answer Get(string id)
        {
            var docId = new ObjectId(id);
            return repository.Single<Answer>(a => a._id == docId);
        }

        public Answer Get(Answer answer)
        {
            return repository.Single<Answer>(a =>
                        a.CategoryChosen.Equals(answer.CategoryChosen) &&
                        a.CategoryId.Equals(answer.CategoryId) &&
                        a.SessionId.Equals(answer.SessionId) &&
                        a.UserId.Equals(answer.UserId)
                    );
        }

        public List<Answer> Get(List<string> ids)
        {
            return repository.List<Answer>(a => ids.Contains(a._id.ToString())).ToList();
        }

        public List<Answer> GetAll()
        {
            return repository.List<Answer>(a => a._id != null).ToList();
        }

        public Answer Create(Answer answer)
        {
            repository.Insert<Answer>(answer);
            return answer;
        }

        public List<Answer> Create(List<Answer> answers)
        {
            repository.Insert<Answer>(answers);
            return answers;
        }

        public void Update(string id, Answer answer)
        {
            var docId = new ObjectId(id);
            repository.Update<Answer>(id, answer);
        }

        public void Remove(Answer answer)
        {
            var docId = new ObjectId(answer._id.ToString());
            answer.IsDeleted = true;
            repository.Update<Answer>(docId, answer);
        }

        public void Remove(ObjectId id)
        {
            var answer = repository.Single<Answer>(a => a._id == id);
            answer.IsDeleted = true;
            repository.Update<Answer>(id, answer);
        }
    }
}
