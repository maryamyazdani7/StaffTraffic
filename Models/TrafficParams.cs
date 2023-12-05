using System.ComponentModel.DataAnnotations;

namespace StaffTraffic.Models
{
    public class TrafficParams
    {
        public Guid UserId { get; set; }

        public DateTime OutDate { get; set; }

        public DateTime InDate { get; set; }

        [MaxLength(250)]
        public string? Description { get; set; }
    }
}
