using HealthCheck.Model;
using HealthCheck.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace HealthCheck.API.Services
{
    public class AnswerService
    {
        private readonly IEFRepository<Answer> repository;
        private readonly DatabaseContext databaseContext;

        public AnswerService(IEFRepository<Answer> repository, DatabaseContext databaseContext)
        {
            this.repository = repository;
            this.databaseContext = databaseContext;
        }

        public async Task<Answer> GetById(int id)
        {
            return await repository.Get(id);
        }

        public async Task<Answer> SingleOrDefault(Expression<Func<Answer, bool>> where)
        {
            return await repository.SingleOrDefault(where);
        }

        public async Task<Answer> FirstOrDefault(Expression<Func<Answer, bool>> where)
        {
            return await repository.FirstOrDefault(where);
        }

        public async Task<Answer> GetAnswer(Answer answer)
        {
            return await repository.SingleOrDefault(a => a.AnswerId == answer.AnswerId
                        //&& a.AnswerOptions == answer.AnswerOptions
                        && a.CategoryId == answer.CategoryId
                        && a.SessionId == answer.SessionId
                        && a.UserId == answer.UserId);
        }

        public async Task<ICollection<Answer>> GetAll()
        {
            return await repository.GetAll();
        }

        public IQueryable<Answer> GetAnswers(Expression<Func<Answer, bool>> where)
        {
            return databaseContext.Answers.Where(where);
        }

        public async Task<Answer> Create(Answer answer)
        {
            return await repository.Create(answer);
        }

        public async Task<IEnumerable<Answer>> Create(IEnumerable<Answer> answers)
        {
            return await repository.CreateMany(answers);
        }

        public async Task<Answer> Update(Answer answer)
        {
            return await repository.Update(answer);
        }

        public void Delete(Answer answer)
        {
            repository.Delete(answer);
        }

        public void SaveChanges()
        {
            repository.SaveChanges();
        }
    }
}
