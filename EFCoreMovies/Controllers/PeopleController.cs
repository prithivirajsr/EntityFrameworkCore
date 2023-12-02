using EFCoreMovies.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EFCoreMovies.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PeopleController : ControllerBase
    {
        private ApplicationDbContext _context;
        public PeopleController(ApplicationDbContext context) 
        {
            _context = context;
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<ActionResult<Person>> Get(int id)
        {
            var person = await _context.People
                        .Include(p => p.SentMessages)
                        .Include(p => p.ReceivedMessages).AsNoTracking()
                        .FirstOrDefaultAsync(p => p.Id.Equals(id));  

            if(person is null)
            {
                return NotFound();
            }

            return Ok(person);
        }
    }
}
