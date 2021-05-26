using Entities.Models;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Entities.DataTransferObjects
{
    public class FbReportDto
    {
        public int Id { get; set; }
        public int? Tables { get; set; }
        public int? Food { get; set; }
        public int? Beverage { get; set; }
        public int? OtherIncome { get; set; }
        public int? GuestsFromHotel { get; set; }
        public int? GuestsFromOutsideHotel { get; set; }
        public bool IsPublicHoliday { get; set; }
        public string EventNotes { get; set; }
        public string ImagePath { get; set; }
        public string GSourceOfBusinessNotes { get; set; }
        public DateTime Date { get; set; }
        public int OutletId { get; set; }
        public string UserId { get; set; }
        public int? LocalEventId { get; set; }

        // Navigation properties
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public ICollection<GuestSourceOfBusiness> GuestSourceOfBusinesses { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public ICollection<Weather> Weathers { get; set; }


    }
}
