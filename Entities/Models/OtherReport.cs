using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace Entities.Models
{
    public partial class OtherReport
    {
        //public OtherReport()
        //{
        //    OtherReports = new HashSet<OtherReport>();
        //}

        public int Id { get; set; }
        public int? Trips { get; set; }
        public int? RevenueTrips { get; set; }
        public int? RevenueFoodAndBeverage { get; set; }
        public int? RevenueOther { get; set; }
        public int? TotNrOfGuests { get; set; }
        public bool IsPublicHoliday { get; set; }
        public string Notes { get; set; }
        public DateTime Date { get; set; }
        [Required]
        public int UserId { get; set; }
        [Required]
        public int CruiseShipId { get; set; }

        // Navigation properties
        public virtual CruiseShip CruiseShip { get; set; }
        public virtual User User { get; set; }
        public virtual ICollection<WeatherOtherReport> WeatherOtherReports { get; set; }
    }
}
