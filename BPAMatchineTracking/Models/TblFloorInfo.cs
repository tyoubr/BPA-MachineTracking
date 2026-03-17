using System;
using System.Collections.Generic;

namespace BPAMatchineTrack.Models;

public partial class TblFloorInfo
{
    public int Fid { get; set; }

    public int? Cid { get; set; }

    public int? Buid { get; set; }

    public string? Name { get; set; }

    public string? Description { get; set; }

    public string? Remarks { get; set; }

    public string? Status { get; set; }

    public virtual TblBuildingInfo? Bu { get; set; }

    public virtual TblCompanyInfo? CidNavigation { get; set; }

    public virtual ICollection<TblMcLocation> TblMcLocations { get; set; } = new List<TblMcLocation>();
}
