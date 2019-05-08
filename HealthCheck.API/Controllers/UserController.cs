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
        private readonly UserRepository userService;

        public UserController(UserRepository userService)
        {
            this.userService = userService;
        }

        public IEnumerable<User> Get()
        {
            return userService.GetAll();
        }

        [HttpGet]
        public IEnumerable<User> Get(Expression<Func<User, bool>> exp)
        {
            return userService.GetUsers(exp);
        }

        [HttpGet("{id:length(24)}")]
        [Route("[action]")]
        public User GetById(int id)
        {
            return userService.GetById(id);
        }

        [HttpGet("{name}")]
        [Route("[action]")]
        public User GetByName(string name)
        {
            return userService.SingleOrDefault(u => u.Name.Contains(name));
        }

        [HttpGet("{email}")]
        [Route("[action]")]
        public User GetByEmail(string email)
        {
            return userService.SingleOrDefault(u => u.Email.Contains(email));
        }

        [HttpPost]
        public User Create([FromBody] User user)
        {
            if (user == null)
            {
                return null;
            }

            var persistedUser = userService.Create(user);
            userService.SaveChanges();
            return persistedUser;
        }
    }
}