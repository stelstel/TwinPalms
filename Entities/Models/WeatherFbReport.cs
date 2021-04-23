using System;
using System.Collections.Generic;

#nullable disable

namespace Entities.Models
{
    public partial class WeatherFbReport
    {
        public int WeatherId { get; set; }
        public int FbReportId { get; set; }

        public virtual FbReport FbReport { get; set; }
        public virtual Weather Weather { get; set; }
    }
}
