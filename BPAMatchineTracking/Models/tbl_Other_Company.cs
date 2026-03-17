using System;
using System.Collections.Generic;

namespace BPAMatchineTrack.Models;

public partial class tbl_Other_Company
{
    public int OCID { get; set; }

    public string? OC_NAME { get; set; }

    public string? ADDRESS { get; set; }

    public string? CONTRACT_PERSON { get; set; }

    public string? REMARKS { get; set; }
    public virtual ICollection<BPAMachineTrack.Models.tbl_Layout>? Layouts { get; set; }
}
