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
    public class CompanyController : Controller
    {
        private readonly CottonclubContext _context;

        public CompanyController(CottonclubContext context)
        {
            _context = context;
        }

        // GET: Company
        public async Task<IActionResult> Index(string searchTerm)
        {
            ViewData["searchTerm"] = searchTerm;

            var companies = from c in _context.TblCompanyInfos
                            select c;

            if (!string.IsNullOrEmpty(searchTerm))
            {
                companies = companies.Where(c => c.CompanyName.Contains(searchTerm) ||
                                                 c.ShortName.Contains(searchTerm) ||
                                                 c.Status.Contains(searchTerm) ||
                                                 c.Address.Contains(searchTerm));
            }

            return View(await companies.ToListAsync());
        }


        // GET: Company/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tblCompanyInfo = await _context.TblCompanyInfos
                .FirstOrDefaultAsync(m => m.Cid == id);
            if (tblCompanyInfo == null)
            {
                return NotFound();
            }

            return View(tblCompanyInfo);
        }

        // GET: Company/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Company/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Cid,CompanyName,ShortName,Address,Phone,Email,Remarks,Opt1,Opt2,Opt3,Opt4,Opt5,Status")] TblCompanyInfo tblCompanyInfo)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tblCompanyInfo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tblCompanyInfo);
        }

        // GET: Company/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var company = await _context.TblCompanyInfos.FindAsync(id);
            if (company == null)
            {
                return NotFound();
            }
            return View(company);
        }

        // POST: Company/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Cid,CompanyName,ShortName,Address,Phone,Email,Status,Remarks")] TblCompanyInfo tblCompanyInfo)
        {
            if (id != tblCompanyInfo.Cid)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tblCompanyInfo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CompanyExists(tblCompanyInfo.Cid))
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
            return View(tblCompanyInfo);
        }

        private bool CompanyExists(int id)
        {
            return _context.TblCompanyInfos.Any(e => e.Cid == id);
        }


        // GET: Company/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tblCompanyInfo = await _context.TblCompanyInfos
                .FirstOrDefaultAsync(m => m.Cid == id);
            if (tblCompanyInfo == null)
            {
                return NotFound();
            }

            return View(tblCompanyInfo);
        }

        // POST: Company/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tblCompanyInfo = await _context.TblCompanyInfos.FindAsync(id);
            if (tblCompanyInfo != null)
            {
                _context.TblCompanyInfos.Remove(tblCompanyInfo);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TblCompanyInfoExists(int id)
        {
            return _context.TblCompanyInfos.Any(e => e.Cid == id);
        }
       

    }
}
