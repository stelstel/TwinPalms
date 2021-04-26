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
                    Id = -1,
                    Name = "Catch Beach Club",
                    CompanyId = -1
                },
                new Hotel
                {
                    Id = -2,
                    Name = "Catch Junior",
                    CompanyId = -2,
                }
            );
        }
    }
}
