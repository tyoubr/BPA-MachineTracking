using System;
using System.Collections.Generic;

namespace BPAMatchineTrack.Models;

public partial class tbl_Extra_MC_Req_D
{
    public int TRNSID { get; set; }

    public DateTime? TRNSDATE { get; set; }

    public int? RID { get; set; }

    public int? MTID { get; set; }

    public decimal? QTY { get; set; }

    public decimal? Exist_Qty { get; set; }

    public decimal? Capacity { get; set; }

    public virtual tbl_Extra_MC_Requisition? RIDNavigation { get; set; }
}
