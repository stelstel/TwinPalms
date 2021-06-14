using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

#nullable disable

namespace Entities.Models
{
    public partial class FbReport
    {
        public FbReport()
        {
            WeatherFbReports = new HashSet<WeatherFbReport>();
            FbReportGuestSourceOfBusinesses = new HashSet<FbReportGuestSourceOfBusiness>();
        }

        public int Id { get; set; }
        public int? Tables { get; set; }
        public int? Food { get; set; }
        public int? Beverage { get; set; }
        public int? OtherIncome { get; set; }
        public int? GuestsFromHotel { get; set; }
        public int? GuestsFromOutsideHotel { get; set; }
        public bool IsPublicHoliday { get; set; }
        public string EventNotes { get; set; }
        public string ImagePath { get; set; } = "fakeImagePath";
        public string GSourceOfBusinessNotes { get; set; }
        [Required]
        public DateTime Date { get; set; }
        [Required]
        public int OutletId { get; set; }
        [Required]
        public string UserId { get; set; }
        public int? LocalEventId { get; set; }

        // timestamp
        public DateTime CreatedAt { get; set; } = DateTime.Now; 


        // Navigation properties
        public virtual Outlet Outlet { get; set; }
        public virtual User User { get; set; }
        public virtual LocalEvent LocalEvent { get; set; }
        public virtual ICollection<WeatherFbReport> WeatherFbReports { get; set; }
        public virtual ICollection<FbReportGuestSourceOfBusiness> FbReportGuestSourceOfBusinesses { get; set; }
    }
}
