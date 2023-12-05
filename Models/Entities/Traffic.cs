using StaffTraffic.Areas.Identity.Data;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;

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

        public DateTime OutDate { get; set; }

        public string OutDatePersian
        {
            get
            {
                if (OutDate != default)
                {
                    var calendar = new PersianCalendar();
                    return $"{calendar.GetYear(OutDate)}/{calendar.GetMonth(OutDate)}/{calendar.GetDayOfMonth(OutDate)} {calendar.GetHour(OutDate)}:{calendar.GetMinute(OutDate)}";
                     
                }
                return "";
            }
        }
        public DateTime InDate { get; set; }

        public string InDatePersian
        {
            get
            {
                if (InDate != default)
                {
                    var calendar = new PersianCalendar();
                    return $"{calendar.GetYear(InDate)}/{calendar.GetMonth(InDate)}/{calendar.GetDayOfMonth(InDate)} {calendar.GetHour(InDate)}:{calendar.GetMinute(InDate)}";

                }
                return "";
            }
        }

        [MaxLength(250)]
        public string? Description { get; set; }
    }
}
