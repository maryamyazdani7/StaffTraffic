using StaffTraffic.Areas.Identity.Data;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StaffTraffic.Models.Entities
{
    public class Traffic
    {
        [Key]
        public Guid Id { get; set; }

        [ForeignKey("UserId")]
        public ApplicationUser User { get; set; }

        public Guid UserId { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime RegDate { get; set; }

        public bool IsInDate { get; set; }

        [MaxLength(250)]
        public string? Description { get; set; }
    }
}
