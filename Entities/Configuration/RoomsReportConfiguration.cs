using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace Entities.Configuration
{
    class RoomsReportConfiguration : IEntityTypeConfiguration<RoomsReport>
    {
        public void Configure(EntityTypeBuilder<RoomsReport> builder)
        {
            builder.HasIndex(e => e.LoggerId, "IX_RoomsReports_LoggerId");

            builder.HasIndex(e => e.RoomTypeId, "IX_RoomsReports_RoomTypeId");

            builder.Property(e => e.Date).HasColumnType("date");

            builder.HasOne(d => d.Logger)
                .WithMany(p => p.RoomsReports)
                .HasForeignKey(d => d.LoggerId)
                .HasConstraintName("FK_Users_RommsReports");

            builder.HasOne(d => d.RoomType)
                .WithMany(p => p.RoomsReports)
                .HasForeignKey(d => d.RoomTypeId)
                .HasConstraintName("FK_RoomTypes_RoomsReports");

            ////Seeding data. Requires seeded data in roomtypes and users/loggers
            builder.HasData
            (
                new RoomsReport
                {
                    Id = 1,
                    NewRoomNights = 5,
                    TodaysRevenuePickup = 31200,
                    OtherRevenue = 5000,
                    IsPublicHoliday = false,
                    Notes = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et " +
                                "dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco",
                    Date = DateTime.Now,
                    RoomTypeId = 1,
                    LoggerId = "35947f01-393b-442c-b815-d6d9f7d4b81e"
                },
                new RoomsReport
                {
                    Id = 2,
                    NewRoomNights = 2,
                    TodaysRevenuePickup = 29300,
                    OtherRevenue = 4000,
                    IsPublicHoliday = false,
                    Notes = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et " +
                            "dolore magna aliqua. Ut enim ad minim veniam",
                    Date = DateTime.Now.AddDays(-1),
                    RoomTypeId = 2,
                    LoggerId = "35947f01-393b-442c-b815-d6d9f7d4b81e"
                },
                new RoomsReport
                {
                    Id = 3,
                    NewRoomNights = 4,
                    TodaysRevenuePickup = 39400,
                    OtherRevenue = 6000,
                    IsPublicHoliday = false,
                    Notes = "Lorem ipsum dolor sit amet",
                    Date = DateTime.Now.AddDays(-2),
                    RoomTypeId = 3,
                    LoggerId = "b0b22e53-3ad2-4a0a-9e58-aa0a70a5a157"
                }
            );
        }
    }
}
