using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API_GOPR_SERVICE.Models;
using Microsoft.AspNetCore.Cors;

namespace API_GOPR_SERVICE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminsController : ControllerBase
    {
        private readonly GoprServiceContext _context;

        public AdminsController(GoprServiceContext context)
        {
            _context = context;
        }

        // GET: api/Admins
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Admins>>> GetAdmins()
        {
            return await _context.Admins.ToListAsync();
        }

        // GET: api/Admins/5
        [HttpGet("{phoneNumber}")]
        public async Task<ActionResult<Admins>> GetAdmins(string phoneNumber)
        {
            var admins = await _context.Admins.FirstOrDefaultAsync(user => user.phoneNumber == phoneNumber);

            if (admins == null)
            {
                return NotFound();
            }

            return admins;
        }

        // PUT: api/Admins/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAdmins(int id, Admins admins)
        {
            if (id != admins.id)
            {
                return BadRequest();
            }

            _context.Entry(admins).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AdminsExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Admins
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Admins>> PostAdmins(Admins admins)
        {
            _context.Admins.Add(admins);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAdmins", new { id = admins.id }, admins);
        }

        // DELETE: api/Admins/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAdmins(int id)
        {
            var admins = await _context.Admins.FindAsync(id);
            if (admins == null)
            {
                return NotFound();
            }

            _context.Admins.Remove(admins);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AdminsExists(int id)
        {
            return _context.Admins.Any(e => e.id == id);
        }
    }
}
