using BPAMachineTrack.Models;
using BPAMatchineTrack.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Security.Cryptography;
using System.Threading.Tasks;
using X.PagedList;
using X.PagedList.Extensions;
using X.PagedList.Mvc.Core;

namespace BPAMatchineTrack.Controllers
{
    [Authorize(Roles = "Admin,Super Admin,User")]
    public class LayoutController : Controller
    {
        private readonly CottonclubContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public LayoutController(CottonclubContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
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
        [HttpGet]
        [Authorize(Roles = "User,Admin,Super Admin")]
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
        [Authorize(Roles = "User,Admin,Super Admin")]
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
        [Authorize(Roles = "User,Admin,Super Admin")]
        public async Task<JsonResult> GetAllLocationIdsAsync()
        {
            //var locations = _context.TblMcLocations
            //    .Select(l => new { l.Lid, l.Name })
            //    .ToList();

            //var locations = (from l in _context.TblMcLocations
            //                 join c in _context.TblCompanyInfos
            //                 on l.Cid equals c.Cid
            //                 select new
            //                 {
            //                     l.Lid,
            //                     l.Name,
            //                     CompanyName = c.CompanyName
            //                 })
            //     .ToList();

            //var userName = User.Identity.Name;
            //var prefix = userName.Split('_')[0].ToLower();

            //var query = _context.TblMcLocations.Join(_context.TblCompanyInfos,
            //              l => l.Cid,
            //              c => c.Cid,
            //              (l, c) => new
            //              {
            //                  l.Lid,
            //                  l.Name,
            //                  CompanyName = c.CompanyName,
            //                  ShortName = c.ShortName
            //              });

            //var hasMatch = query.Any(x => x.ShortName.ToLower() == prefix);

            //var locations = hasMatch
            //    ? query.Where(x => x.ShortName.ToLower() == prefix)
            //           .Select(x => new { x.Lid, x.Name, x.CompanyName })
            //           .ToList()
            //    : query.Select(x => new { x.Lid, x.Name, x.CompanyName })
            //           .ToList();

            var user = await _userManager.GetUserAsync(User);

            //bool isAdmin = await _userManager.IsInRoleAsync(user, "Admin")
            //            || await _userManager.IsInRoleAsync(user, "SuperAdmin");
            bool isAdmin = User.IsInRole("Admin") || User.IsInRole("Super Admin");

            var prefix = user.UserName.Split('_')[0].ToLower();
            //var userName = User.Identity.Name;
            //var prefix = userName.Split('_')[0].ToLower();

            var locations = _context.TblMcLocations.Join(_context.TblCompanyInfos,
                              l => l.Cid,
                              c => c.Cid,
                              (l, c) => new
                              {
                                  l.Lid,
                                  l.Name,
                                  CompanyName = c.CompanyName,
                                  ShortName = c.ShortName
                              })
                            .Where(x => isAdmin || x.ShortName.ToLower() == prefix)
                            .Select(x => new
                            {
                                x.Lid,
                                x.Name,
                                x.CompanyName
                            })
                            .ToList();

            if (locations == null || !locations.Any())
            {
                return Json(new { success = false, message = "No locations found." });
            }

            return Json(new { success = true, data = locations });
        }

        [HttpGet]
        [Authorize(Roles = "User,Admin,Super Admin")]
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

        [HttpGet]
        [Authorize(Roles = "User,Admin,Super Admin")]
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
        [Authorize(Roles = "User,Admin,Super Admin")]
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
        [HttpGet]
        [Authorize(Roles = "User,Admin,Super Admin")]
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
        [Authorize(Roles = "User,Admin,Super Admin")]
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
        [HttpGet]
        [Authorize(Roles = "User,Admin,Super Admin")]
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
        [Authorize(Roles = "User,Admin,Super Admin")]
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
