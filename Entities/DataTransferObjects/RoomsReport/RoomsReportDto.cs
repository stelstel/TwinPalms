using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DataTransferObjects
{
    public class RoomsReportDto
    {
        public int Id { get; set; }
        public int? Tables { get; set; }
        public int? Food { get; set; }
        public int? Beverage { get; set; }
        public int? OtherIncome { get; set; }
        public int? GuestsFromHotel { get; set; }
        public int? GuestsFromOutsideHotel { get; set; }
        public bool IsPublicHoliday { get; set; }
        public string Notes { get; set; }
        public DateTime Date { get; set; }
        public int OutletId { get; set; }
        public int UserId { get; set; }
        public int? LocalEventId { get; set; }

        // Navigation properties
        /*public virtual Outlet Outlet { get; set; }
        public virtual User User { get; set; }
        public virtual LocalEvent LocalEvent { get; set; }
        public virtual ICollection<WeatherFbReport> WeatherFbReports { get; set; }
        public virtual ICollection<FbReportGuestSourceOfBusiness> FbReportGuestSourceOfBusinesses { get; set; }
        */

    }
}
