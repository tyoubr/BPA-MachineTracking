using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BPAMachineTrack.Models;
using X.PagedList.Mvc.Core;
using X.PagedList.Extensions;
using BPAMatchineTrack.Models;
using Microsoft.IdentityModel.Tokens;
using System.Reflection.PortableExecutable;
using System.Security.Cryptography;
using X.PagedList;

namespace BPAMatchineTrack.Controllers
{
    public class LayoutController : Controller
    {
        private readonly CottonclubContext _context;

        public LayoutController(CottonclubContext context)
        {
            _context = context;
        }

        //GET: Layout
        //public IActionResult Index(string searchTerm, int? page)
        //{
        //    int pageNumber = page ?? 1;
        //    int pageSize = 14;

        //    var query = from layout in _context.tbl_Layouts
        //                join location in _context.TblMcLocations on layout.LID equals location.Lid
        //                join machine in _context.tbl_Machine_Details on layout.MCID equals machine.MCID
        //                join mtype in _context.TblMachineTypeInfo on machine.MTID equals mtype.Mtid
        //                join otherCompany in _context.tbl_Other_Companies on layout.OCID equals otherCompany.OCID
        //                                        select new
        //                {
        //                    layout.SLNO,
        //                    layout.DATE,
        //                    layout.MCID,
        //                    machine.SRNO,
        //                    mtype.Name,
        //                    layout.LID,
        //                    LocationName = location.Name,
        //                    layout.LOCATION_DETAILS,
        //                    OC_NAME = otherCompany.OC_NAME,
        //                    layout.STATUS,
        //                    machine.Status
        //                };

        //    if (!string.IsNullOrEmpty(searchTerm))
        //    {
        //        query = query.Where(x =>
        //            x.MCID.ToString().Contains(searchTerm) ||
        //            x.LocationName.Contains(searchTerm) ||
        //            x.LOCATION_DETAILS.Contains(searchTerm) ||
        //           x.OC_NAME.Contains(searchTerm) ||  
        //             x.Status.Contains(searchTerm) ||
        //            x.SRNO.Contains(searchTerm));
        //    }

        //    var pagedData = query.OrderBy(x => x.SLNO).ToPagedList(pageNumber, pageSize);

        //    return Request.Headers["X-Requested-With"] == "XMLHttpRequest"
        //        ? PartialView("_TablePartial", pagedData)
        //        : View(pagedData);
        //}

        public IActionResult Index(string searchTerm, int? page)
        {
            int pageNumber = page ?? 1;
            int pageSize = 14;

            var query = from layout in _context.tbl_Layouts
                        join location in _context.TblMcLocations on layout.LID equals location.Lid into locationJoin
                        from location in locationJoin.DefaultIfEmpty()
                        join machine in _context.tbl_Machine_Details on layout.MCID equals machine.MCID
                        join mtype in _context.TblMachineTypeInfo on machine.MTID equals mtype.Mtid
                        join otherCompany in _context.tbl_Other_Companies on layout.OCID equals otherCompany.OCID into companyJoin
                        from otherCompany in companyJoin.DefaultIfEmpty()
                        where machine.Status == "Active"   // 👈 USE MACHINE STATUS HERE
                        select new MachineLocationViewModel
                        {
                            SLNO = layout.SLNO,
                            DATE = layout.DATE,
                            MCID = layout.MCID ?? 0,
                            SRNO = machine.SRNO,
                            MachineTypeName = mtype.Name,
                            LID = layout.LID ?? 0,
                            LocationName = location != null ? location.Name : "N/A",
                            LOCATION_DETAILS = layout.LOCATION_DETAILS,
                            OC_NAME = otherCompany != null ? otherCompany.OC_NAME : "N/A",
                            STATUS = layout.STATUS,
                            MachineStatus = machine.Status
                        };

            if (!string.IsNullOrEmpty(searchTerm))
            {
                query = query.Where(x =>
                    x.MCID.ToString().Contains(searchTerm) ||
                    (x.LocationName != null && x.LocationName.Contains(searchTerm)) ||
                    (x.LOCATION_DETAILS != null && x.LOCATION_DETAILS.Contains(searchTerm)) ||
                    (x.OC_NAME != null && x.OC_NAME.Contains(searchTerm)) ||
                    (x.STATUS != null && x.STATUS.Contains(searchTerm)) ||
                    (x.MachineStatus != null && x.MachineStatus.Contains(searchTerm)) ||
                    (x.SRNO != null && x.SRNO.Contains(searchTerm))
                );
            }

            var pagedData = query.OrderBy(x => x.SLNO).ToPagedList(pageNumber, pageSize);

            return Request.Headers["X-Requested-With"] == "XMLHttpRequest"
                ? PartialView("_TablePartial", pagedData)
                : View(pagedData);
        }



        //[HttpGet]
        //public JsonResult GetAllMachineIds()
        //{
        //    var machines = _context.tbl_Machine_Details
        //        .Select(m => new { m.MCID, m.SRNO })
        //        .ToList();

        //    if (machines == null || !machines.Any())
        //    {
        //        return Json(new { success = false, message = "No machines found." });
        //    }

        //    return Json(new { success = true, data = machines });
        //}

        [HttpGet]
        public JsonResult GetAllMachineIds()
        {
            var machines = _context.tbl_Machine_Details
                .Where(m => !_context.tbl_Layouts
                    .Any(l => l.MCID == m.MCID))
                .Select(m => new
                {
                    m.MCID,
                    m.SRNO
                })
                .ToList();

            if (machines == null || !machines.Any())
            {
                return Json(new { success = false, message = "No available machines found." });
            }

            return Json(new { success = true, data = machines });
        }

        [HttpGet]
        public JsonResult GetAllLocationIds()
        {
            var locations = _context.TblMcLocations
                .Select(l => new { l.Lid, l.Name })
                .ToList();

            if (locations == null || !locations.Any())
            {
                return Json(new { success = false, message = "No locations found." });
            }

            return Json(new { success = true, data = locations });
        }

        [HttpGet]
        public JsonResult GetLocationDetails(string lid)
        {
            if (string.IsNullOrWhiteSpace(lid) || !int.TryParse(lid, out int locationId))
            {
                return Json(new { success = false, message = "Invalid Location ID." });
            }

            var location = _context.TblMcLocations
                .Where(loc => loc.Lid == locationId)
                .Select(loc => new
                {
                    CompanyName = _context.TblCompanyInfos
                        .Where(c => c.Cid == loc.Cid)
                        .Select(c => c.ShortName)
                        .FirstOrDefault(),

                    BuildingName = _context.TblBuildingInfos
                        .Where(b => b.Buid == loc.Buid)
                        .Select(b => b.Name)
                        .FirstOrDefault(),

                    FloorName = _context.TblFloorInfos
                        .Where(f => f.Fid == loc.Fid)
                        .Select(f => f.Name)
                        .FirstOrDefault()
                })
                .FirstOrDefault();

            if (location == null)
            {
                return Json(new { success = false, message = "No location details found." });
            }

            return Json(new { success = true, data = location });
        }

        // GET: Layout/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tbl_Layout = await _context.tbl_Layouts
                .FirstOrDefaultAsync(m => m.SLNO == id);
            if (tbl_Layout == null)
            {
                return NotFound();
            }

            return View(tbl_Layout);
        }

        // GET: Layout/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Layout/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SLNO,DATE,MCID,LID,LOCATION_DETAILS,STATUS")] tbl_Layout tbl_Layout)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tbl_Layout);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tbl_Layout);
        }

        // GET: Layout/Edit/5
        //public async Task<IActionResult> Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var layout = await _context.tbl_Layouts.FindAsync(id);
        //    if (layout == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(layout);
        //}
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var layout = await _context.tbl_Layouts.FindAsync(id);
            if (layout == null)
                return NotFound();

            // ✅ Add company list for dropdown
            ViewBag.Companies = new SelectList(
                _context.tbl_Other_Companies.ToList(),
                "OCID",
                "OC_NAME",
                layout.OCID // Pre-select current company
            );

            return View(layout);
        }

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public IActionResult Edit(tbl_Layout layout)
        //{
        //    if (!layout.LID.HasValue)
        //    {
        //        ModelState.AddModelError("LID", "Location ID is required.");
        //        return View(layout);
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            _context.Update(layout);
        //            _context.SaveChanges();
        //            return RedirectToAction(nameof(Index));
        //        }
        //        catch (Exception ex)
        //        {
        //            // Log the exception (optional)
        //            ModelState.AddModelError("", $"An error occurred while updating the record: {ex.Message}");
        //        }
        //    }

        //    // Return the view with the model in case of an error or invalid state
        //    return View(layout);
        //}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, tbl_Layout layout)
        {
            if (id != layout.SLNO)
                return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(layout);
                    await _context.SaveChangesAsync();
                    TempData["SuccessMessage"] = "Layout updated successfully.";
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.tbl_Layouts.Any(e => e.SLNO == id))
                        return NotFound();
                    else
                        throw;
                }
            }

            // Repopulate the dropdown on error
            ViewBag.Companies = new SelectList(
                _context.tbl_Other_Companies.ToList(),
                "OCID",
                "OC_NAME",
                layout.OCID
            );

            return View(layout);
        }

        // GET: Layout/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tbl_Layout = await _context.tbl_Layouts
                .FirstOrDefaultAsync(m => m.SLNO == id);
            if (tbl_Layout == null)
            {
                return NotFound();
            }

            return View(tbl_Layout);
        }

        // POST: Layout/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tbl_Layout = await _context.tbl_Layouts.FindAsync(id);
            if (tbl_Layout != null)
            {
                _context.tbl_Layouts.Remove(tbl_Layout);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool tbl_LayoutExists(int id)
        {
            return _context.tbl_Layouts.Any(e => e.SLNO == id);
        }
    }
}
