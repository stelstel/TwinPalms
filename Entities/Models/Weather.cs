using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

#nullable disable

namespace Entities.Models
{
    public partial class Weather
    {
        public Weather()
        {
            WeatherFbReports = new HashSet<WeatherFbReport>();
            WeatherOtherReports = new HashSet<WeatherOtherReport>();
            WeatherRoomsReports = new HashSet<WeatherRoomsReport>();
        }

        public int Id { get; set; }
        public string TypeOfWeather { get; set; }
        [JsonIgnore]
        public virtual ICollection<WeatherFbReport> WeatherFbReports { get; set; }
        [JsonIgnore]
        public virtual ICollection<WeatherOtherReport> WeatherOtherReports { get; set; }
        [JsonIgnore]
        public virtual ICollection<WeatherRoomsReport> WeatherRoomsReports { get; set; }
    }
}
