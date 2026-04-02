using BPAMatchineTrack.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace BPAMatchineTrack.Controllers
{
    [Authorize]
    public class MachineDetailController : Controller
    {
        private readonly CottonclubContext _context;
        private const int PageSize = 10; // Number of items per page

        public MachineDetailController(CottonclubContext context)
        {
            _context = context;
        }

        // GET: MachineDetail
        [HttpGet]
        [Authorize(Roles = "Admin,Super Admin")]
        public async Task<IActionResult> Index(string searchTerm, int page = 1)
        {
            // Query for all machine details
            var query = _context.tbl_Machine_Details
                                .Include(t => t.BR)
                                .Include(t => t.CIDNavigation)
                                .Include(t => t.MT)
                                .AsQueryable();

            // Apply search filter if searchTerm is provided
            if (!string.IsNullOrEmpty(searchTerm))
            {
                query = query.Where(md => md.Name.Contains(searchTerm)
                                        || md.MCNO.Contains(searchTerm)
                                        || md.MT.Name.Contains(searchTerm)
                                        || md.SRNO.Contains(searchTerm)
                                        || md.Model.Contains(searchTerm));
            }

            // Calculate pagination values
            var totalItems = await query.CountAsync();
            var items = await query.Skip((page - 1) * PageSize)
                                   .Take(PageSize)
                                   .ToListAsync();

            // Pass the pagination and search term information to the view
            ViewData["searchTerm"] = searchTerm;
            ViewData["CurrentPage"] = page;
            ViewData["TotalPages"] = (int)Math.Ceiling((double)totalItems / PageSize);

            return View(items);
        }
        // GET: MachineDetail/Details/5
        [HttpGet]
        [Authorize(Roles = "Admin,Super Admin")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tbl_Machine_Detail = await _context.tbl_Machine_Details
                .Include(t => t.BR)
                .Include(t => t.CIDNavigation)
                .Include(t => t.MT)
                .FirstOrDefaultAsync(m => m.MCID == id);
            if (tbl_Machine_Detail == null)
            {
                return NotFound();
            }

            return View(tbl_Machine_Detail);
        }

        // GET: MachineDetail/Create
        //public IActionResult Create()
        //{
        //    ViewData["BRID"] = new SelectList(_context.TblBrandInformation, "Brid", "Name");
        //    ViewData["CID"] = new SelectList(_context.TblCompanyInfos, "Cid", "CompanyName");

        //    // Combine Name and Description for MTID dropdown
        //    ViewData["MTID"] = new SelectList(_context.TblMachineTypeInfo
        //        .Select(mt => new { mt.Mtid, CombinedText = mt.Name + " /( " + mt.Description + " ) " })
        //        .ToList(), "Mtid", "CombinedText");

        //    // Pass the machine types as JSON for client-side processing
        //    ViewBag.MachineTypes = _context.TblMachineTypeInfo
        //        .Select(mt => new { mt.Mtid, mt.Name, mt.Description })
        //        .ToList();

        //    return View();
        //}
        // Update the Create GET action to initialize the model

        // GET: MachineDetail/Create
        [HttpGet]
        [Authorize(Roles = "Admin,Super Admin")]
        public IActionResult Create()
        {
            // Initialize a new machine detail with default values
            var model = new tbl_Machine_Detail
            {
                Status = "Active", // Set default status
                Type = "Clutch",   // Set default type
                P_System = "Yes"   // Set default pneumatic system
            };

            ViewData["BRID"] = new SelectList(_context.TblBrandInformation, "Brid", "Name");
            ViewData["CID"] = new SelectList(_context.TblCompanyInfos, "Cid", "CompanyName");

            // Combine Name and Description for MTID dropdown
            ViewData["MTID"] = new SelectList(_context.TblMachineTypeInfo
                .Select(mt => new { mt.Mtid, CombinedText = mt.Name + " /( " + mt.Description + " ) " })
                .ToList(), "Mtid", "CombinedText");

            // Pass the machine types as JSON for client-side processing
            ViewBag.MachineTypes = _context.TblMachineTypeInfo
                .Select(mt => new { mt.Mtid, mt.Name, mt.Description })
                .ToList();

            return View(model); // Pass the initialized model
        }


        // POST: MachineDetail/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create([Bind("MCID,CID,MCNO,MTID,Name,BRID,Model,SRNO,Remarks,Status")] tbl_Machine_Detail tbl_Machine_Detail)
        //{
        //    if (IsDuplicateSRNO(tbl_Machine_Detail.SRNO))
        //    {
        //        ModelState.AddModelError("SRNO", "The Serial Number already exists. Please use a unique SRNO.");
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        _context.Add(tbl_Machine_Detail);
        //        await _context.SaveChangesAsync();
        //        return RedirectToAction(nameof(Index));
        //    }

        //    ViewData["BRID"] = new SelectList(_context.TblBrandInformation, "Brid", "Name", tbl_Machine_Detail.BRID);
        //    ViewData["CID"] = new SelectList(_context.TblCompanyInfos, "Cid", "CompanyName", tbl_Machine_Detail.CID);
        //    ViewData["MTID"] = new SelectList(_context.TblMachineTypeInfo, "Mtid", "Name", tbl_Machine_Detail.MTID);

        //    return View(tbl_Machine_Detail);
        //}
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,Super Admin")]
        public async Task<IActionResult> Create([Bind("MCID,CID,MCNO,MTID,Name,BRID,Model,SRNO,Rcv_Date,Capaity,Type,P_System,Remarks,Status")] tbl_Machine_Detail tbl_Machine_Detail)
        {
            if (IsDuplicateSRNO(tbl_Machine_Detail.SRNO))
            {
                ModelState.AddModelError("SRNO", "The Serial Number already exists. Please use a unique SRNO.");
            }

            if (ModelState.IsValid)
            {
                _context.Add(tbl_Machine_Detail);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["BRID"] = new SelectList(_context.TblBrandInformation, "Brid", "Name", tbl_Machine_Detail.BRID);
            ViewData["CID"] = new SelectList(_context.TblCompanyInfos, "Cid", "CompanyName", tbl_Machine_Detail.CID);
            ViewData["MTID"] = new SelectList(_context.TblMachineTypeInfo, "Mtid", "Name", tbl_Machine_Detail.MTID);

            return View(tbl_Machine_Detail);
        }


        // GET: MachineDetail/Edit/5
        [HttpGet]
        [Authorize(Roles = "Admin,Super Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tbl_Machine_Detail = await _context.tbl_Machine_Details.FindAsync(id);
            if (tbl_Machine_Detail == null)
            {
                return NotFound();
            }

            ViewData["BRID"] = new SelectList(_context.TblBrandInformation, "Brid", "Name", tbl_Machine_Detail.BRID);
            ViewData["CID"] = new SelectList(_context.TblCompanyInfos, "Cid", "CompanyName", tbl_Machine_Detail.CID);
            ViewData["MTID"] = new SelectList(_context.TblMachineTypeInfo, "Mtid", "Name", tbl_Machine_Detail.MTID);

            ViewBag.MachineTypes = _context.TblMachineTypeInfo
                .Select(mt => new { mt.Mtid, mt.Name, mt.Description })
                .ToList();


            return View(tbl_Machine_Detail);
        }

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(int id, [Bind("MCID,CID,MCNO,MTID,Name,BRID,Model,SRNO,Remarks,Status")] tbl_Machine_Detail tbl_Machine_Detail)
        //{
        //    if (id != tbl_Machine_Detail.MCID)
        //    {
        //        return NotFound();
        //    }

        //    if (IsDuplicateSRNO(tbl_Machine_Detail.SRNO, tbl_Machine_Detail.MCID))
        //    {
        //        ModelState.AddModelError("SRNO", "The Serial Number already exists. Please use a unique SRNO.");
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            _context.Update(tbl_Machine_Detail);
        //            await _context.SaveChangesAsync();
        //        }
        //        catch (DbUpdateConcurrencyException)
        //        {
        //            if (!tbl_Machine_DetailExists(tbl_Machine_Detail.MCID))
        //            {
        //                return NotFound();
        //            }
        //            else
        //            {
        //                throw;
        //            }
        //        }
        //        return RedirectToAction(nameof(Index));
        //    }

        //    ViewData["BRID"] = new SelectList(_context.TblBrandInformation, "Brid", "Name", tbl_Machine_Detail.BRID);
        //    ViewData["CID"] = new SelectList(_context.TblCompanyInfos, "Cid", "CompanyName", tbl_Machine_Detail.CID);
        //    ViewData["MTID"] = new SelectList(_context.TblMachineTypeInfo, "Mtid", "Name", tbl_Machine_Detail.MTID);

        //    return View(tbl_Machine_Detail);
        //}

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,Super Admin")]
        public async Task<IActionResult> Edit(int id, [Bind("MCID,CID,MCNO,MTID,Name,BRID,Model,SRNO,Rcv_Date,Capaity,Type,P_System,Remarks,Status")] tbl_Machine_Detail tbl_Machine_Detail)
        {
            if (id != tbl_Machine_Detail.MCID)
            {
                return NotFound();
            }

            if (IsDuplicateSRNO(tbl_Machine_Detail.SRNO, tbl_Machine_Detail.MCID))
            {
                ModelState.AddModelError("SRNO", "The Serial Number already exists. Please use a unique SRNO.");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tbl_Machine_Detail);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!tbl_Machine_DetailExists(tbl_Machine_Detail.MCID))
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

            ViewData["BRID"] = new SelectList(_context.TblBrandInformation, "Brid", "Name", tbl_Machine_Detail.BRID);
            ViewData["CID"] = new SelectList(_context.TblCompanyInfos, "Cid", "CompanyName", tbl_Machine_Detail.CID);
            ViewData["MTID"] = new SelectList(_context.TblMachineTypeInfo, "Mtid", "Name", tbl_Machine_Detail.MTID);

            return View(tbl_Machine_Detail);
        }


        // GET: MachineDetail/Delete/5
        [HttpGet]
        [Authorize(Roles = "Admin,Super Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tbl_Machine_Detail = await _context.tbl_Machine_Details
                .Include(t => t.BR)
                .Include(t => t.CIDNavigation)
                .Include(t => t.MT)
                .FirstOrDefaultAsync(m => m.MCID == id);
            if (tbl_Machine_Detail == null)
            {
                return NotFound();
            }

            return View(tbl_Machine_Detail);
        }

        // POST: MachineDetail/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,Super Admin")]

        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tbl_Machine_Detail = await _context.tbl_Machine_Details.FindAsync(id);
            if (tbl_Machine_Detail != null)
            {
                _context.tbl_Machine_Details.Remove(tbl_Machine_Detail);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool tbl_Machine_DetailExists(int id)
        {
            return _context.tbl_Machine_Details.Any(e => e.MCID == id);
        }

        [HttpGet]
        [Authorize(Roles = "Admin,Super Admin")]
        public async Task<IActionResult> GetNextMCNO(int cid, int mtid)
        {
            // Find the max MCNO for the given CID and MTID, converting it to an integer
            var maxMcno = await _context.tbl_Machine_Details
                .Where(md => md.CID == cid && md.MTID == mtid)
                .OrderByDescending(md => md.MCNO)  // Sort descending by MCNO
                .Select(md => md.MCNO)
                .FirstOrDefaultAsync();

            // If no existing MCNO found, return "1" as the first MCNO
            if (string.IsNullOrEmpty(maxMcno))
            {
                return Json(new { nextMcno = 1 });
            }

            // Parse the MCNO as an integer and increment it by 1
            int nextMcno = int.TryParse(maxMcno, out int currentMcno) ? currentMcno + 1 : 1;

            return Json(new { nextMcno });
        }

        [HttpGet]
        [Authorize(Roles = "Admin,Super Admin")]
        public JsonResult GetNextMachineNo(int cid, int mtid)
        {
            // Find the max MCNO for the given CID and MTID, assuming it's stored as varchar
            var maxMCNO = _context.tbl_Machine_Details
                                  .Where(md => md.CID == cid && md.MTID == mtid)
                                  .Select(md => md.MCNO)
                                  .AsEnumerable()  // Convert to in-memory collection
                                  .Select(mcno => int.TryParse(mcno, out var num) ? num : 0) // Parse to int
                                  .Max(); // Get the max number

            // Increment by 1 to get the next available machine number
            var nextMCNO = (maxMCNO + 1).ToString();

            // Return the next machine number as a JSON result
            return Json(nextMCNO);
        }
        private bool IsDuplicateSRNO(string srno, int? mcid = null)
        {
            // Check if SRNO already exists, excluding the current machine being edited (if editing)
            return _context.tbl_Machine_Details
                           .Any(md => md.SRNO == srno && (!mcid.HasValue || md.MCID != mcid.Value));
        }

        // Other actions remain the same...
    }
}
