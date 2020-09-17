using HealthCheck.Model;
using HealthCheck.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace HealthCheck.API.Services
{
    public class AnswerRepository
    {
        private readonly IEFRepository<Answer> answerRepository;
        private readonly IEFRepository<AnswerOption> answerOptionRepository;
        private readonly IEFRepository<GuestUserAnswer> guestAnswerRepository;
        private readonly DatabaseContext databaseContext;

        public AnswerRepository(IEFRepository<Answer> answerRepository, IEFRepository<AnswerOption> answerOptionRepository, IEFRepository<GuestUserAnswer> guestAnswerRepository, DatabaseContext databaseContext)
        {
            this.answerRepository = answerRepository;
            this.answerOptionRepository = answerOptionRepository;
            this.guestAnswerRepository = guestAnswerRepository;
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

        public IEnumerable<AnswerOption> GetAnswerOptions()
        {
            return databaseContext.AnswerOptions.ToList();
        }

        public IQueryable<Answer> GetAnswers(Expression<Func<Answer, bool>> where)
        {
            return databaseContext.Answers.Where(where);//.AsNoTracking().Include(x=>x.User).Include(x=>x.Category).Include(x=>x.Session);
        }
        
        public IQueryable<GuestUserAnswer> GetGuestAnswers(Expression<Func<GuestUserAnswer, bool>> where)
        {
            return databaseContext.GuestUserAnswers.Where(where);
        }

        public async Task<Answer> InsertOrUpdateAnswer(Answer answer)
        {
            if (answer == null)
            {
                return null;
            }

            var dbAnswer = await FirstOrDefault(a => a.UserId == answer.UserId && a.SessionId == answer.SessionId && a.CategoryId == answer.CategoryId);
            if (dbAnswer != null)
            {
                dbAnswer.AnswerOptionId = answer.AnswerOptionId;
                return Update(dbAnswer);
            }
            return await Create(answer);
        }

        public async Task<GuestUserAnswer> InsertOrUpdateAnswer(GuestUserAnswer answer)
        {
            if (answer == null)
            {
                return null;
            }

            var dbAnswer =  databaseContext.GuestUserAnswers.FirstOrDefault(a => a.SessionOnlyUserId == answer.SessionOnlyUserId && a.SessionId == answer.SessionId && a.CategoryId == answer.CategoryId);
            if (dbAnswer != null)
            {
                dbAnswer.AnswerOptionId = answer.AnswerOptionId;
                return Update(dbAnswer);
            }
            return await Create(answer);
        }

        public async Task<Answer> Create(Answer answer)
        {
            return await answerRepository.Create(answer);
        }

        public async Task<IEnumerable<Answer>> Create(IEnumerable<Answer> answers)
        {
            return await answerRepository.CreateMany(answers);
        }

        public async Task<GuestUserAnswer> Create(GuestUserAnswer answer)
        {
            return await guestAnswerRepository.Create(answer);
        }

        public async Task<IEnumerable<GuestUserAnswer>> Create(IEnumerable<GuestUserAnswer> answers)
        {
            return await guestAnswerRepository.CreateMany(answers);
        }

        public Answer Update(Answer answer)
        {
            return answerRepository.Update(answer);
        }

        public GuestUserAnswer Update(GuestUserAnswer answer)
        {
            return guestAnswerRepository.Update(answer);
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
