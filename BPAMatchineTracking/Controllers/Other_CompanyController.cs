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
    public class Other_CompanyController : Controller
    {
        private readonly CottonclubContext _context;

        public Other_CompanyController(CottonclubContext context)
        {
            _context = context;
        }

        // GET: Other_Company
        public async Task<IActionResult> Index()
        {
            return View(await _context.tbl_Other_Companies.ToListAsync());
        }

        // GET: Other_Company/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tbl_Other_Company = await _context.tbl_Other_Companies
                .FirstOrDefaultAsync(m => m.OCID == id);
            if (tbl_Other_Company == null)
            {
                return NotFound();
            }

            return View(tbl_Other_Company);
        }

        // GET: Other_Company/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Other_Company/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("OCID,OC_NAME,ADDRESS,CONTRACT_PERSON,REMARKS")] tbl_Other_Company tbl_Other_Company)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tbl_Other_Company);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tbl_Other_Company);
        }

        // GET: Other_Company/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tbl_Other_Company = await _context.tbl_Other_Companies.FindAsync(id);
            if (tbl_Other_Company == null)
            {
                return NotFound();
            }
            return View(tbl_Other_Company);
        }

        // POST: Other_Company/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("OCID,OC_NAME,ADDRESS,CONTRACT_PERSON,REMARKS")] tbl_Other_Company tbl_Other_Company)
        {
            if (id != tbl_Other_Company.OCID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tbl_Other_Company);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!tbl_Other_CompanyExists(tbl_Other_Company.OCID))
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
            return View(tbl_Other_Company);
        }

        // GET: Other_Company/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tbl_Other_Company = await _context.tbl_Other_Companies
                .FirstOrDefaultAsync(m => m.OCID == id);
            if (tbl_Other_Company == null)
            {
                return NotFound();
            }

            return View(tbl_Other_Company);
        }

        // POST: Other_Company/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tbl_Other_Company = await _context.tbl_Other_Companies.FindAsync(id);
            if (tbl_Other_Company != null)
            {
                _context.tbl_Other_Companies.Remove(tbl_Other_Company);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool tbl_Other_CompanyExists(int id)
        {
            return _context.tbl_Other_Companies.Any(e => e.OCID == id);
        }
    }
}
