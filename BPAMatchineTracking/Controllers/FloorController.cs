using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
//using BPAMachineTrack.Models;
using BPAMatchineTrack.Models;
using Microsoft.AspNetCore.Authorization;

namespace BPAMachineTrack.Controllers
{
    [Authorize]
    public class FloorController : Controller
    {
        private readonly CottonclubContext _context;

        public FloorController(CottonclubContext context)
        {
            _context = context;
        }

        // GET: Floor
        [HttpGet]
        [Authorize(Roles = "Admin,Super Admin")]
        public async Task<IActionResult> Index()
        {
            var cottonclubContext = _context.TblFloorInfos.Include(t => t.Bu).Include(t => t.CidNavigation);
            return View(await cottonclubContext.ToListAsync());
        }

        // GET: Floor/Details/5
        [HttpGet]
        [Authorize(Roles = "Admin,Super Admin")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tblFloorInfo = await _context.TblFloorInfos
                .Include(t => t.Bu)
                .Include(t => t.CidNavigation)
                .FirstOrDefaultAsync(m => m.Fid == id);
            if (tblFloorInfo == null)
            {
                return NotFound();
            }

            return View(tblFloorInfo);
        }

        // GET: Floor/Create
        public IActionResult Create()
        {
            ViewData["Buid"] = new SelectList(_context.TblBuildingInfos, "Buid", "Name"); // Display Building Name
            ViewData["Cid"] = new SelectList(_context.TblCompanyInfos, "Cid", "CompanyName"); // Display Company Name
            return View();
        }

        // POST: Floor/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,Super Admin")]
        public async Task<IActionResult> Create([Bind("Fid,Cid,Buid,Name,Description,Remarks,Status")] TblFloorInfo tblFloorInfo)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tblFloorInfo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Buid"] = new SelectList(_context.TblBuildingInfos, "Buid", "Name", tblFloorInfo.Buid); // Display Building Name
            ViewData["Cid"] = new SelectList(_context.TblCompanyInfos, "Cid", "CompanyName", tblFloorInfo.Cid); // Display Company Name
            return View(tblFloorInfo);
        }

        // GET: Floor/Edit/5
        [HttpGet]
        [Authorize(Roles = "Admin,Super Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tblFloorInfo = await _context.TblFloorInfos.FindAsync(id);
            if (tblFloorInfo == null)
            {
                return NotFound();
            }
            ViewData["Buid"] = new SelectList(_context.TblBuildingInfos, "Buid", "Name", tblFloorInfo.Buid); // Display Building Name
            ViewData["Cid"] = new SelectList(_context.TblCompanyInfos, "Cid", "CompanyName", tblFloorInfo.Cid); // Display Company Name
            return View(tblFloorInfo);
        }

        // POST: Floor/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,Super Admin")]
        public async Task<IActionResult> Edit(int id, [Bind("Fid,Cid,Buid,Name,Description,Remarks,Status")] TblFloorInfo tblFloorInfo)
        {
            if (id != tblFloorInfo.Fid)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tblFloorInfo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TblFloorInfoExists(tblFloorInfo.Fid))
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
            ViewData["Buid"] = new SelectList(_context.TblBuildingInfos, "Buid", "Name", tblFloorInfo.Buid); // Display Building Name
            ViewData["Cid"] = new SelectList(_context.TblCompanyInfos, "Cid", "CompanyName", tblFloorInfo.Cid); // Display Company Name
            return View(tblFloorInfo);
        }

        // GET: Floor/Delete/5
        [HttpGet]
        [Authorize(Roles = "Admin,Super Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tblFloorInfo = await _context.TblFloorInfos
                .Include(t => t.Bu)
                .Include(t => t.CidNavigation)
                .FirstOrDefaultAsync(m => m.Fid == id);
            if (tblFloorInfo == null)
            {
                return NotFound();
            }

            return View(tblFloorInfo);
        }

        // POST: Floor/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,Super Admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tblFloorInfo = await _context.TblFloorInfos.FindAsync(id);
            if (tblFloorInfo != null)
            {
                _context.TblFloorInfos.Remove(tblFloorInfo);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TblFloorInfoExists(int id)
        {
            return _context.TblFloorInfos.Any(e => e.Fid == id);
        }
        public async Task<IActionResult> GetBuildingsByCompany(int companyId)
        {
            var buildings = await _context.TblBuildingInfos
                .Where(b => b.Cid == companyId)
                .Select(b => new { b.Buid, b.Name })
                .ToListAsync();

            return Json(buildings);
        }
    }
}
