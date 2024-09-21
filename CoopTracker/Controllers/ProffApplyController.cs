using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CoopTracker;
using CoopTracker.Models;
using NuGet.DependencyResolver;

namespace CoopTracker.Controllers;

public class ProffApplyController : Controller
{
    private readonly CoopTrackerDbContext _context;

    public ProffApplyController(CoopTrackerDbContext context)
    {
        _context = context;

    }

    // GET: ProffApply
    public async Task<IActionResult> Index(int? trackerId)
    {
        var modelView = new ProffApplyModelIndex() { TrackeeId=trackerId};
        modelView.ProffApplyModelUpdateList = await _context.ProffApplys.Where(e => e.TrackeeId == trackerId).Select(e => e.ToUpdateViewModel()).ToListAsync();
        return View(modelView);
    }

    // GET: ProffApply/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var proffApply = await _context.ProffApplys
            .FirstOrDefaultAsync(m => m.ProffApplyId == id);
        if (proffApply == null)
        {
            return NotFound();
        }

        return View(proffApply.ToUpdateViewModel());
    }

    // GET: ProffApply/Create
    public IActionResult Create(int trackerId)
    {
        return View(new ProffApplyModelCreate() { TrackeeId = trackerId });
    }

    // POST: ProffApply/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("ProffApplyId,TenantId,TrackeeId,Image,Description")] ProffApplyModelCreate proffApply)
    {
        if (ModelState.IsValid)
        {
            var file = proffApply.ToCreateEntity();
            _context.ProffApplys.Add(file);
            await _context.SaveChangesAsync();
            var viewmodel = file.ToCreateViewModel();
            return RedirectToAction("Index", "ProffApply");
        }
        return View(proffApply);
    }


    // GET: ProffApply/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var proffApply = await _context.ProffApplys.FindAsync(id);
        if (proffApply == null)
        {
            return NotFound();
        }
        return View(proffApply.ToUpdateViewModel());
    }

    // POST: ProffApply/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("ProffApplyId,TenantId,Image,Description")] ProffApplyModelUpdate proffApply)
    {
        if (id != proffApply.ProffApplyId)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(proffApply);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProffApplyExists(proffApply.ProffApplyId))
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
        return View(proffApply);
    }

    // GET: ProffApply/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var proffApply = await _context.ProffApplys
            .FirstOrDefaultAsync(m => m.ProffApplyId == id);
        if (proffApply == null)
        {
            return NotFound();
        }

        return View(proffApply.ToUpdateViewModel());
    }

    // POST: ProffApply/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var proffApply = await _context.ProffApplys.FindAsync(id);
        if (proffApply != null)
        {
            _context.ProffApplys.Remove(proffApply);
        }

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool ProffApplyExists(int id)
    {
        return _context.ProffApplys.Any(e => e.ProffApplyId == id);
    }

    public async Task<IActionResult> Upload(ProffApplyModelCreate viewModel)
    {
        var file = viewModel.ToCreateEntity();
        _context.ProffApplys.Add(file);
        await _context.SaveChangesAsync();
        var viewmodel = file.ToCreateViewModel();
        return View(viewmodel);
    }


    public FileResult FileEndpoint(int Id)
    {
        var content = _context.ProffApplys.FirstOrDefault(x => x.ProffApplyId == Id).Image;
        return File(content, "image/png");
    }
}


