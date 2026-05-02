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
    public class McLocationsController : Controller
    {
        private readonly CottonclubContext _context;

        public McLocationsController(CottonclubContext context)
        {
            _context = context;
        }

        // GET: McLocations
        [HttpGet]
        [Authorize(Roles = "Admin,Super Admin")]
        public async Task<IActionResult> Index()
        {
            var cottonclubContext = _context.TblMcLocations.Include(t => t.Bu).Include(t => t.CidNavigation).Include(t => t.FidNavigation);
            return View(await cottonclubContext.ToListAsync());
        }

        // GET: McLocations/Details/5
        [HttpGet]
        [Authorize(Roles = "Admin,Super Admin")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tblMcLocation = await _context.TblMcLocations
                .Include(t => t.Bu)
                .Include(t => t.CidNavigation)
                .Include(t => t.FidNavigation)
                .FirstOrDefaultAsync(m => m.Lid == id);
            if (tblMcLocation == null)
            {
                return NotFound();
            }

            return View(tblMcLocation);
        }

        // GET: McLocations/Create
        [HttpGet]
        [Authorize(Roles = "Admin,Super Admin")]
        public IActionResult Create()
        {
            ViewBag.Cid = new SelectList(_context.TblCompanyInfos, "Cid", "CompanyName");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,Super Admin")]
        public async Task<IActionResult> Create([Bind("Lid,Cid,Buid,Fid,Name,Remarks,Opt1,Opt2,Opt3,Status")] TblMcLocation tblMcLocation)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tblMcLocation);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Buid"] = new SelectList(_context.TblBuildingInfos, "Buid", "Name", tblMcLocation.Buid);
            ViewData["Cid"] = new SelectList(_context.TblCompanyInfos, "Cid", "CompanyName", tblMcLocation.Cid);
            ViewData["Fid"] = new SelectList(_context.TblFloorInfos, "Fid", "Name", tblMcLocation.Fid);
            return View(tblMcLocation);
        }


        // GET: McLocations/Edit/5
        [HttpGet]
        [Authorize(Roles = "Admin,Super Admin")]
        public async Task<IActionResult> Edit(int id)
        {
            var mcLocation = await _context.TblMcLocations.FindAsync(id);
            if (mcLocation == null)
            {
                return NotFound();
            }

            ViewBag.Cid = new SelectList(_context.TblCompanyInfos, "Cid", "CompanyName", mcLocation.Cid);
            ViewBag.Buid = new SelectList(_context.TblBuildingInfos.Where(b => b.Cid == mcLocation.Cid), "Buid", "Name", mcLocation.Buid);
            ViewBag.Fid = new SelectList(_context.TblFloorInfos.Where(f => f.Buid == mcLocation.Buid), "Fid", "Name", mcLocation.Fid);

            return View(mcLocation);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,Super Admin")]
        public async Task<IActionResult> Edit(int id, [Bind("Lid,Cid,Buid,Fid,Name,Status,Remarks")] TblMcLocation mcLocation)
        {
            if (id != mcLocation.Lid)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(mcLocation);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!McLocationExists(mcLocation.Lid))
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
            ViewBag.Cid = new SelectList(_context.TblCompanyInfos, "Cid", "CompanyName", mcLocation.Cid);
            ViewBag.Buid = new SelectList(_context.TblBuildingInfos.Where(b => b.Cid == mcLocation.Cid), "Buid", "Name", mcLocation.Buid);
            ViewBag.Fid = new SelectList(_context.TblFloorInfos.Where(f => f.Buid == mcLocation.Buid), "Fid", "Name", mcLocation.Fid);
            return View(mcLocation);
        }

        private bool McLocationExists(int id)
        {
            return _context.TblMcLocations.Any(e => e.Lid == id);
        }



        // GET: McLocations/Delete/5
        [HttpGet]
        [Authorize(Roles = "Admin,Super Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tblMcLocation = await _context.TblMcLocations
                .Include(t => t.Bu)
                .Include(t => t.CidNavigation)
                .Include(t => t.FidNavigation)
                .FirstOrDefaultAsync(m => m.Lid == id);
            if (tblMcLocation == null)
            {
                return NotFound();
            }

            return View(tblMcLocation);
        }

        // POST: McLocations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,Super Admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tblMcLocation = await _context.TblMcLocations.FindAsync(id);
            if (tblMcLocation != null)
            {
                _context.TblMcLocations.Remove(tblMcLocation);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TblMcLocationExists(int id)
        {
            return _context.TblMcLocations.Any(e => e.Lid == id);
        }
        //public async Task<IActionResult> GetBuildingsByCompany(int companyId)
        //{
        //    var buildings = await _context.TblBuildingInfos
        //        .Where(b => b.Cid == companyId)
        //        .Select(b => new { b.Buid, b.Name })
        //        .ToListAsync();

        //    return Json(buildings);
        //}
        public async Task<IActionResult> GetBuildingsByCompany(int companyId)
        {
            var buildings = await _context.TblBuildingInfos
                                          .Where(b => b.Cid == companyId)
                                          .Select(b => new { Buid = b.Buid, Name = b.Name }) // Ensure property names match
                                          .ToListAsync();
            return Json(buildings);
        }

        public async Task<IActionResult> GetFloorsByCompanyAndBuilding(int companyId, int buildingId)
        {
            var floors = await _context.TblFloorInfos
                                       .Where(f => f.Cid == companyId && f.Buid == buildingId)
                                       .Select(f => new { Fid = f.Fid, Name = f.Name }) // Ensure property names match
                                       .ToListAsync();
            return Json(floors);
        }




    }
}
