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
    public class UserRepository
    {
        private readonly IEFRepository<User> userRepository;
        private readonly IEFRepository<SessionOnlyUser> sessionUserRepository;
        private readonly DatabaseContext databaseContext;

        public UserRepository(IEFRepository<User> userRepository, IEFRepository<SessionOnlyUser> sessionUserRepository, DatabaseContext databaseContext)
        {
            this.userRepository = userRepository;
            this.sessionUserRepository = sessionUserRepository;
            this.databaseContext = databaseContext;
        }

        public async Task<User> Create(User user)
        {
            return await userRepository.Create(user);
        }

        public async Task<SessionOnlyUser> CreateSessionOnlyUser(SessionOnlyUser sessionOnlyUser)
        {
            return await sessionUserRepository.Create(sessionOnlyUser);
        }

        public IEnumerable<User> GetAll()
        {
            return databaseContext.Users.ToList();
        }

        public IEnumerable<User> GetUsers(Expression<Func<User, bool>> where)
        {
            return databaseContext.Users.Where(where);
        }

        public async Task<User> GetByIdAsync(int id)
        {
            return await databaseContext.Users.SingleOrDefaultAsync(u => u.UserId == id);
        }

        public async Task<SessionOnlyUser> GetSessionOnlyUserByIdAsync(int id)
        {
            return await databaseContext.SessionOnlyUsers.SingleOrDefaultAsync(u => u.SessionOnlyUserId == id);
        }

        public async Task<User> SingleOrDefault(Expression<Func<User, bool>> where)
        {
            return await userRepository.SingleOrDefault(where);
        }

        public async Task<User> FirstOrDefault(Expression<Func<User, bool>> where)
        {
            return await userRepository.FirstOrDefault(where);
        }

        public void SaveChanges()
        {
            databaseContext.SaveChanges();
        }
    }
}
