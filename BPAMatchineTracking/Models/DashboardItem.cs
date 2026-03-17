namespace BPAMatchineTrack.Models
{
    public class DashboardItem
    {
        public string Company_Name { get; set; }
        public int MCQty { get; set; }
        public int OwnRunning { get; set; }
        public int RentalRunning { get; set; }
        public int OwnIdle { get; set; }
        public int RentalIdle { get; set; }
        public int OwnUnderMaintenance { get; set; }
        public int RentalUnderMaintenance { get; set; }
        public int Damaged {  get; set; }
    }
}
