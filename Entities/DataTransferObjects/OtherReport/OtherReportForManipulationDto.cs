using Entities.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace Entities.DataTransferObjects
{
    public abstract class OtherReportForManipulationDto
    {
        public int Trips { get; set; }
        public int RevenueTrips { get; set; }
        public int RevenueFoodAndBeverage { get; set; }
        public int RevenueOther { get; set; }
        public int TotNrOfGuests { get; set; }
        public bool IsPublicHoliday { get; set; }
        public string Notes { get; set; }
        public DateTime Date { get; set; }

        public string UserId { get; set; }
        public int CruiseShipId { get; set; }
    }
}
