using System;
using System.Collections.Generic;

namespace BPAMatchineTrack.Models;

public partial class tbl_Extra_MC_Requisition
{
    public int RID { get; set; }

    public DateTime? RDATE { get; set; }

    public int? CID { get; set; }

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

    public virtual ICollection<tbl_Extra_MC_Req_D> tbl_Extra_MC_Req_Ds { get; set; } = new List<tbl_Extra_MC_Req_D>();
}
