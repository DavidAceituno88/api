using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TodoApi.Models;

namespace TodoApi.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UserController : ControllerBase
    {
        private readonly TodoContext context;

        public UserController(TodoContext context)
        {
            this.context = context;
        }
        [HttpGet]
        public async Task<ActionResult<List<User>>> Get()
        {
            return await context.Users.Include(x => x.TodoItems).ToListAsync();
        }


        [HttpPost]
        public async Task<ActionResult> Post(User user)
        {
            context.Add(user);
            await context.SaveChangesAsync();
            return Ok();
        }
    }
}
