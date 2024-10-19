using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

using BPAMatchineTrack.Models;

namespace BPAMachineTrack.Controllers
{
    public class BuildingController : Controller
    {
        private readonly CottonclubContext _context;

        public BuildingController(CottonclubContext context)
        {
            _context = context;
        }

        // GET: Building
        public async Task<IActionResult> Index()
        {
            var cottonclubContext = _context.TblBuildingInfos.Include(t => t.CidNavigation);
            return View(await cottonclubContext.ToListAsync());
        }

        // GET: Building/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tblBuildingInfo = await _context.TblBuildingInfos
                .Include(t => t.CidNavigation)
                .FirstOrDefaultAsync(m => m.Buid == id);
            if (tblBuildingInfo == null)
            {
                return NotFound();
            }

            return View(tblBuildingInfo);
        }

        // GET: Building/Create
        public IActionResult Create()
        {
            ViewData["Cid"] = new SelectList(_context.TblCompanyInfos, "Cid", "CompanyName");
            return View();
        }

        // POST: Building/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Buid,Cid,Name,Description,Remarks,Status")] TblBuildingInfo tblBuildingInfo)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tblBuildingInfo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Cid"] = new SelectList(_context.TblCompanyInfos, "Cid", "CompanyName", tblBuildingInfo.Cid);
            return View(tblBuildingInfo);
        }


        // GET: Building/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tblBuildingInfo = await _context.TblBuildingInfos.FindAsync(id);
            if (tblBuildingInfo == null)
            {
                return NotFound();
            }
            ViewData["Cid"] = new SelectList(_context.TblCompanyInfos, "Cid", "CompanyName", tblBuildingInfo.Cid);
            return View(tblBuildingInfo);
        }

        // POST: Building/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Buid,Name,Cid,Description,Remarks,Status,Opt1,Opt2,Opt3")] TblBuildingInfo tblBuildingInfo)
        {
            if (id != tblBuildingInfo.Buid)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tblBuildingInfo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TblBuildingInfoExists(tblBuildingInfo.Buid))
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
            ViewData["Cid"] = new SelectList(_context.TblCompanyInfos, "Cid", "CompanyName", tblBuildingInfo.Cid);
            return View(tblBuildingInfo);
        }

        // GET: Building/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tblBuildingInfo = await _context.TblBuildingInfos
                .Include(t => t.CidNavigation)
                .FirstOrDefaultAsync(m => m.Buid == id);
            if (tblBuildingInfo == null)
            {
                return NotFound();
            }

            return View(tblBuildingInfo);
        }

        // POST: Building/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tblBuildingInfo = await _context.TblBuildingInfos.FindAsync(id);
            if (tblBuildingInfo != null)
            {
                _context.TblBuildingInfos.Remove(tblBuildingInfo);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TblBuildingInfoExists(int id)
        {
            return _context.TblBuildingInfos.Any(e => e.Buid == id);
        }

    }
}
