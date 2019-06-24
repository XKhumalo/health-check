using HealthCheck.API.Services;
using HealthCheck.Model;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace HealthCheck.API.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        private readonly UserRepository userRepository;

        public UserController(UserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public IEnumerable<User> Get()
        {
            return userRepository.GetAll();
        }

        [HttpGet]
        public IEnumerable<User> Get(Expression<Func<User, bool>> exp)
        {
            return userRepository.GetUsers(exp);
        }

        [HttpGet("{id:length(24)}")]
        [Route("[action]")]
        public User GetById(int id)
        {
            return userRepository.GetById(id);
        }

        [HttpGet("{name}")]
        [Route("[action]")]
        public User GetByName(string name)
        {
            return await userRepository.SingleOrDefault(u => u.Name.Contains(name));
        }

        [HttpGet("{email}")]
        [Route("[action]")]
        public User GetByEmail(string email)
        {
            return await userRepository.SingleOrDefault(u => u.Email.Contains(email));
        }

        [HttpPost]
        public User Create([FromBody] User user)
        {
            if (user == null)
            {
                return null;
            }

            var persistedUser = await userRepository.Create(user);
            userRepository.SaveChanges();
            return persistedUser;
        }
    }
}