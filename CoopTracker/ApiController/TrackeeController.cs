using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CoopTracker;

namespace CoopTracker.ApiController
{
    [Route("api/[controller]")]
    [ApiController]
    public class TrackeeController : ControllerBase
    {
        private readonly CoopTrackerDbContext _context;

        public TrackeeController(CoopTrackerDbContext context)
        {
            _context = context;
        }

        // GET: api/Trackee
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Trackee>>> GetTrackees()
        {
            return await _context.Trackees.ToListAsync();
        }

        // GET: api/Trackee/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Trackee>> GetTrackee(int id)
        {
            var trackee = await _context.Trackees.FindAsync(id);

            if (trackee == null)
            {
                return NotFound();
            }

            return trackee;
        }

        // PUT: api/Trackee/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTrackee(int id, Trackee trackee)
        {
            if (id != trackee.TrackeeId)
            {
                return BadRequest();
            }

            _context.Entry(trackee).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TrackeeExists(id))
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

        // POST: api/Trackee
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Trackee>> PostTrackee(Trackee trackee)
        {
            _context.Trackees.Add(trackee);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTrackee", new { id = trackee.TrackeeId }, trackee);
        }

        // DELETE: api/Trackee/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTrackee(int id)
        {
            var trackee = await _context.Trackees.FindAsync(id);
            if (trackee == null)
            {
                return NotFound();
            }

            _context.Trackees.Remove(trackee);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TrackeeExists(int id)
        {
            return _context.Trackees.Any(e => e.TrackeeId == id);
        }
    }
}
