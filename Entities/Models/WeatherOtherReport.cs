using System;
using System.Collections.Generic;

#nullable disable

namespace Entities.Models
{
    public partial class WeatherOtherReport
    {
        public int WeatherId { get; set; }
        public int OtherReportId { get; set; }

        public virtual OtherReport OtherReport { get; set; }
        public virtual Weather Weather { get; set; }
    }
}
