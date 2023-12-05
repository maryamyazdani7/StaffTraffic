using StaffTraffic.Areas.Identity.Data;
using StaffTraffic.Models.Entities;

namespace StaffTraffic.Models
{
    public class TrafficPageViewModel
    {
        public List<Traffic> Traffics { get; set; }
        public List<ApplicationUser> Users { get; set; }
    }
}
