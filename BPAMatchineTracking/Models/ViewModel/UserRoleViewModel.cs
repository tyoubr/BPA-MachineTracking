using BPAMatchineTrack.Models.ViewModel;

namespace BPAMatchineTrack.Models.ViewModel
{
    public class UserRoleViewModel
    {
        public string UserId { get; set; }
        public string Email { get; set; }
        public string CurrentRole { get; set; }
        public string SelectedRole { get; set; }
        public List<string> Roles { get; set; }
    }
}

