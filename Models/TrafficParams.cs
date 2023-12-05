using System.ComponentModel.DataAnnotations;

namespace StaffTraffic.Models
{
    public class TrafficParams
    {
        public Guid UserId { get; set; }

        public string OutDate { get; set; }

        public string InDate { get; set; }

        [MaxLength(250)]
        public string? Description { get; set; }
    }
}
