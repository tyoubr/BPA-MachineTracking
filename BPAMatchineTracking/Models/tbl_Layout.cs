using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace BPAMachineTrack.Models;

public partial class tbl_Layout
{
    public int SLNO { get; set; }

    public DateTime? DATE { get; set; }

    public int? MCID { get; set; }

    public int? LID { get; set; }

    public string? LOCATION_DETAILS { get; set; }

    public string? STATUS { get; set; }
    public int? OCID { get; set; }
    [ForeignKey("OCID")]
    public virtual BPAMatchineTrack.Models.tbl_Other_Company? OtherCompany { get; set; }
}

public class MachineLocationViewModel
{
    //public int SLNO { get; set; }
    //public DateTime DATE { get; set; }
    //public string MCID { get; set; }
    //public int LID { get; set; }
    //public string LocationName { get; set; }
    //public string LOCATION_DETAILS { get; set; }
    //public string STATUS { get; set; }
        public int SLNO { get; set; }
        public DateTime DATE { get; set; }
        public string MCID { get; set; }
        public string SRNO { get; set; }
        public string MachineTypeName { get; set; }
        public int? LID { get; set; }
        public string LocationName { get; set; }
        public string OtherCompanyName { get; set; }
        public string LOCATION_DETAILS { get; set; }
        public string STATUS { get; set; }
        public string MachineStatus { get; set; }
    
}
