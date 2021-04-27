using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;


namespace Entities.Configuration
{
    public class FbReportConfiguration : IEntityTypeConfiguration<FbReport>
    {
        public void Configure(EntityTypeBuilder<FbReport> builder)
        {
            builder.HasData
            (
                new FbReport
                {
                    Id = 1,
                    Tables = 1,
                    Food = 10000,
                    Beverage = 20000,
                    OtherIncome = 5000,
                    GuestsFromHotel = 15,
                    GuestsFromOutsideHotel = 10,
                    IsPublicHoliday = false,
                    Notes = "Horrible. A bunch of loud people from the U.S.",
                    Date = DateTime.Now,
                    OutletId = 1,
                    UserId = 2 // TODO This should scream
                 },
                new FbReport
                {
                    Id = 2,
                    Tables = 10,
                    Food = 88000,
                    Beverage = 91000,
                    OtherIncome = 17400,
                    GuestsFromHotel = 29,
                    GuestsFromOutsideHotel = 21,
                    IsPublicHoliday = true,
                    Notes = "Hectic day. A lot of fat Germans",
                    Date = DateTime.Now.AddDays(-1).AddHours(-1),
                    OutletId = 2,
                    UserId = 1,
                    LocalEventId = 2
                },
                new FbReport
                {
                    Id = 3,
                    Tables = 20,
                    Food = 21000,
                    Beverage = 32000,
                    OtherIncome = 8500,
                    GuestsFromHotel = 24,
                    GuestsFromOutsideHotel = 30,
                    IsPublicHoliday = false,
                    Notes = "Nice day. A lot of drunk Swedes",
                    Date = DateTime.Now.AddDays(-2).AddHours(-2).AddMinutes(-24),
                    OutletId = 4,
                    UserId = 3,
                    LocalEventId = 1
                }
            );
        }
    }
}
