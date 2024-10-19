using System;
using System.Collections.Generic;

namespace BPAMatchineTrack.Models;

public partial class tbl_Rent_MC_Req_D
{
    public int TRNSID { get; set; }

    public DateTime? TRNSDATE { get; set; }

    public int? RID { get; set; }

    public int? MTID { get; set; }

    public decimal? QTY { get; set; }

    public string? REMARKS { get; set; }

    
}
