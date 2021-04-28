using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace Entities.Configuration
{
    class WeatherFbReportConfiguration : IEntityTypeConfiguration<WeatherFbReport>
    {
        public void Configure(EntityTypeBuilder<WeatherFbReport> builder)
        {   
            builder.HasKey(e => new { e.WeatherId, e.FbReportId });

            builder.ToTable("WeatherFbReport");

            builder.HasIndex(e => e.FbReportId, "IX_WeatherFbReport_FbReportId");

            builder.HasIndex(e => e.WeatherId, "IX_WeatherFbReport_WeatherId");

            builder.HasOne(d => d.FbReport)
                .WithMany(p => p.WeatherFbReports)
                .HasForeignKey(d => d.FbReportId)
                .HasConstraintName("FK_FReports_WeatherFbReport");

            builder.HasOne(d => d.Weather)
                .WithMany(p => p.WeatherFbReports)
                .HasForeignKey(d => d.WeatherId)
                .HasConstraintName("FK_Weather_WeatherFbReport");

            builder.HasData
            (
                new WeatherFbReport
                {
                    WeatherId = 1,
                    FbReportId = 3
                },
                new WeatherFbReport
                {
                    WeatherId = 5,
                    FbReportId = 2
                },
                new WeatherFbReport
                {
                    WeatherId = 2,
                    FbReportId = 3
                },
                new WeatherFbReport
                {
                    WeatherId = 5,
                    FbReportId = 3
                },
                new WeatherFbReport
                {
                    WeatherId = 6,
                    FbReportId = 3
                }
            );
        }
    }
}
