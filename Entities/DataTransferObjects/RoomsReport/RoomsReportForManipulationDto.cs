using Entities.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace Entities.DataTransferObjects
{
    public abstract class RoomsReportForManipulationDto
    {
        public int NewsRoomNight { get; set; }
        public int Id { get; set; }
        public int? NewRoomNights { get; set; }
        public int? TodaysRevenuePickup { get; set; }
        public int? OtherRevenue { get; set; }
        public bool IsPublicHoliday { get; set; }
        public string Notes { get; set; }
        public DateTime Date { get; set; }
        public string LoggerId { get; set; }
        public int RoomTypeId { get; set; }
        public int? LocalEventId { get; set; }
    }
}
