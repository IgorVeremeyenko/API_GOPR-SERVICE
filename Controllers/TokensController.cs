using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API_GOPR_SERVICE.Models;
using Microsoft.AspNetCore.Cors;

namespace API_GOPR_SERVICE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokensController : ControllerBase
    {
        private readonly GoprServiceContext _context;

        public TokensController(GoprServiceContext context)
        {
            _context = context;
        }

        // GET: api/Tokens
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Tokens>>> GetTokens()
        {
            return await _context.Tokens.ToListAsync();
        }

        // GET: api/Tokens/5
        [HttpGet("{phoneNumber}")]
        public async Task<ActionResult<Tokens>> GetTokens(string phoneNumber)
        {
            var tokens = await _context.Tokens.FirstOrDefaultAsync(p => p.phoneNumber == phoneNumber);

            if (tokens == null)
            {
                return NotFound();
            }

            return tokens;
        }

        // PUT: api/Tokens/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTokens(int id, Tokens tokens)
        {
            if (id != tokens.id)
            {
                return BadRequest();
            }

            _context.Entry(tokens).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TokensExists(id))
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

        // POST: api/Tokens
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        
        public async Task<ActionResult<Tokens>> PostTokens(Tokens tokens)
        {
            _context.Tokens.Add(tokens);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTokens", new { id = tokens.id }, tokens);
        }

        // DELETE: api/Tokens/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTokens(int id)
        {
            var tokens = await _context.Tokens.FindAsync(id);
            if (tokens == null)
            {
                return NotFound();
            }

            _context.Tokens.Remove(tokens);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TokensExists(int id)
        {
            return _context.Tokens.Any(e => e.id == id);
        }
    }
}
