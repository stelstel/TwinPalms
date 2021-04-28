using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace Entities.Configuration
{
    class WeatherRoomsReportConfiguration : IEntityTypeConfiguration<WeatherRoomsReport>
    {
        public void Configure(EntityTypeBuilder<WeatherRoomsReport> builder)
        {
            builder.HasKey(e => new { e.WeatherId, e.RoomsReportId });

            builder.ToTable("WeatherRoomsReport");

            builder.HasIndex(e => e.RoomsReportId, "IX_WeatherRoomsReport_RoomsReportId");

            builder.HasIndex(e => e.WeatherId, "IX_WeatherRoomsReport_WeatherId");

            builder.HasOne(d => d.RoomsReport)
                .WithMany(p => p.WeatherRoomsReports)
                .HasForeignKey(d => d.RoomsReportId)
                .HasConstraintName("FK_RoomsReports_WeatherRoomsReport");

            builder.HasOne(d => d.Weather)
                .WithMany(p => p.WeatherRoomsReports)
                .HasForeignKey(d => d.WeatherId)
                .HasConstraintName("FK_Weather_WeatherRoomsReport");

            // Seeder
            builder.HasData
            (
                new WeatherRoomsReport
                {
                    WeatherId = 2,
                    RoomsReportId = 1
                },
                new WeatherRoomsReport
                {
                    WeatherId = 3,
                    RoomsReportId = 1
                },
                new WeatherRoomsReport
                {
                    WeatherId = 5,
                    RoomsReportId = 2
                },
                new WeatherRoomsReport
                {
                    WeatherId = 1,
                    RoomsReportId = 3
                },
                new WeatherRoomsReport
                {
                    WeatherId = 2,
                    RoomsReportId = 3
                }
            );
        }
    }
}
