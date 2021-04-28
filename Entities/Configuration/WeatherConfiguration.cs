using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace Entities.Configuration
{
    class WeatherConfiguration : IEntityTypeConfiguration<Weather>
    {
        public void Configure(EntityTypeBuilder<Weather> builder)
        {
            builder.HasData
            (
                new Weather { Id = 1, TypeOfWeather = "Sunny/Clear" },
                new Weather { Id = 2, TypeOfWeather = "Partially Cloudy" },
                new Weather { Id = 3, TypeOfWeather = "Overcast" },
                new Weather { Id = 4, TypeOfWeather = "Rain" },
                new Weather { Id = 5, TypeOfWeather = "Showers" },
                new Weather { Id = 6, TypeOfWeather = "Stormy" }
            );
        }
    }
}
