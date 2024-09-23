using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CoopTracker.Controllers
{
    public class TrackeeController : ControllerBase42
    {
        private readonly CoopTrackerDbContext _context;

        public TrackeeController(CoopTrackerDbContext context)
        {
            _context = context;
        }

        // GET: Trackee
        public async Task<IActionResult> Index(int? trakerId)
        {
            return View(await _context.Trackees.Include(e=>e.ProffApply).Where(e=>e.TrackerId== this.trackerId).Include(e=>e.Student).Include(e=>e.Tracker).ToListAsync());
        }

       

        // GET: Trackee/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Trackee/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TrackeeId,TenantId,StudentId,TrackerId,CompanyName,CompanyCity,JobTitle,DateAppliation,DocumentProvided,LastUpdate,UrlLink")] Trackee trackee)
        {
            if (ModelState.IsValid)
            {
                _context.Add(trackee);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(trackee);
        }

        // GET: Trackee/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var trackee =  _context.Trackees.Include(e=>e.Student).Include(e=>e.Tracker).FirstOrDefault(e=>e.TrackeeId==id) ;
            if (trackee == null)
            {
                return NotFound();
            }
            return View(trackee);
        }

        // POST: Trackee/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TrackeeId,TenantId,StudentId,TrackerId,CompanyName,CompanyCity,JobTitle,DateAppliation,DocumentProvided,LastUpdate,UrlLink")] Trackee trackee)
        {
            if (id != trackee.TrackeeId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(trackee);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TrackeeExists(trackee.TrackeeId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(trackee);
        }

        // GET: Trackee/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var trackee = await _context.Trackees
                .FirstOrDefaultAsync(m => m.TrackeeId == id);
            if (trackee == null)
            {
                return NotFound();
            }

            return View(trackee);
        }

        // POST: Trackee/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var trackee = await _context.Trackees.FindAsync(id);
            if (trackee != null)
            {
                _context.Trackees.Remove(trackee);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TrackeeExists(int id)
        {
            return _context.Trackees.Any(e => e.TrackeeId == id);
        }
    }
}
