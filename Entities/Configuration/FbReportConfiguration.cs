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
            builder.HasOne(d => d.Outlet)
                .WithMany(p => p.FbReports)
                .HasForeignKey(d => d.OutletId)
                .HasConstraintName("FK_Outlets_FbReports");

            builder.HasOne(d => d.User)
                .WithMany(p => p.FbReports)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_Users_FbReports");

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
                    UserId = "35947f01-393b-442c-b815-d6d9f7d4b81e"
                },
                new FbReport
                {
                    Id = 2,
                    Tables = 14,
                    Food = 19000,
                    Beverage = 31000,
                    OtherIncome = 9100,
                    GuestsFromHotel = 25,
                    GuestsFromOutsideHotel = 4,
                    IsPublicHoliday = false,
                    Notes = "Typical day",
                    Date = DateTime.Now.AddMonths(-1),
                    OutletId = 1,
                    UserId = "35947f01-393b-442c-b815-d6d9f7d4b81e"
                },
                new FbReport
                {
                    Id = 3,
                    Tables = 19,
                    Food = 15000,
                    Beverage = 21000,
                    OtherIncome = 6500,
                    GuestsFromHotel = 35,
                    GuestsFromOutsideHotel = 18,
                    IsPublicHoliday = false,
                    Notes = "Lorem",
                    Date = DateTime.Now.AddMonths(-2),
                    OutletId = 1,
                    UserId = "35947f01-393b-442c-b815-d6d9f7d4b81e"
                },
                new FbReport
                {
                    Id = 4,
                    Tables = 16,
                    Food = 27000,
                    Beverage = 28000,
                    OtherIncome = 51000,
                    GuestsFromHotel = 11,
                    GuestsFromOutsideHotel = 44,
                    IsPublicHoliday = false,
                    Notes = "Ipsum",
                    Date = DateTime.Now.AddMonths(-3),
                    OutletId = 1,
                    UserId = "35947f01-393b-442c-b815-d6d9f7d4b81e"
                },
                new FbReport
                {
                    Id = 5,
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
                    UserId = "b0b22e53-3ad2-4a0a-9e58-aa0a70a5a157",
                    LocalEventId = 2
                },
                new FbReport
                {
                    Id = 6,
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
                    UserId = "35947f01-393b-442c-b815-d6d9f7d4b81e",
                    LocalEventId = 1
                }
            );
        }
    }
}
