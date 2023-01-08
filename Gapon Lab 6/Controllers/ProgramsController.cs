using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Lab6.Models;
using Lab_6.Models;

namespace Lab_6.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProgramsController : ControllerBase
    {
        private readonly TvChannelContext _context;

        public ProgramsController(TvChannelContext context)
        {
            _context = context;
        }

        // GET: api/Programs
        [HttpGet]
        public IEnumerable<ProgramView> GetPrograms()
        {
            var programs = _context.TvPrograms.Include(i => i.Genre).ToList();

            var result = programs.Select(i => new ProgramView
            {
                Id = i.Id,
                Name = i.Name,
                Length = i.Length,
                Rating = i.Rating,
                GenreName = i.Genre.Name,
                Description = i.Description
            });

            return result;
        }

        // GET: api/Programs/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Lab6.Models.Program>> GetProgram(int id)
        {
            var program = await _context.TvPrograms.FindAsync(id);

            if (program == null)
            {
                return NotFound();
            }

            return program;
        }

        // PUT: api/Programs/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProgram(int id, Lab6.Models.Program program)
        {
            if (id != program.Id)
            {
                return BadRequest();
            }

            _context.Entry(program).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProgramExists(id))
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

        // POST: api/Programs
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Lab6.Models.Program>> PostProgram(Lab6.Models.Program program)
        {
            _context.TvPrograms.Add(program);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (ProgramExists(program.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetProgram", new { id = program.Id }, program);
        }

        // DELETE: api/Programs/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProgram(int id)
        {
            var program = await _context.TvPrograms.FindAsync(id);
            if (program == null)
            {
                return NotFound();
            }

            _context.TvPrograms.Remove(program);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpGet("genres")]
        public List<Genre> GetGenres()
        {
            return _context.Genres.ToList();
        }

        private bool ProgramExists(int id)
        {
            return _context.TvPrograms.Any(e => e.Id == id);
        }
    }
}
