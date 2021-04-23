using System;
using System.Collections.Generic;

#nullable disable

namespace Entities.Models
{
    public partial class WeatherRoomsReport
    {
        public int WeatherId { get; set; }
        public int RoomsReportId { get; set; }

        public virtual RoomsReport RoomsReport { get; set; }
        public virtual Weather Weather { get; set; }
    }
}
