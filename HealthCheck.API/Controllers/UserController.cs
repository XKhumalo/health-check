using HealthCheck.API.Services;
using HealthCheck.Model;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace HealthCheck.API.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        private readonly UserService userService;

        public UserController(UserService userService)
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
        public async Task<User> GetByName(string name)
        {
            return await userService.SingleOrDefault(u => u.Name.Contains(name));
        }

        [HttpGet("{email}")]
        [Route("[action]")]
        public async Task<User> GetByEmail(string email)
        {
            return await userService.SingleOrDefault(u => u.Email.Contains(email));
        }

        [HttpPost]
        public async Task<User> Create([FromBody] User user)
        {
            if (user == null)
            {
                return null;
            }

            var persistedUser = await userService.Create(user);
            userService.SaveChanges();
            return persistedUser;
        }
    }
}