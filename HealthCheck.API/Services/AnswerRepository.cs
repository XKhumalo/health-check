using HealthCheck.Model;
using HealthCheck.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace HealthCheck.API.Services
{
    public class AnswerRepository
    {
        private readonly DatabaseContext databaseContext;

        public AnswerRepository(DatabaseContext databaseContext)
        {
            this.databaseContext = databaseContext;
        }

        public Answer GetById(int id)
        {
            return databaseContext.Answers.Find(id);
        }

        public Answer SingleOrDefault(Expression<Func<Answer, bool>> where)
        {
            return databaseContext.Answers.SingleOrDefault(where);
        }

        public Answer FirstOrDefault(Expression<Func<Answer, bool>> where)
        {
            return databaseContext.Answers.FirstOrDefault(where);
        }

        public IQueryable<Answer> GetAnswers(Expression<Func<Answer, bool>> where)
        {
            return databaseContext.Answers.Where(where);
        }

        public Answer Create(Answer answer)
        {
            var persistedAnswer = databaseContext.Answers.Add(answer);
            SaveChanges();
            return persistedAnswer.Entity;
        }

        public IEnumerable<Answer> Create(IEnumerable<Answer> answers)
        {
            var answersList = answers.ToList();
            foreach (var answer in answersList)
            {
                databaseContext.Answers.Add(answer);
            }
            SaveChanges();
            return answersList;
        }

        public Answer Update(Answer answer)
        {
            var updatedAnswer = databaseContext.Answers.Update(answer);
            SaveChanges();
            return updatedAnswer.Entity;
        }

        public void Delete(Answer answer)
        {
            databaseContext.Answers.Remove(answer);
            SaveChanges();
        }

        public void SaveChanges()
        {
            databaseContext.SaveChanges();
        }
    }
}
