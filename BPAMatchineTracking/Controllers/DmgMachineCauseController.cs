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
    public class DmgMachineCauseController : Controller
    {
        private readonly CottonclubContext _context;

        public DmgMachineCauseController(CottonclubContext context)
        {
            _context = context;
        }

        // GET: Company
        public async Task<IActionResult> Index(string searchTerm)
        {
            ViewData["searchTerm"] = searchTerm;

            var causes = from c in _context.TblMcDamageCause
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
        public async Task<IActionResult> Create(TblMcDamageCause cause)
        {
            if (ModelState.IsValid)
            {
                _context.Add(cause);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(cause);
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cause = await _context.TblMcDamageCause.FindAsync(id);
            if (cause == null)
            {
                return NotFound();
            }
            return View(cause);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, TblMcDamageCause cause)
        {
            if (id != cause.Dcid)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cause);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CauseExists(cause.Dcid))
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
            return View(cause);
        }

        private bool CauseExists(int id)
        {
            return _context.TblMcDamageCause.Any(e => e.Dcid == id);
        }


        // GET: Company/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cause = await _context.TblMcDamageCause
                .FirstOrDefaultAsync(m => m.Dcid == id);
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
            var cause = await _context.TblMcDamageCause.FindAsync(id);
            if (cause != null)
            {
                _context.TblMcDamageCause.Remove(cause);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

    }
}

