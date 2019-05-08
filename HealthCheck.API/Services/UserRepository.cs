using HealthCheck.Model;
using HealthCheck.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace HealthCheck.API.Services
{
    public class UserRepository
    {
        private readonly DatabaseContext databaseContext;

        public UserRepository(DatabaseContext databaseContext)
        {
            this.databaseContext = databaseContext;
        }

        public User GetById(int id)
        {
            return databaseContext.Users.Find(id);
        }

        public User SingleOrDefault(Expression<Func<User, bool>> where)
        {
            return  databaseContext.Users.SingleOrDefault(where);
        }

        public User FirstOrDefault(Expression<Func<User, bool>> where)
        {
            return  databaseContext.Users.FirstOrDefault(where);
        }

        public IEnumerable<User> GetAll()
        {
            return databaseContext.Users.ToList();
        }

        public IEnumerable<User> GetUsers(Expression<Func<User, bool>> where)
        {
            return databaseContext.Users.Where(where);
        }

        public User Create(User user)
        {
            var persistedUser = databaseContext.Users.Add(user);
            SaveChanges();
            return persistedUser.Entity;
        }

        public void SaveChanges()
        {
            databaseContext.SaveChanges();
        }
    }
}
