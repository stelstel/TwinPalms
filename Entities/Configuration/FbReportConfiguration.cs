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
                    EventNotes = "The DJ got everybody dancing",
                    GSourceOfBusinessNotes = "A lot of people just dropped in at around 1:00 AM",
                    Date = DateTime.Now,
                    OutletId = 1,
                    UserId = "35947f01-393b-442c-b815-d6d9f7d4b81e",
                    LocalEventId = 2
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
                    EventNotes = "The DJ was really good",
                    GSourceOfBusinessNotes = "A lot of peolpe came from Google Search",
                    Date = DateTime.Now.AddMonths(-1),
                    OutletId = 1,
                    UserId = "35947f01-393b-442c-b815-d6d9f7d4b81e",
                    LocalEventId = 3
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
                    EventNotes = "The Flamenco dance lesson was quite nice, had many people dancing",
                    GSourceOfBusinessNotes = "Instagram",
                    Date = DateTime.Now.AddMonths(-2),
                    OutletId = 1,
                    UserId = "35947f01-393b-442c-b815-d6d9f7d4b81e",
                    LocalEventId = 4
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
                    EventNotes = "The DJ was a star",
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
                    EventNotes = "Umpa Umpa DJ",
                    GSourceOfBusinessNotes = "Hectic day. A lot of Germans. Since the didn't speak english we were unable to find out how they got to know of the Umpa Umpa Madness Night",
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
                    EventNotes = "The samba night was a success especially with the Italians",
                    GSourceOfBusinessNotes = "Most of the guest had been handed leaflets down town",
                    Date = DateTime.Now.AddDays(-2).AddHours(-2).AddMinutes(-24),
                    OutletId = 4,
                    UserId = "35947f01-393b-442c-b815-d6d9f7d4b81e",
                    LocalEventId = 1
                },
                new FbReport
                {
                    Id = 7,
                    Tables = 20,
                    Food = 21000,
                    Beverage = 32000,
                    OtherIncome = 8500,
                    GuestsFromHotel = 24,
                    GuestsFromOutsideHotel = 30,
                    IsPublicHoliday = false,
                    EventNotes = "Busy night",
                    GSourceOfBusinessNotes = "A lot of the guests came from Agent referral",
                    Date = new DateTime(2021, 5, 09, 04, 00, 00), //closing time ex 4 am
                    OutletId = 4,
                    UserId = "35947f01-393b-442c-b815-d6d9f7d4b81e",
                    LocalEventId = 1
                }
            );
        }
    }
}
