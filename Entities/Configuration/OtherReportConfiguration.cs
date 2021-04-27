using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;


namespace Entities.Configuration
{
    public class OtherReportConfiguration : IEntityTypeConfiguration<OtherReport>
    {
        public void Configure(EntityTypeBuilder<OtherReport> builder)
        {
            builder.HasData
            (
                new OtherReport
                {
                    Id = 1,
                    Trips = 5,
                    RevenueTrips = 11500,
                    RevenueFoodAndBeverage = 24910,
                    RevenueOther = 6150,
                    TotNrOfGuests = 45,
                    IsPublicHoliday = false,
                    Notes = "Nice calm water today",
                    Date = DateTime.Now,
                    
                    CruiseShipId = 1,
                    UserId = 2 // TODO This should scream
                },
                new OtherReport
                {
                    Id = 2,
                    Trips = 7,
                    RevenueTrips = 18500,
                    RevenueFoodAndBeverage = 29810,
                    RevenueOther = 7150,
                    TotNrOfGuests = 46,
                    IsPublicHoliday = false,
                    Notes = "Lorem Ipsum",
                    Date = DateTime.Now.AddDays(-1).AddHours(-4),

                    CruiseShipId = 2,
                    UserId = 1 // TODO This should scream
                },
                new OtherReport
                {
                    Id = 3,
                    Trips = 8,
                    RevenueTrips = 19500,
                    RevenueFoodAndBeverage = 30810,
                    RevenueOther = 7250,
                    TotNrOfGuests = 49,
                    IsPublicHoliday = true,
                    Notes = "A lot of customers today",
                    Date = DateTime.Now.AddDays(-2).AddHours(-5).AddMinutes(-13),

                    CruiseShipId = 1,
                    UserId = 3 // TODO This should scream
                }
            );
        }
    }
}
