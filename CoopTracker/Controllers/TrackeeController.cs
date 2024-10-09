using System.Linq;
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
            return View(await _context.Trackees.Include(e => e.ProffApply).Where(e => e.TrackerId == this.trackerId).Include(e => e.Student).Include(e => e.Tracker).ToListAsync());
        }
        // Action to return the partial view based on the input value
        [HttpGet]
        public async Task<IActionResult> GetPartialView(string fieldValue)
        {
            // Some logic to get data based on 'fieldValue'
            var data = "You selected: " + fieldValue;

            var trakke = _context.Trackees
                .Include(e => e.Tracker)
                .Select(e => new { e.TrackeeId, e.UrlLink, e.CompanyName, e.JobTitle, Tracker = e.Tracker.Description })
                .ToList()
                .Select(e => new SimilatiryURLPartialViewModel { Tracker = e.Tracker, TrackeeId = e.TrackeeId, similatiry = CalculateSimilarity(e.UrlLink, fieldValue), CompanyName = e.CompanyName, JobTitle = e.JobTitle, UrlLink = e.UrlLink })
                .Where(e => e.similatiry > 80);

            var yyy = trakke.OrderByDescending(e => e.similatiry).Select(e => $"Similatiry:{e.similatiry}% <a href='/Trackee/Edit/{e.TrackeeId}'> {e.Tracker} {e.JobTitle}</a>");
            return PartialView("_SimilatiryURLPartialView", trakke); // Return partial view with data
        }

       

        public double CalculateSimilarity(string url1, string url2)
        {
            url1 = url1.Replace("https", "").Replace("http", "").Replace(":", "").Replace("/", "");
            url2 = url2.Replace("https", "").Replace("http", "").Replace(":", "").Replace("/", "");
            int levenshteinDistance = LevenshteinDistance(url1, url2);
            int maxLength = Math.Max(url1.Length, url2.Length);

            // Similarity percentage
            return Math.Round((1.0 - (double)levenshteinDistance / maxLength) * 100);
        }

        private int LevenshteinDistance(string s1, string s2)
        {
            int n = s1.Length;
            int m = s2.Length;
            int[,] d = new int[n + 1, m + 1];

            if (n == 0) return m;
            if (m == 0) return n;

            for (int i = 0; i <= n; i++) d[i, 0] = i;
            for (int j = 0; j <= m; j++) d[0, j] = j;

            for (int i = 1; i <= n; i++)
            {
                for (int j = 1; j <= m; j++)
                {
                    int cost = (s2[j - 1] == s1[i - 1]) ? 0 : 1;
                    d[i, j] = Math.Min(
                        Math.Min(d[i - 1, j] + 1, d[i, j - 1] + 1),
                        d[i - 1, j - 1] + cost);
                }
            }
            return d[n, m];
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
                UserSelectedTrackerTrakeeCount = currentTracker.Trackee.Count();
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

            var trackee = _context.Trackees.Include(e => e.Student).Include(e => e.Tracker).FirstOrDefault(e => e.TrackeeId == id);
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
            UserSelectedTrackerTrakeeCount = currentTracker.Trackee.Count();
            return RedirectToAction(nameof(Index));
        }

        private bool TrackeeExists(int id)
        {
            return _context.Trackees.Any(e => e.TrackeeId == id);
        }
    }
}
