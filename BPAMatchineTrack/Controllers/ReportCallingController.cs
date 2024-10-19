using DevExpress.XtraReports.UI;
using Microsoft.AspNetCore.Mvc;

namespace BPAMatchineTrack.Controllers
{
    public class ReportCallingController : Controller
    {
        public IActionResult Test()
        {
            try
            {
                var rptPath = "BPAMatchineTrack.Reports.XtraReport2";
                XtraReport report = (XtraReport)Activator.CreateInstance(Type.GetType(rptPath));
                ViewBag.ReportName = report;
                return View("~/Views/Shared/_LayoutReport.cshtml");
            }
            catch (Exception ex)
            {

                throw ex.InnerException;
            }
            
        }

        public IActionResult Test1()
        {
            try
            {
                var rptPath = "BPAMatchineTrack.Reports.XtraReport1";
                XtraReport report = (XtraReport)Activator.CreateInstance(Type.GetType(rptPath));
                ViewBag.ReportName = report;
                return View("~/Views/Shared/_LayoutReport.cshtml");
            }
            catch (Exception ex)
            {

                throw ex.InnerException;
            }

        }
        public IActionResult Test2()
        {
            try
            {
                var rptPath = "BPAMatchineTrack.Reports.XtraReport2";
                XtraReport report = (XtraReport)Activator.CreateInstance(Type.GetType(rptPath));
                ViewBag.ReportName = report;
                return View("~/Views/Shared/_LayoutReport.cshtml");
            }
            catch (Exception ex)
            {

                throw ex.InnerException;
            }

        }
        
             public IActionResult Test3()
        {
            try
            {
                var rptPath = "BPAMatchineTrack.Reports.rptLocation";
                XtraReport report = (XtraReport)Activator.CreateInstance(Type.GetType(rptPath));
                ViewBag.ReportName = report;
                return View("~/Views/Shared/_LayoutReport.cshtml");
            }
            catch (Exception ex)
            {

                throw ex.InnerException;
            }

        }
        public IActionResult MachineInfo()
        {
            try
            {
                var rptPath = "BPAMatchineTrack.Reports.rptMachineInformation";
                XtraReport report = (XtraReport)Activator.CreateInstance(Type.GetType(rptPath));
                ViewBag.ReportName = report;
                return View("~/Views/Shared/_LayoutReport.cshtml");
            }
            catch (Exception ex)
            {

                throw ex.InnerException;
            }

        }
        public IActionResult MachineInfo_CCBDL()
        {
            try
            {
                var rptPath = "BPAMatchineTrack.Reports.rptMachineScanning_CCBDL";
                XtraReport report = (XtraReport)Activator.CreateInstance(Type.GetType(rptPath));
                ViewBag.ReportName = report;
                return View("~/Views/Shared/_LayoutReport.cshtml");
            }
            catch (Exception ex)
            {

                throw ex.InnerException;
            }

        }
       
            public IActionResult MachineInfo_CCBLE()
        {
            try
            {
                var rptPath = "BPAMatchineTrack.Reports.rptMachineScanning_CCBLE";
                XtraReport report = (XtraReport)Activator.CreateInstance(Type.GetType(rptPath));
                ViewBag.ReportName = report;
                return View("~/Views/Shared/_LayoutReport.cshtml");
            }
            catch (Exception ex)
            {

                throw ex.InnerException;
            }

        }
        public IActionResult MachineInfo_CCBD()
        {
            try
            {
                var rptPath = "BPAMatchineTrack.Reports.rptMachineScanning_CCBD";
                XtraReport report = (XtraReport)Activator.CreateInstance(Type.GetType(rptPath));
                ViewBag.ReportName = report;
                return View("~/Views/Shared/_LayoutReport.cshtml");
            }
            catch (Exception ex)
            {

                throw ex.InnerException;
            }

        }
        public IActionResult RentMCRequisition(int id)
        {
            try
            {
                var rptPath = "BPAMatchineTrack.Reports.rptRentMCRequisition";
                XtraReport report = (XtraReport)Activator.CreateInstance(Type.GetType(rptPath));
                ViewBag.ReportName = report;
                return View("~/Views/Shared/_LayoutReport.cshtml");
            }
            catch (Exception ex)
            {

                throw ex.InnerException;
            }

        }

        public IActionResult RentalMachineStatus(int id)
        {
            try
            {
                var rptPath = "BPAMatchineTrack.Reports.rptRental_Machine_Status";
                XtraReport report = (XtraReport)Activator.CreateInstance(Type.GetType(rptPath));
                ViewBag.ReportName = report;
                return View("~/Views/Shared/_LayoutReport.cshtml");
            }
            catch (Exception ex)
            {

                throw ex.InnerException;
            }

        }
        public IActionResult IdleMachineStatus(int id)
        {
            try
            {
                var rptPath = "BPAMatchineTrack.Reports.rptIdleMachineStatus";
                XtraReport report = (XtraReport)Activator.CreateInstance(Type.GetType(rptPath));
                ViewBag.ReportName = report;
                return View("~/Views/Shared/_LayoutReport.cshtml");
            }
            catch (Exception ex)
            {

                throw ex.InnerException;
            }

        }
        public IActionResult UMMachineStatus(int id)
        {
            try
            {
                var rptPath = "BPAMatchineTrack.Reports.rptUMMachineStatus";
                XtraReport report = (XtraReport)Activator.CreateInstance(Type.GetType(rptPath));
                ViewBag.ReportName = report;
                return View("~/Views/Shared/_LayoutReport.cshtml");
            }
            catch (Exception ex)
            {

                throw ex.InnerException;
            }

        }

    }
}
