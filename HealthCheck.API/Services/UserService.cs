using HealthCheck.Model;
using HealthCheck.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace HealthCheck.API.Services
{
    public class UserService
    {
        private readonly IEFRepository<User> repository;
        private readonly DatabaseContext databaseContext;

        public UserService(IEFRepository<User> repository, DatabaseContext databaseContext)
        {
            this.repository = repository;
            this.databaseContext = databaseContext;
        }

        public async Task<User> Create(User user)
        {
            return await repository.Create(user);
        }

        public IEnumerable<User> GetAll()
        {
            return databaseContext.Users.ToList();
        }

        public IEnumerable<User> GetUsers(Expression<Func<User, bool>> where)
        {
            return databaseContext.Users.Where(where);
        }

        public User GetById(int id)
        {
            return databaseContext.Users.SingleOrDefault(u => u.UserId == id);
        }

        public async Task<User> SingleOrDefault(Expression<Func<User, bool>> where)
        {
            return await repository.SingleOrDefault(where);
        }

        public async Task<User> FirstOrDefault(Expression<Func<User, bool>> where)
        {
            return await repository.FirstOrDefault(where);
        }

        public void SaveChanges()
        {
            databaseContext.SaveChanges();
        }
    }
}
