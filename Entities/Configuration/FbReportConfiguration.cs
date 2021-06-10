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
                    GuestsFromHotelTP = 15,
                    GuestsFromHotelTM = 13,
                    GuestsFromOutsideHotel = 10,
                    IsPublicHoliday = false,
                    EventNotes = "The DJ got everybody dancing",
                    GSourceOfBusinessNotes = "A lot of people just dropped in at around 1:00 AM",
                    Notes = "Lorem ipsum dolor sit amet",
                    Date = new DateTime(2021, 12, 2, 4, 0, 0),
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
                    GuestsFromHotelTP = 25,
                    GuestsFromHotelTM = 9,
                    GuestsFromOutsideHotel = 4,
                    IsPublicHoliday = false,
                    EventNotes = "The DJ was really good",
                    GSourceOfBusinessNotes = "A lot of people came from Google Search",
                    Notes = "Consectetur adipiscing elit",
                    Date = new DateTime(2021, 8, 6, 1, 19, 42),
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
                    GuestsFromHotelTP = 35,
                    GuestsFromHotelTM = 22,
                    GuestsFromOutsideHotel = 18,
                    IsPublicHoliday = false,
                    EventNotes = "The Flamenco dance lesson was quite nice, had many people dancing",
                    GSourceOfBusinessNotes = "Instagram",
                    Notes = "Sed do eiusmod tempor incididunt ut labore et dolore magna aliqua",
                    Date = new DateTime(2021, 7, 12, 1, 4, 9),
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
                    GuestsFromHotelTP = 11,
                    GuestsFromHotelTM = 14,
                    GuestsFromOutsideHotel = 44,
                    IsPublicHoliday = false,
                    EventNotes = "The DJ was a star",
                    Notes = "Ut enim ad minim veniam",
                    Date = new DateTime(2021, 6, 22, 1, 19, 44),
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
                    GuestsFromHotelTP = 29,
                    GuestsFromHotelTM = 21,
                    GuestsFromOutsideHotel = 21,
                    IsPublicHoliday = true,
                    EventNotes = "Umpa Umpa DJ",
                    GSourceOfBusinessNotes = "Hectic day. A lot of Germans. Since they didn't speak english " +
                        "we were unable to find out how they got to know about the Umpa Umpa Madness Night",
                    Notes = "Quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat",
                    Date = new DateTime(2021, 11, 2, 4, 0, 0),
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
                    GuestsFromHotelTP = 24,
                    GuestsFromHotelTM = 20,
                    GuestsFromOutsideHotel = 30,
                    IsPublicHoliday = false,
                    EventNotes = "The samba night was a success especially with the Italians",
                    GSourceOfBusinessNotes = "Most of the guest had been handed leaflets down town",
                    Notes = "Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur",
                    Date = new DateTime(2021, 10, 3, 3, 29, 0),
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
                    GuestsFromHotelTP = 24,
                    GuestsFromHotelTM = 21,
                    GuestsFromOutsideHotel = 30,
                    IsPublicHoliday = false,
                    EventNotes = "Busy night",
                    GSourceOfBusinessNotes = "A lot of the guests came from Agent referral",
                    Notes = "Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum",
                    Date = new DateTime(2021, 9, 5, 2, 39, 10),
                    OutletId = 4,
                    UserId = "35947f01-393b-442c-b815-d6d9f7d4b81e",
                    LocalEventId = 1
                }
            );
        }
    }
}
