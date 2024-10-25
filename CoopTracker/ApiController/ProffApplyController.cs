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
    public class ProffApplyController : ControllerBase
    {
        private readonly CoopTrackerDbContext _context;

        public ProffApplyController(CoopTrackerDbContext context)
        {
            _context = context;
        }

        // GET: api/ProffApply
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProffApply>>> GetProffApplys()
        {
            return await _context.ProffApplys.ToListAsync();
        }

        // GET: api/ProffApply/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ProffApply>> GetProffApply(int id)
        {
            var proffApply = await _context.ProffApplys.FindAsync(id);

            if (proffApply == null)
            {
                return NotFound();
            }

            return proffApply;
        }

        // PUT: api/ProffApply/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProffApply(int id, ProffApply proffApply)
        {
            if (id != proffApply.ProffApplyId)
            {
                return BadRequest();
            }

            _context.Entry(proffApply).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProffApplyExists(id))
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

        // POST: api/ProffApply
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ProffApply>> PostProffApply(ProffApply proffApply)
        {
            _context.ProffApplys.Add(proffApply);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProffApply", new { id = proffApply.ProffApplyId }, proffApply);
        }

        // DELETE: api/ProffApply/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProffApply(int id)
        {
            var proffApply = await _context.ProffApplys.FindAsync(id);
            if (proffApply == null)
            {
                return NotFound();
            }

            _context.ProffApplys.Remove(proffApply);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProffApplyExists(int id)
        {
            return _context.ProffApplys.Any(e => e.ProffApplyId == id);
        }
    }
}
