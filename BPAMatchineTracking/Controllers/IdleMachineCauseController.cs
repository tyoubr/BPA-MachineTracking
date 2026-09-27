using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BPAMatchineTrack.Models;

namespace BPAMatchineTrack.Controllers
{
    public class IdleMachineCauseController : Controller
    {
        private readonly CottonclubContext _context;

        public IdleMachineCauseController(CottonclubContext context)
        {
            _context = context;
        }

        // GET: Company
        public async Task<IActionResult> Index(string searchTerm)
        {
            ViewData["searchTerm"] = searchTerm;

            var causes = from c in _context.TblMcIdleCause
                            select c;

            if (!string.IsNullOrEmpty(searchTerm))
            {
                causes = causes.Where(c => c.CauseName.Contains(searchTerm) ||
                                           c.Status.Contains(searchTerm) ||
                                           c.Remarks.Contains(searchTerm));
            }

            return View(await causes.ToListAsync());
        }


        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TblMcIdleCause idleCause)
        {
            if (ModelState.IsValid)
            {
                _context.Add(idleCause);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(idleCause);
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cause = await _context.TblMcIdleCause.FindAsync(id);
            if (cause == null)
            {
                return NotFound();
            }
            return View(cause);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id,TblMcIdleCause idleCause)
        {
            if (id != idleCause.Icid)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(idleCause);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CauseExists(idleCause.Icid))
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
            return View(idleCause);
        }

        private bool CauseExists(int id)
        {
            return _context.TblMcIdleCause.Any(e => e.Icid == id);
        }


        // GET: Company/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cause = await _context.TblMcIdleCause
                .FirstOrDefaultAsync(m => m.Icid == id);
            if (cause == null)
            {
                return NotFound();
            }

            return View(cause);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var cause = await _context.TblMcIdleCause.FindAsync(id);
            if (cause != null)
            {
                _context.TblMcIdleCause.Remove(cause);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
