using Entities.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace Entities.DataTransferObjects
{
    public abstract class FbReportForManipulationDto
    {
        [Range(0, 10000, ErrorMessage = "Tables can't be lower than 0")]
        public int? Tables { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Food can't be lower than 0")]
        public int? Food { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Beverage can't be lower than 0")]
        public int? Beverage { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "OtherIncome can't be lower than 0")]
        public int? OtherIncome { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "GuestsFromHotel can't be lower than 0")]
        public int? GuestsFromHotel { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "GuestsFromOutsideHotel can't be lower than 0")]
        public int? GuestsFromOutsideHotel { get; set; }

        [Required(ErrorMessage = "IsPublicHoliday is a required field (true/false)")]
        public bool IsPublicHoliday { get; set; }

        [StringLength(5000, ErrorMessage = "Notes can't contain more than 5000 characters")]
        [DataType(DataType.Text)]
        public string Notes { get; set; }

        [Required(ErrorMessage = "Date is a required field.")]
        [DataType(DataType.DateTime)]
        public DateTime Date { get; set; }

        [Required(ErrorMessage = "OutletId is a required field.")]
        [Range(1, 10000, ErrorMessage = "OutletId is a required field and can't be lower than 1 or higer than 10000")]
        public int OutletId { get; set; }
        
        [Required(ErrorMessage = "UserId is a required field.")]
        [StringLength(36, MinimumLength = 36, ErrorMessage = "UserId should contain exactly 36 characters")]
        public string UserId { get; set; }

        [Range(1, 1000, ErrorMessage = "LocalEventId can't be lower than 1")]
        public int? LocalEventId { get; set; }

        public ICollection<int> Weathers { get; set; }
        public ICollection<int> GuestSourceOfBusinesses { get; set; }
    }
}
