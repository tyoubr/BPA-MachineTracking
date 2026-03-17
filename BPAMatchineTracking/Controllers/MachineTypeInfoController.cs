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
    public class MachineTypeInfoController : Controller
    {
        private readonly CottonclubContext _context;

        public MachineTypeInfoController(CottonclubContext context)
        {
            _context = context;
        }

        // GET: MachineTypeInfo
        public async Task<IActionResult> Index()
        {
            return View(await _context.TblMachineTypeInfo.ToListAsync());
        }

        // GET: MachineTypeInfo/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tblMachineTypeInfo = await _context.TblMachineTypeInfo
                .FirstOrDefaultAsync(m => m.Mtid == id);
            if (tblMachineTypeInfo == null)
            {
                return NotFound();
            }

            return View(tblMachineTypeInfo);
        }

        // GET: MachineTypeInfo/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: MachineTypeInfo/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Mtid,Name,Description,Status")] TblMachineTypeInfo tblMachineTypeInfo)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tblMachineTypeInfo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tblMachineTypeInfo);
        }

        // GET: MachineTypeInfo/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tblMachineTypeInfo = await _context.TblMachineTypeInfo.FindAsync(id);
            if (tblMachineTypeInfo == null)
            {
                return NotFound();
            }
            return View(tblMachineTypeInfo);
        }

        // POST: MachineTypeInfo/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Mtid,Name,Description,Status")] TblMachineTypeInfo tblMachineTypeInfo)
        {
            if (id != tblMachineTypeInfo.Mtid)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tblMachineTypeInfo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TblMachineTypeInfoExists(tblMachineTypeInfo.Mtid))
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
            return View(tblMachineTypeInfo);
        }

        // GET: MachineTypeInfo/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tblMachineTypeInfo = await _context.TblMachineTypeInfo
                .FirstOrDefaultAsync(m => m.Mtid == id);
            if (tblMachineTypeInfo == null)
            {
                return NotFound();
            }

            return View(tblMachineTypeInfo);
        }

        // POST: MachineTypeInfo/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tblMachineTypeInfo = await _context.TblMachineTypeInfo.FindAsync(id);
            if (tblMachineTypeInfo != null)
            {
                _context.TblMachineTypeInfo.Remove(tblMachineTypeInfo);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TblMachineTypeInfoExists(int id)
        {
            return _context.TblMachineTypeInfo.Any(e => e.Mtid == id);
        }
    }
}
