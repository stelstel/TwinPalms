using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DataTransferObjects
{
    public class OtherReportDto
    {
        public int Id { get; set; }
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
