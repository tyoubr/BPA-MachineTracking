using System;
using System.Collections.Generic;

namespace BPAMatchineTrack.Models;

public partial class TblMcUmCause
{
    public int Umcid { get; set; }

    public string? CauseName { get; set; }

    public string? Status { get; set; }

    public string? Remarks { get; set; }
}
