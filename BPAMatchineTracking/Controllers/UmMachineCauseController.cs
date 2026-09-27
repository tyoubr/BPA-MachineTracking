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
    public class UmMachineCauseController : Controller
    {
        private readonly CottonclubContext _context;

        public UmMachineCauseController(CottonclubContext context)
        {
            _context = context;
        }

        // GET: Company
        public async Task<IActionResult> Index(string searchTerm)
        {
            ViewData["searchTerm"] = searchTerm;

            var causes = from c in _context.TblMcUmCause
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
        public async Task<IActionResult> Create(TblMcUmCause cause)
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

            var cause = await _context.TblMcUmCause.FindAsync(id);
            if (cause == null)
            {
                return NotFound();
            }
            return View(cause);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, TblMcUmCause cause)
        {
            if (id != cause.Umcid)
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
                    if (!CauseExists(cause.Umcid))
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
            return _context.TblMcUmCause.Any(e => e.Umcid == id);
        }


        // GET: Company/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cause = await _context.TblMcUmCause
                .FirstOrDefaultAsync(m => m.Umcid == id);
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
            var cause = await _context.TblMcUmCause.FindAsync(id);
            if (cause != null)
            {
                _context.TblMcUmCause.Remove(cause);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

    }
}
