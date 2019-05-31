﻿using HealthCheck.Model;
using HealthCheck.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

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
            return databaseContext.Answers.FirstOrDefault(x => x.AnswerId == id);
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

        public Answer InsertOrUpdateAnswer(Answer answer)
        {
            if (answer == null)
            {
                return null;
            }

            var dbAnswer = FirstOrDefault(a => a.UserId == answer.UserId && a.SessionId == answer.SessionId && a.CategoryId == answer.CategoryId);
            if (dbAnswer != null)
            {
                dbAnswer.AnswerOptionId = answer.AnswerOptionId;
                return Update(dbAnswer);
            }
            return Create(answer);
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
