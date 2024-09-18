using CoopTracker.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CoopTracker.Controllers
{
    public class GroupKeyMasterController : ControllerBase42
    {
        private readonly CoopTrackerDbContext _context;

        public GroupKeyMasterController(CoopTrackerDbContext context)
        {
            _context = context;

        }
        // GET: GroupKeyMaster
        public async Task<IActionResult> Select(int id)
        {
            var student = await _context.GroupKeyMasters
                .FirstOrDefaultAsync(m => m.GroupKeyMasterId == id);
            if (student == null)
            {
                return NotFound();
            }
            groupHashGroup = student.HashGroup;
            groupkey = id;

            return RedirectToAction("Index", "Home");
        }


        // GET: GroupKeyMaster/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var groupKeyMaster = await _context.GroupKeyMasters
                .FirstOrDefaultAsync(m => m.GroupKeyMasterId == id);
            if (groupKeyMaster == null)
            {
                return NotFound();
            }

            return View(groupKeyMaster);
        }

        // GET: GroupKeyMaster/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: GroupKeyMaster/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("GroupKeyMasterId,HashGroup")] GroupKeyMaster groupKeyMaster)
        {
            if (ModelState.IsValid)
            {
                _context.Add(groupKeyMaster);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Home");
            }
            return View(groupKeyMaster);
        }

        // GET: GroupKeyMaster/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var groupKeyMaster = await _context.GroupKeyMasters.FindAsync(id);
            if (groupKeyMaster == null)
            {
                return NotFound();
            }
            return View(groupKeyMaster);
        }

        // POST: GroupKeyMaster/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("GroupKeyMasterId,HashGroup")] GroupKeyMaster groupKeyMaster)
        {
            if (id != groupKeyMaster.GroupKeyMasterId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(groupKeyMaster);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GroupKeyMasterExists(groupKeyMaster.GroupKeyMasterId))
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
            return View(groupKeyMaster);
        }




        // GET: GroupKeyMaster/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var groupKeyMaster = await _context.GroupKeyMasters
                .FirstOrDefaultAsync(m => m.GroupKeyMasterId == id);
            if (groupKeyMaster == null)
            {
                return NotFound();
            }

            return View(groupKeyMaster);
        }

        // POST: GroupKeyMaster/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var groupKeyMaster = await _context.GroupKeyMasters.FindAsync(id);
            if (groupKeyMaster != null)
            {
                _context.GroupKeyMasters.Remove(groupKeyMaster);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "Home");
        }

        private bool GroupKeyMasterExists(int id)
        {
            return _context.GroupKeyMasters.Any(e => e.GroupKeyMasterId == id);
        }
    }
}
