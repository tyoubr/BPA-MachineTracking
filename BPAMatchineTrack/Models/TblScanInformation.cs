using System;
using System.Collections.Generic;

namespace BPAMatchineTrack.Models;

public partial class TblScanInformation
{
    public int Scid { get; set; }

    public DateTime? Scdate { get; set; }

    public int? Lid { get; set; }
    public string? Details { get; set; }

    public string? Barcode { get; set; }

    public string? Remarks { get; set; }

    public string? Status { get; set; }

    public string? Opt1 { get; set; }

    public string? Opt2 { get; set; }

    public string? Opt3 { get; set; }

}
