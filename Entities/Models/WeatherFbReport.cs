using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

#nullable disable

namespace Entities.Models
{
    public partial class WeatherFbReport
    {
        public int WeatherId { get; set; }
        public int FbReportId { get; set; }

        [JsonIgnore]
        public virtual FbReport FbReport { get; set; }
        [JsonIgnore]
        public virtual Weather Weather { get; set; }
    }
}
