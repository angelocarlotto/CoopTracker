using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CoopTracker;
using NuGet.Versioning;

namespace CoopTracker.ApiController
{
    [Route("api/[controller]")]
    [ApiController]
    public class TrackerController : ControllerBase
    {
        private readonly CoopTrackerDbContext _context;

        public TrackerController(CoopTrackerDbContext context)
        {
            _context = context;
        }

        // GET: api/Tracker
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Tracker>>> GetTrackers()
        {
            var trackers = await _context.Trackers
                                          .Include(e => e.Trackee)
                                          .OrderBy(e => e.Submit)
                                          .ToListAsync();

            if (trackers == null || trackers.Count == 0)
            {
                return NotFound("No trackers found.");
            }

            return Ok(trackers);
        }

        // GET: api/Tracker/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Tracker>> GetTracker(int id)
        {
            var tracker = await _context.Trackers.FindAsync(id);

            var trackers = await _context.Trackers
                                          .Include(e => e.Trackee)
                                          .OrderBy(e => e.Submit)
                                          .FirstAsync(e=>e.TrackerId==id);

            if (tracker == null)
            {
                return NotFound();
            }

            return tracker;
        }

 // GET: api/Tracker/5/trackees
        [HttpGet("{id}/trackees")]
        public async Task<ActionResult<ICollection< Trackee>>> GetTrackees(int id)
        {
            //var tracker = await _context.Trackers.FindAsync(id);

            var trackers = await  _context.Trackees.Where(e=>e.TrackerId==id).ToListAsync();

            if (trackers == null)
            {
                return NotFound();
            }

            return trackers;
        }

        // PUT: api/Tracker/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTracker(int id, Tracker tracker)
        {
            if (id != tracker.TrackerId)
            {
                return BadRequest();
            }

            _context.Entry(tracker).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TrackerExists(id))
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

        // POST: api/Tracker
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Tracker>> PostTracker(Tracker tracker)
        {
            _context.Trackers.Add(tracker);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTracker", new { id = tracker.TrackerId }, tracker);
        }

        // DELETE: api/Tracker/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTracker(int id)
        {
            var tracker = await _context.Trackers.FindAsync(id);
            if (tracker == null)
            {
                return NotFound();
            }

            _context.Trackers.Remove(tracker);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TrackerExists(int id)
        {
            return _context.Trackers.Any(e => e.TrackerId == id);
        }
    }
}
