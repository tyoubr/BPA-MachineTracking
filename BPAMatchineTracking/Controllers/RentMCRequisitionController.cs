using BPAMatchineTrack.Models;
using BPAMatchineTrack.ViewModels; // Ensure this line is present
using DevExpress.DataAccess.Sql;
using DevExpress.XtraReports.UI;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace BPAMatchineTrack.Controllers
{
    [Authorize]
    public class RentMCRequisitionController : Controller
    {
        private readonly CottonclubContext _context;

        public RentMCRequisitionController(CottonclubContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            // Sort the requisitions by RID in descending order so the last saved is shown first
            var requisitions = _context.tbl_Rent_MC_Requisition
                                .Include(r => r.Company) // Make sure Company is included for displaying the name
                                .OrderByDescending(r => r.RID) // or use r.RDATE if you'd prefer
                                .ToList();

            return View(requisitions);
        }

              

        [HttpGet] // Explicitly mark the GET method
        public IActionResult Create()
        {
            var viewModel = new RentMCRequisitionViewModel
            {
                Requisition = new tbl_Rent_MC_Requisition
                {
                    RDATE = DateTime.Now // Set current date
                },
                Companies = _context.TblCompanyInfos.ToList(),
                MachineTypes = _context.TblMachineTypeInfo.ToList()
            };
            return View(viewModel);
        }

        [HttpPost] // Explicitly mark the POST method
        public async Task<IActionResult> Create(RentMCRequisitionViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                using (var transaction = await _context.Database.BeginTransactionAsync())
                {
                    try
                    {
                        // Save master requisition
                        var requisition = viewModel.Requisition;

                        // Set foreign key and other properties
                        requisition.CID = viewModel.SelectedCompanyId;
                        requisition.BUID = viewModel.SelectedBuildingId; // Ensure these properties are defined
                        requisition.FID = viewModel.SelectedFloorId;
                        requisition.LID = viewModel.SelectedLocationId;
                        requisition.BOOKING_NO = viewModel.Booking_No;
                        requisition.REQ_FOR = viewModel.Req_For;
                        requisition.REQUIRED_DATE = viewModel.Req_Date;

                        // Add the requisition to the context
                        _context.tbl_Rent_MC_Requisition.Add(requisition);
                        await _context.SaveChangesAsync();

                        // Save detail records if any
                        if (viewModel.RequisitionDetails != null && viewModel.RequisitionDetails.Count > 0)
                        {
                            foreach (var detail in viewModel.RequisitionDetails)
                            {
                                if (detail.MTID > 0 && detail.QTY > 0)
                                {
                                    detail.RID = requisition.RID; // Link master RID
                                    _context.tbl_Rent_MC_Req_D.Add(detail);
                                }
                            }
                            await _context.SaveChangesAsync();
                        }

                        await transaction.CommitAsync();
                        return RedirectToAction("Index");
                    }
                    catch (DbUpdateException ex)
                    {
                        await transaction.RollbackAsync();
                        ModelState.AddModelError("", "Database error: " + ex.InnerException?.Message);
                    }
                    catch (Exception ex)
                    {
                        await transaction.RollbackAsync();
                        ModelState.AddModelError("", "An unexpected error occurred: " + ex.Message);
                    }
                }
            }

            PopulateDropdowns(viewModel);
            return View(viewModel);
        }
        
        private void PopulateDropdowns(RentMCRequisitionViewModel viewModel)
        {
            viewModel.Companies = _context.TblCompanyInfos.ToList();
            viewModel.Buildings = _context.TblBuildingInfos.ToList();
            viewModel.Floors = _context.TblFloorInfos.ToList();
            viewModel.Locations = _context.TblMcLocations.ToList();
            viewModel.MachineTypes = _context.TblMachineTypeInfo.ToList();
        }

        [HttpGet]
        public JsonResult GetBuildingsByCompany(int companyId)
        {
            var buildings = _context.TblBuildingInfos
                                    .Where(b => b.Cid == companyId)
                                    .Select(b => new { BuildingId = b.Buid, BuildingName = b.Name })
                                    .ToList();
            return Json(buildings);
        }

        [HttpGet]
        public JsonResult GetFloorsByBuilding(int buildingId)
        {
            var floors = _context.TblFloorInfos
                                 .Where(f => f.Buid == buildingId)
                                 .Select(f => new { FloorId = f.Fid, FloorName = f.Name })
                                 .ToList();
            return Json(floors);
        }

        [HttpGet]
        public JsonResult GetLocationsByFloor(int floorId)
        {
            var locations = _context.TblMcLocations
                                    .Where(l => l.Fid == floorId)
                                    .Select(l => new { LocationId = l.Lid, LocationName = l.Name })
                                    .ToList();
            return Json(locations);
        }

    }
}
