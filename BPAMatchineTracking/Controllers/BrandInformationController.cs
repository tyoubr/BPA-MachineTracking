using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BPAMatchineTrack.Models;
using Microsoft.AspNetCore.Authorization;

namespace BPAMatchineTrack.Controllers
{
    [Authorize]
    public class BrandInformationController : Controller
    {
        private readonly CottonclubContext _context;

        public BrandInformationController(CottonclubContext context)
        {
            _context = context;
        }

        // GET: BrandInformation
        [HttpGet]
        [Authorize(Roles = "Admin,Super Admin")]
        public async Task<IActionResult> Index()
        {
            return View(await _context.TblBrandInformation.ToListAsync());
        }

        // GET: BrandInformation/Details/5
        [HttpGet]
        [Authorize(Roles = "Admin,Super Admin")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tblBrandInformation = await _context.TblBrandInformation
                .FirstOrDefaultAsync(m => m.Brid == id);
            if (tblBrandInformation == null)
            {
                return NotFound();
            }

            return View(tblBrandInformation);
        }

        // GET: BrandInformation/Create
        [HttpGet]
        [Authorize(Roles = "Admin,Super Admin")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: BrandInformation/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,Super Admin")]
        public async Task<IActionResult> Create([Bind("Brid,Name,Description,Status")] TblBrandInformation tblBrandInformation)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tblBrandInformation);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tblBrandInformation);
        }

        // GET: BrandInformation/Edit/5
        [HttpGet]
        [Authorize(Roles = "Admin,Super Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tblBrandInformation = await _context.TblBrandInformation.FindAsync(id);
            if (tblBrandInformation == null)
            {
                return NotFound();
            }
            return View(tblBrandInformation);
        }

        // POST: BrandInformation/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,Super Admin")]
        public async Task<IActionResult> Edit(int id, [Bind("Brid,Name,Description,Status")] TblBrandInformation tblBrandInformation)
        {
            if (id != tblBrandInformation.Brid)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tblBrandInformation);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TblBrandInformationExists(tblBrandInformation.Brid))
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
            return View(tblBrandInformation);
        }

        // GET: BrandInformation/Delete/5
        [HttpGet]
        [Authorize(Roles = "Admin,Super Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tblBrandInformation = await _context.TblBrandInformation
                .FirstOrDefaultAsync(m => m.Brid == id);
            if (tblBrandInformation == null)
            {
                return NotFound();
            }

            return View(tblBrandInformation);
        }

        // POST: BrandInformation/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,Super Admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tblBrandInformation = await _context.TblBrandInformation.FindAsync(id);
            if (tblBrandInformation != null)
            {
                _context.TblBrandInformation.Remove(tblBrandInformation);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TblBrandInformationExists(int id)
        {
            return _context.TblBrandInformation.Any(e => e.Brid == id);
        }
    }
}
