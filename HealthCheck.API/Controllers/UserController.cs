using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HealthCheck.API.Services;
using HealthCheck.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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

        public async Task<IEnumerable<User>> Get()
        {
            return await userService.Get();
        }

        [HttpGet("{id:length(24)}")]
        [Route("[action]")]
        public async Task<User> GetById(string id)
        {
            return await userService.GetById(id);
        }

        [HttpGet("{name}")]
        [Route("[action]")]
        public async Task<User> GetByName(string name)
        {
            return await userService.GetByName(name);
        }

        [HttpGet("{email}")]
        [Route("[action]")]
        public async Task<User> GetByEmail(string email)
        {
            return await userService.GetByEmail(email);
        }

        [HttpPost]
        public async Task<User> Create([FromBody] User user)
        {
            if (user == null)
            {
                return null;
            }

            return await userService.Create(user);
        }
    }
}