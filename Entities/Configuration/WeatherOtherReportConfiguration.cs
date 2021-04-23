using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace Entities.Configuration
{
    class WeatherOtherReportConfiguration : IEntityTypeConfiguration<WeatherOtherReport>
    {
        public void Configure(EntityTypeBuilder<WeatherOtherReport> builder)
        {
            builder.HasKey(e => new { e.WeatherId, e.OtherReportId });

            builder.ToTable("WeatherOtherReport");

            builder.HasIndex(e => e.OtherReportId, "IX_WeatherOtherReport_OtherReportId");

            builder.HasIndex(e => e.WeatherId, "IX_WeatherOtherReport_WeatherId");

            builder.HasOne(d => d.OtherReport)
                .WithMany(p => p.WeatherOtherReports)
                .HasForeignKey(d => d.OtherReportId)
                .HasConstraintName("FK_OtherReports_WeatherOtherReport");

            builder.HasOne(d => d.Weather)
                .WithMany(p => p.WeatherOtherReports)
                .HasForeignKey(d => d.WeatherId)
                .HasConstraintName("FK_Weather_WeatherOtherReport");
        }
    }
}
