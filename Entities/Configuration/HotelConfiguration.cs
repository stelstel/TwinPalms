using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;


namespace Entities.Configuration
{
    public class HotelConfiguration : IEntityTypeConfiguration<Hotel>
    {
        public void Configure(EntityTypeBuilder<Hotel> builder)
        {
            builder.HasData
            (
                new Hotel
                {
                    Id = 1,
                    Name = "TwinPalms Phuket",
                    CompanyId = 1
                },
                new Hotel
                {
                    Id = 2,
                    Name = "TwinPalms Montazure",
                    CompanyId = 3,
                }
            );
        }
    }
}
