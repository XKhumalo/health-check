using HealthCheck.Model;
using HealthCheck.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace HealthCheck.API.Services
{
    public class AnswerRepository
    {
        private readonly IEFRepository<Answer> answerRepository;
        private readonly DatabaseContext databaseContext;

        public AnswerRepository(IEFRepository<Answer> answerRepository, DatabaseContext databaseContext)
        {
            this.answerRepository = answerRepository;
            this.databaseContext = databaseContext;
        }

        public async Task<Answer> GetById(int id)
        {
            return await answerRepository.Get(id);
        }

        public async Task<Answer> SingleOrDefault(Expression<Func<Answer, bool>> where)
        {
            return await answerRepository.SingleOrDefault(where);
        }

        public async Task<Answer> FirstOrDefault(Expression<Func<Answer, bool>> where)
        {
            return await answerRepository.FirstOrDefault(where);
        }

        public async Task<Answer> GetAnswer(Answer answer)
        {
            return await answerRepository.SingleOrDefault(a => a.AnswerId == answer.AnswerId
                        //&& a.AnswerOptions == answer.AnswerOptions
                        && a.CategoryId == answer.CategoryId
                        && a.SessionId == answer.SessionId
                        && a.UserId == answer.UserId);
        }

        public async Task<ICollection<Answer>> GetAll()
        {
            return await answerRepository.GetAll();
        }

        public IQueryable<Answer> GetAnswers(Expression<Func<Answer, bool>> where)
        {
            return databaseContext.Answers.Where(where);
        }

        public async Task<Answer> Create(Answer answer)
        {
            return await answerRepository.Create(answer);
        }

        public async Task<IEnumerable<Answer>> Create(IEnumerable<Answer> answers)
        {
            return await answerRepository.CreateMany(answers);
        }

        public async Task<Answer> Update(Answer answer)
        {
            return await answerRepository.Update(answer);
        }

        public void Delete(Answer answer)
        {
            answerRepository.Delete(answer);
        }

        public void SaveChanges()
        {
            answerRepository.SaveChanges();
        }
    }
}
