using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DataTransferObjects
{
    public class RoomReportDto
    {
        public int Id { get; set; }
        public int NewRoomNights { get; set; }
        public int TodaysRevenuePickup { get; set; }
        public int OtherRevenue { get; set; }
        public bool IsPublicHoliday { get; set; }
        public string Notes { get; set; }
        public DateTime Date { get; set; }
        public string LoggerId { get; set; }
        public int RoomTypeId { get; set; }
        public int LocalEventId { get; set; }
        // Navigation properties
        /*public virtual Outlet Outlet { get; set; }
        public virtual User User { get; set; }
        public virtual LocalEvent LocalEvent { get; set; }
        public virtual ICollection<WeatherFbReport> WeatherFbReports { get; set; }
        public virtual ICollection<FbReportGuestSourceOfBusiness> FbReportGuestSourceOfBusinesses { get; set; }
        */

    }
}
