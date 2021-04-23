using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace Entities.Models
{
    public partial class RoomsReport
    {
        public RoomsReport()
        {
            WeatherRoomsReports = new HashSet<WeatherRoomsReport>();
        }

        public int Id { get; set; }
        public int? NewRoomNights { get; set; }
        public int? TodaysRevenuePickup { get; set; }
        public int? OtherRevenue { get; set; }
        public bool IsPublicHoliday { get; set; }
        public string Notes { get; set; }
        public DateTime Date { get; set; }
        public int LoggerId { get; set; }
        public int RoomTypeId { get; set; }
        public int? LocalEventId { get; set; }

        // Navigation properties
        public virtual User Logger { get; set; }
        public virtual RoomType RoomType { get; set; }
        public virtual LocalEvent LocalEvent { get; set; }
        public virtual ICollection<WeatherRoomsReport> WeatherRoomsReports { get; set; }
    }
}
