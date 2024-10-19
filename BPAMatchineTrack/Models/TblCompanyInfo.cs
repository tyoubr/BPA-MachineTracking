using System;
using System.Collections.Generic;

namespace BPAMatchineTrack.Models;

public partial class TblCompanyInfo
{
    public int Cid { get; set; }

    public string? CompanyName { get; set; }

    public string? ShortName { get; set; }

    public string? Address { get; set; }

    public string? Phone { get; set; }

    public string? Email { get; set; }

    public string? Remarks { get; set; }

    public string? Opt1 { get; set; }

    public string? Opt2 { get; set; }

    public string? Opt3 { get; set; }

    public string? Opt4 { get; set; }

    public string? Opt5 { get; set; }

    public string? Status { get; set; }

    public virtual ICollection<TblBuildingInfo> TblBuildingInfos { get; set; } = new List<TblBuildingInfo>();

    public virtual ICollection<TblFloorInfo> TblFloorInfos { get; set; } = new List<TblFloorInfo>();

    public virtual ICollection<TblMcLocation> TblMcLocations { get; set; } = new List<TblMcLocation>();
    public virtual ICollection<tbl_Machine_Detail> tbl_Machine_Details { get; set; } = new List<tbl_Machine_Detail>();
}
