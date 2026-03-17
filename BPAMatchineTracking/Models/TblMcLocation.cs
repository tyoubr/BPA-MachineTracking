using System;
using System.Collections.Generic;

namespace BPAMatchineTrack.Models;

public partial class TblMcLocation
{
    public int Lid { get; set; }

    public int? Cid { get; set; }

    public int? Buid { get; set; }

    public int? Fid { get; set; }

    public string? Name { get; set; }

    public string? Remarks { get; set; }

    public string? Opt1 { get; set; }

    public string? Opt2 { get; set; }

    public string? Opt3 { get; set; }

    public string? Status { get; set; }

    public virtual TblBuildingInfo? Bu { get; set; }

    public virtual TblCompanyInfo? CidNavigation { get; set; }

    public virtual TblFloorInfo? FidNavigation { get; set; }

    //public virtual ICollection<TblMachineType> TblMachineTypes { get; set; } = new List<TblMachineType>();
}
