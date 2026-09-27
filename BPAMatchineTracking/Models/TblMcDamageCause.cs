using System;
using System.Collections.Generic;

namespace BPAMatchineTrack.Models;

public partial class TblMcDamageCause
{
    public int Dcid { get; set; }

    public string? CauseName { get; set; }

    public string? Status { get; set; }

    public string? Remarks { get; set; }
}
