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
            var tracker = await _context.Trackers.Include(e=>e.Trackee)
                .FirstOrDefaultAsync(m => m.TrackerId == id);
            if (tracker == null)
            {
                return NotFound();
            }
            trackerDescription= tracker.Description;
            trackerId= id;
            IsUserSelectedTracker = true;
            UserSelectedTrackerTrakeeCount = tracker.Trackee.Count();
            return RedirectToAction("Index", "Home");
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
        public async Task<IActionResult> Create([Bind("TrackerId,TenantId,Start,End,Submit,Description")] Tracker tracker)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tracker);
                await _context.SaveChangesAsync();
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
