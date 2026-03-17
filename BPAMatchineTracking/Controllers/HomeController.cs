using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using System.Diagnostics;
using BPAMatchineTrack.Models;

namespace BPAMatchineTrack.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly string _connectionString;

        public HomeController(ILogger<HomeController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public IActionResult Index()
        {
            var dashboardData = GetDashboardData();
            return View(dashboardData);
        }

        private List<DashboardItem> GetDashboardData()
        {
            var data = new List<DashboardItem>();

            using (var connection = new SqlConnection(_connectionString))
            {
                string query = @"
                    SELECT 
     c.Company_Name, 
     COUNT(a.MCID) AS MCQTY,
     SUM(CASE WHEN d.Model != 'Rental' AND a.Status = 'Running' THEN 1 ELSE 0 END) AS OwnRunning,
     SUM(CASE WHEN d.Model = 'Rental' AND a.Status = 'Running' THEN 1 ELSE 0 END) AS RentalRunning,
     SUM(CASE WHEN d.Model != 'Rental' AND a.Status = 'Idle' THEN 1 ELSE 0 END) AS OwnIdle,
     SUM(CASE WHEN d.Model = 'Rental' AND a.Status = 'Idle' THEN 1 ELSE 0 END) AS RentalIdle,
     SUM(CASE WHEN d.Model != 'Rental' AND a.Status = 'Under Maintenance' THEN 1 ELSE 0 END) AS OwnUnderMaintenance,
     SUM(CASE WHEN d.Model = 'Rental' AND a.Status = 'Under Maintenance' THEN 1 ELSE 0 END) AS RentalUnderMaintenance,
	 SUM(CASE WHEN A.STATUS='DAMAGED' THEN 1 ELSE 0 END) AS Damaged
 FROM [dbo].[tbl_Layout] a
 LEFT JOIN [dbo].[tbl_MC_Location] b ON a.LID = b.LID
 LEFT JOIN [dbo].[tbl_Company_Info] c ON b.CID = c.CID
 LEFT JOIN [dbo].[tbl_Machine_Details] d ON a.MCID = d.MCID
 where d.status='Active'
 AND c.Company_Name is not null
 GROUP BY c.Company_Name;";

                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    data.Add(new DashboardItem
                    {
                        Company_Name = reader["Company_Name"].ToString(),
                        MCQty = Convert.ToInt32(reader["MCQTY"]),
                        OwnRunning = Convert.ToInt32(reader["OwnRunning"]),
                        RentalRunning = Convert.ToInt32(reader["RentalRunning"]),
                        OwnIdle = Convert.ToInt32(reader["OwnIdle"]),
                        RentalIdle = Convert.ToInt32(reader["RentalIdle"]),
                        OwnUnderMaintenance = Convert.ToInt32(reader["OwnUnderMaintenance"]),
                        RentalUnderMaintenance = Convert.ToInt32(reader["RentalUnderMaintenance"]),
                        Damaged = Convert.ToInt32(reader["Damaged"])
                    });
                }
            }

            return data;
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
