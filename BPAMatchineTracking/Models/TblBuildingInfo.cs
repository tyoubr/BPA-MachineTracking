using System;
using System.Collections.Generic;

namespace BPAMatchineTrack.Models;

public partial class TblBuildingInfo
{
    public int Buid { get; set; }

    public string? Name { get; set; }

    public int? Cid { get; set; }

    public string? Description { get; set; }

    public string? Remarks { get; set; }

    public string? Status { get; set; }

    public string? Opt1 { get; set; }

    public string? Opt2 { get; set; }

    public string? Opt3 { get; set; }

    public virtual TblCompanyInfo? CidNavigation { get; set; }

    public virtual ICollection<TblFloorInfo> TblFloorInfos { get; set; } = new List<TblFloorInfo>();

    public virtual ICollection<TblMcLocation> TblMcLocations { get; set; } = new List<TblMcLocation>();
}
