using System;
using System.Collections.Generic;

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

        public virtual ICollection<WeatherFbReport> WeatherFbReports { get; set; }
        public virtual ICollection<WeatherOtherReport> WeatherOtherReports { get; set; }
        public virtual ICollection<WeatherRoomsReport> WeatherRoomsReports { get; set; }
    }
}
