using System;
using System.Collections.Generic;

namespace BPAMatchineTrack.Models;

public partial class TblBrandInformation
{
    public int Brid { get; set; }

    public string? Name { get; set; }

    public string? Description { get; set; }

    public string? Status { get; set; }
    public virtual ICollection<tbl_Machine_Detail> tbl_Machine_Details { get; set; } = new List<tbl_Machine_Detail>();
}
