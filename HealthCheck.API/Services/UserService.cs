using HealthCheck.Model;
using Microsoft.Extensions.Configuration;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HealthCheck.API.Services
{
    public class UserService 
    {
        private readonly Repository.Repository repository;

        public UserService(IConfiguration config)
        {
            repository = new Repository.Repository(config.GetConnectionString("HealthCheckDB"), "healthcheck");
        }

        public async Task<User> Create(User user)
        {
            await repository.Insert<User>(user);
            return user;
        }

        public async Task<User> Get(string id)
        {
            var docId = new ObjectId(id);
            return await repository.Single<User>(user => user._id == docId);
        }

        public async Task<User> GetByName(string name)
        {
            return await repository.Single<User>(u => u.Name.Equals(name));
        }

        public async Task<User> GetByEmail(string email)
        {
            return await repository.Single<User>(u => u.Email.Equals(email));
        }
    }
}
