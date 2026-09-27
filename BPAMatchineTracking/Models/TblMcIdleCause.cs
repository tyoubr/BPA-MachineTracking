using System;
using System.Collections.Generic;

namespace BPAMatchineTracking.Models;

public partial class TblMcIdleCause
{
    public int Icid { get; set; }

    public string? CauseName { get; set; }

    public string? Status { get; set; }

    public string? Remarks { get; set; }
}
