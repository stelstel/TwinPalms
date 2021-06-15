using Entities.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace Entities.DataTransferObjects
{
    public abstract class RoomReportForManipulationDto
    {
        [Required(ErrorMessage = "Number of new room nights is required")]
        public int NewRoomNights { get; set; }
        [Required(ErrorMessage = "Number of new room nights is required")]
        public int TodaysRevenuePickup { get; set; }
        [Required(ErrorMessage = "Number of new room nights is required")]
        public int OtherRevenue { get; set; }
        public bool IsPublicHoliday { get; set; }
        public string Notes { get; set; }
        [Required(ErrorMessage = "Number of new room nights is required")]
        [DataType(DataType.DateTime)]
        public DateTime Date { get; set; }
        [Required(ErrorMessage = "Logger is required")]
        public string LoggerId { get; set; }
        public IFormFile File { get; set; }
        [Required(ErrorMessage = "Room type is required"), Range(0, int.MaxValue)]
        public int RoomTypeId { get; set; }
        public int? LocalEventId { get; set; }

    }
}
