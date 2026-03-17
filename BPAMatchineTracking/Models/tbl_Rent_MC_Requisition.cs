
//using System;
//using System.Collections.Generic;
//using System.ComponentModel.DataAnnotations.Schema;

//namespace BPAMatchineTrack.Models
//{
//    public partial class tbl_Rent_MC_Requisition
//    {
//        public int RID { get; set; }
//        public DateTime? RDATE { get; set; }
//        public int? CID { get; set; } // Foreign key to Company
//        public string? BOOKING_NO { get; set; }
//        public DateTime? REQUIRED_DATE { get; set; }
//        public int? BUID { get; set; }
//        public int? FID { get; set; }
//        public int? LID { get; set; }
//        public string? REQ_FOR { get; set; }
//        public string? PREPARE_BY { get; set; }
//        public string? CHECKED_BY { get; set; }
//        public string? AUTH_BY { get; set; }
//        public string? OPT1 { get; set; }
//        public string? OPT2 { get; set; }
//        public string? OPT3 { get; set; }

//    }
//}
using BPAMatchineTrack.Models;
using System.ComponentModel.DataAnnotations.Schema;
namespace BPAMatchineTrack.Models
{
    public partial class tbl_Rent_MC_Requisition
    {
        public int RID { get; set; }
        public DateTime? RDATE { get; set; }
        public int? CID { get; set; } // Foreign key to Company
        public string? BOOKING_NO { get; set; }
        public DateTime? REQUIRED_DATE { get; set; }
        public int? BUID { get; set; }
        public int? FID { get; set; }
        public int? LID { get; set; }
        public string? REQ_FOR { get; set; }
        public string? PREPARE_BY { get; set; }
        public string? CHECKED_BY { get; set; }
        public string? AUTH_BY { get; set; }
        public string? OPT1 { get; set; }
        public string? OPT2 { get; set; }
        public string? OPT3 { get; set; }

        // Navigation property for the Company
        [ForeignKey("CID")]
        public virtual TblCompanyInfo? Company { get; set; }
    }
}