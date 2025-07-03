using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductCategoryAPI.Data;

namespace ProductCategoryAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly AppDbContext _context;
        public UserController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("users")]
        public async Task<IActionResult> GetUsers([FromQuery] int page = 1, [FromQuery] int limit = 50)
        {
            if (page < 1) page = 1;
            if (limit < 1) limit = 50;

            var users = await _context.Users
                .Skip((page - 1) * limit)
                .Take(limit)
                .ToListAsync();

            return Ok(users);
        }
        [HttpGet("users/count")]
        public async Task<IActionResult> GetUsersCount()
        {
            var count = await _context.Users.CountAsync();
            return Ok(new { count });
        }

    }
}