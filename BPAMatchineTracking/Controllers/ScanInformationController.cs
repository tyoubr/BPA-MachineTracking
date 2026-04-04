using BPAMatchineTrack.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BPAMatchineTrack.Controllers
{
    [Authorize]
    public class ScanInformationController : Controller
    {
        private readonly CottonclubContext _context;

        public ScanInformationController(CottonclubContext context)
        {
            _context = context;
        }

        // GET: ScanInformation
        [HttpGet]
        [Authorize(Roles = "User,Admin,Super Admin")]
        public async Task<IActionResult> Index(string searchTerm, int page = 1, int pageSize = 10)
        {
            // Retrieve all scan information
            var scanInformationQuery = _context.TblScanInformations.AsQueryable();

            // Apply search filter if search term is provided
            if (!string.IsNullOrEmpty(searchTerm))
            {
                scanInformationQuery = scanInformationQuery.Where(s => s.Barcode.Contains(searchTerm)
                                                                     || s.Remarks.Contains(searchTerm)
                                                                     || s.Status.Contains(searchTerm));
            }

            // Get total records count
            var totalRecords = await scanInformationQuery.CountAsync();
            var scanInformation = await scanInformationQuery
                .OrderBy(s => s.Scid) // Order by Scid or any other property
                .Skip((page - 1) * pageSize) // Skip records for pagination
                .Take(pageSize) // Take the required number of records
                .ToListAsync();

            // Prepare pagination information
            ViewBag.TotalPages = (int)Math.Ceiling((double)totalRecords / pageSize);
            ViewBag.CurrentPage = page;
            ViewData["searchTerm"] = searchTerm;

            return View(scanInformation);
        }


        // GET: ScanInformation/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tblScanInformation = await _context.TblScanInformations
                .FirstOrDefaultAsync(m => m.Scid == id);
            if (tblScanInformation == null)
            {
                return NotFound();
            }

            return View(tblScanInformation);
        }

        // GET: ScanInformation/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ScanInformation/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create(string scannedDataJson)
        //{
        //    if (!string.IsNullOrEmpty(scannedDataJson))
        //    {
        //        var scannedData = JsonConvert.DeserializeObject<List<TblScanInformation>>(scannedDataJson);
        //        if (ModelState.IsValid && scannedData != null && scannedData.Any())
        //        {
        //            foreach (var data in scannedData)
        //            {
        //                _context.Add(data);
        //            }
        //            await _context.SaveChangesAsync();
        //            return RedirectToAction(nameof(Index));
        //        }
        //    }
        //    return View();
        //}

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "User,Admin,Super Admin")]
        public async Task<IActionResult> Create(string scannedDataJson)
        {
            if (string.IsNullOrEmpty(scannedDataJson))
            {
                ModelState.AddModelError("", "Scanned data is empty.");
                return View();
            }

            try
            {
                var scannedData = JsonConvert.DeserializeObject<List<TblScanInformation>>(scannedDataJson);

                if (scannedData == null || !scannedData.Any())
                {
                    ModelState.AddModelError("", "No valid data found in scanned data.");
                    return View();
                }

                if (ModelState.IsValid)
                {
                    _context.AddRange(scannedData);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error during Create: {ex.Message}");
                ModelState.AddModelError("", "An error occurred while processing your request.");
            }

            return View();
        }



        // GET: ScanInformation/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tblScanInformation = await _context.TblScanInformations.FindAsync(id);
            if (tblScanInformation == null)
            {
                return NotFound();
            }
            return View(tblScanInformation);
        }

        // POST: ScanInformation/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Scid,Scdate,Lid,Barcode,Remarks,Status,Opt1,Opt2,Opt3")] TblScanInformation tblScanInformation)
        {
            if (id != tblScanInformation.Scid)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tblScanInformation);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TblScanInformationExists(tblScanInformation.Scid))
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
            return View(tblScanInformation);
        }

        // GET: ScanInformation/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tblScanInformation = await _context.TblScanInformations
                .FirstOrDefaultAsync(m => m.Scid == id);
            if (tblScanInformation == null)
            {
                return NotFound();
            }

            return View(tblScanInformation);
        }

        // POST: ScanInformation/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tblScanInformation = await _context.TblScanInformations.FindAsync(id);
            if (tblScanInformation != null)
            {
                _context.TblScanInformations.Remove(tblScanInformation);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TblScanInformationExists(int id)
        {
            return _context.TblScanInformations.Any(e => e.Scid == id);
        }
        // Controller Action method to retrieve location details based on LID
        [HttpGet]
        public async Task<IActionResult> GetLocationDetails(int lid)
        {
            var location = await _context.TblMcLocations
                .Include(l => l.CidNavigation) // Include the related company
                .Include(l => l.Bu) // Include the related building
                .Include(l => l.FidNavigation) // Include the related floor
                .FirstOrDefaultAsync(l => l.Lid == lid);

            if (location != null)
            {
                // Assuming you have properties like CompanyName, BuildingName, and FloorName in your Location model
                var locationDetails = $"{location.CidNavigation?.ShortName} - {location.Bu?.Name} - {location.FidNavigation?.Name}";
                return Json(locationDetails);
            }
            return Json(null); // Or any appropriate response if location is not found
        }

        //[HttpGet]
        //public IActionResult CheckBarcodeExists(string barcode, DateTime scdate)
        //{
        //    var barcodeExists = _context.TblScanInformations
        //        .Any(b => b.Barcode == barcode && b.Scdate.HasValue && b.Scdate.Value.Date == scdate.Date);
        //    return Json(new { exists = barcodeExists });
        //}
        [HttpGet]
        public IActionResult CheckBarcodeExists(string barcode)
        {
            var barcodeExists = _context.tbl_Layouts
                .Any(l => l.MCID.ToString() == barcode); // Compare string with string

            return Json(new { exists = barcodeExists });
        }


        [HttpGet]
        public async Task<IActionResult> Search(string searchTerm)
        {
            try
            {
                // Retrieve scan information from the database
                var scanInformation = _context.TblScanInformations.AsQueryable();

                // Apply search filter if search term is provided
                if (!string.IsNullOrEmpty(searchTerm))
                {
                    scanInformation = scanInformation.Where(s => s.Barcode.Contains(searchTerm)
                                                                || s.Remarks.Contains(searchTerm)
                                                                || s.Status.Contains(searchTerm));
                }

                // Convert the query result to a list and pass it to the view
                var model = await scanInformation.ToListAsync();
                ViewData["searchTerm"] = searchTerm;
                return PartialView("_SearchResults", model);
            }
            catch (Exception ex)
            {
                // Log the error
                // You can log the error using a logging framework like Serilog, NLog, etc.
                // Example: logger.LogError(ex, "An error occurred while searching.");

                // Return an error view or handle the error as per your application's requirements
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

    }
}
