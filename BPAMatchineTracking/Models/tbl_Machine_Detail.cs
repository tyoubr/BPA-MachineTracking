//using System;
//using System.Collections.Generic;

//namespace BPAMatchineTrack.Models;

//public partial class tbl_Machine_Detail
//{
//    public int MCID { get; set; }

//    public int? CID { get; set; }

//    public string? MCNO { get; set; }

//    public int? MTID { get; set; }

//    public string? Name { get; set; }

//    public int? BRID { get; set; }

//    public string? Model { get; set; }

//    public string? SRNO { get; set; }

//    public string? Remarks { get; set; }

//    public string? Status { get; set; }

//    public virtual TblBrandInformation? BR { get; set; }

//    public virtual TblCompanyInfo? CIDNavigation { get; set; }

//    public virtual TblMachineTypeInfo? MT { get; set; }
//}

using System;
using System.Collections.Generic;

namespace BPAMatchineTrack.Models;

public partial class tbl_Machine_Detail
{
    public int MCID { get; set; }

    public int? CID { get; set; }

    public string? MCNO { get; set; }

    public int? MTID { get; set; }

    public string? Name { get; set; }

    public int? BRID { get; set; }

    public string? Model { get; set; }

    public string? SRNO { get; set; }

    public DateTime? Rcv_Date { get; set; }  // ✅ NEW COLUMN

    public string? Capaity { get; set; }     // ✅ NEW COLUMN
    public string? Type { get; set; }     // ✅ NEW COLUMN

    public string? P_System { get; set; }    // ✅ NEW COLUMN

    public string? Remarks { get; set; }

    public string? Status { get; set; }

    public virtual TblBrandInformation? BR { get; set; }

    public virtual TblCompanyInfo? CIDNavigation { get; set; }

    public virtual TblMachineTypeInfo? MT { get; set; }
}

