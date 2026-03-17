namespace BPAMatchineTrack.Models.ViewModel
{
    public class ExtraMCRequisitionViewModel
    {
        public tbl_Extra_MC_Requisition Requisition { get; set; } = new tbl_Extra_MC_Requisition();

        public string Booking_No { get; set; }
        public string Req_For { get; set; }
        public DateTime? Req_Date { get; set; }
        public int SelectedCompanyId { get; set; }
        public int SelectedBuildingId { get; set; }
        public int SelectedFloorId { get; set; }
        public int SelectedLocationId { get; set; }


        // Dropdown lists
        public List<TblCompanyInfo> Companies { get; set; } = new List<TblCompanyInfo>();
        public List<TblBuildingInfo> Buildings { get; set; } = new List<TblBuildingInfo>();
        public List<TblFloorInfo> Floors { get; set; } = new List<TblFloorInfo>();
        public List<TblMcLocation> Locations { get; set; } = new List<TblMcLocation>();
        public List<TblMachineTypeInfo> MachineTypes { get; set; } = new List<TblMachineTypeInfo>();

        // Detail rows
        public List<tbl_Extra_MC_Req_D> RequisitionDetails { get; set; } = new List<tbl_Extra_MC_Req_D>();
    }
}
