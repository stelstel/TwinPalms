using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace Entities.Configuration
{
    class OtherReportConfiguration : IEntityTypeConfiguration<OtherReport>
    {
        public void Configure(EntityTypeBuilder<OtherReport> builder)
        {
            builder.HasIndex(e => e.CruiseShipId, "IX_OtherReports_CruiseShipId");

            builder.HasIndex(e => e.UserId, "IX_OtherReports_UserId");

            builder.Property(e => e.Date).HasColumnType("date");

            builder.HasOne(d => d.CruiseShip)
                .WithMany(p => p.OtherReports)
                .HasForeignKey(d => d.CruiseShipId)
                .HasConstraintName("FK_CruiseShips_OtherReports");

            builder.HasOne(d => d.User)
                .WithMany(p => p.OtherReports)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_Users_OtherReports");

        //Seeding data.
        // Must be after CruiseShips seed
        // Must be after Users seed
            builder.HasData
            (
                    new OtherReport
                    {
                        Trips = 15,
                        RevenueTrips = 31200,
                        RevenueFoodAndBeverage = 50000,
                        RevenueOther = 1120,
                        TotNrOfGuests = 12,
                        IsPublicHoliday = false,
                        Notes = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et " +
                                "dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco " +
                                "laboris nisi ut aliquip ex ea commodo consequat.",
                        Date = DateTime.Now,
                        CruiseShipId = 1,
                        UserId = 3
                    },
                    new OtherReport
                    {
                        Trips = 5,
                        RevenueTrips = 32500,
                        RevenueFoodAndBeverage = 5000,
                        RevenueOther = 1230,
                        TotNrOfGuests = 22,
                        IsPublicHoliday = true,
                        Notes = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et " +
                                "dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco " +
                                "laboris nisi ut aliquip ex ea commodo consequat.",
                        Date = DateTime.Now.AddDays(-1),
                        CruiseShipId = 2,
                        UserId = 2
                    },
                    new OtherReport
                    {
                        Trips = 9,
                        RevenueTrips = 322500,
                        RevenueFoodAndBeverage = 51000,
                        RevenueOther = 130,
                        TotNrOfGuests = 29,
                        IsPublicHoliday = false,
                        Notes = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et ",
                        Date = DateTime.Now.AddDays(-2),
                        CruiseShipId = 2,
                        UserId = 1
                    }
            );
        }
    }
}
