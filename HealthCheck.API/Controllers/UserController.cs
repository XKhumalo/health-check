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

        [HttpGet("{id:length(24)}")]
        public async Task<ActionResult<User>> Get(string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var user = await userService.Get(id);

            if (user == null)
            {
                return NotFound();
            }
            return user;
        }

        [HttpGet("{name}")]
        [Route("[action]")]
        public async Task<ActionResult<User>> GetByName(string name)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var user = await userService.GetByName(name);

            if (user == null)
            {
                return NotFound();
            }
            return user;
        }

        [HttpGet("{email}")]
        [Route("[action]")]
        public async Task<ActionResult<User>> GetByEmail(string email)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var user = await userService.GetByEmail(email);

            if (user == null)
            {
                return NotFound();
            }
            return user;
        }

        [HttpPost]
        public async Task<ActionResult<User>> Create([FromBody] User user)
        {
            if (!ModelState.IsValid || user == null)
            {
                return BadRequest();
            }

            return await userService.Create(user);
        }
    }
}