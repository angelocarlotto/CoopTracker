using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace CoopTracker.Controllers
{
    public class TrackerController : ControllerBase42
    {
        private readonly CoopTrackerDbContext _context;

        public TrackerController(CoopTrackerDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Select(int id)
        {
            var student = await _context.Trackers
                .FirstOrDefaultAsync(m => m.TrackerId == id);
            if (student == null)
            {
                return NotFound();
            }
            trackerDescription= student.Description;
            trackerId= id;
            return RedirectToAction("Index", "Home");
        }



        // GET: Tracker/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tracker = await _context.Trackers
                .FirstOrDefaultAsync(m => m.TrackerId == id);
            if (tracker == null)
            {
                return NotFound();
            }

            return View(tracker);
        }

        // GET: Tracker/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Tracker/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TrackerId,GroupKeyMasterId,Start,End,Submit,Description")] Tracker tracker)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tracker);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Home");

            }
            return View(tracker);
        }

        // GET: Tracker/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tracker = await _context.Trackers.FindAsync(id);
            if (tracker == null)
            {
                return NotFound();
            }
            return View(tracker);
        }

        // POST: Tracker/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TrackerId,Start,End,Submit,Description")] Tracker tracker)
        {
            if (id != tracker.TrackerId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tracker);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TrackerExists(tracker.TrackerId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index", "Home");
            }
            return View(tracker);
        }

        // GET: Tracker/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tracker = await _context.Trackers
                .FirstOrDefaultAsync(m => m.TrackerId == id);
            if (tracker == null)
            {
                return NotFound();
            }

            return View(tracker);
        }

        // POST: Tracker/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tracker = await _context.Trackers.FindAsync(id);
            if (tracker != null)
            {
                _context.Trackers.Remove(tracker);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "Home");
        }

        private bool TrackerExists(int id)
        {
            return _context.Trackers.Any(e => e.TrackerId == id);
        }
    }
}