using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace Entities.Configuration
{
    class FbReportGuestSourceOfBusinessConfiguration : IEntityTypeConfiguration<FbReportGuestSourceOfBusiness>
    {
        public void Configure(EntityTypeBuilder<FbReportGuestSourceOfBusiness> builder)
        {               
            builder.HasKey(e => new { e.FbReportId, e.GuestSourceOfBusinessId });

            builder.ToTable("FbReportGuestSourceOfBusiness");

            builder.HasIndex(e => e.FbReportId, "IX_FbReportGuestSourceOfBusiness_FbReportId");

            builder.HasIndex(e => e.GuestSourceOfBusinessId, "IX_FbReportGuestSourceOfBusiness_SourceOfBusinessId");

            builder.HasOne(d => d.FbReport)
                .WithMany(p => p.FbReportGuestSourceOfBusinesses)
                .HasForeignKey(d => d.FbReportId)
                .HasConstraintName("FK_FbReports_FbReportGuestSourceOfBusiness");

            builder.HasOne(d => d.GuestSourceOfBusiness)
                .WithMany(p => p.FbReportGuestSourceOfBusinesses)
                .HasForeignKey(d => d.GuestSourceOfBusinessId)
                .HasConstraintName("FK_FbReport_FbReportGuestSourceOfBusiness");

            // Seed
            builder.HasData
            (
                new FbReportGuestSourceOfBusiness
                {
                    FbReportId = 1,
                    GuestSourceOfBusinessId = 3,
                    GsobNrOfGuests = 22
                },
                new FbReportGuestSourceOfBusiness
                {
                    FbReportId = 1,
                    GuestSourceOfBusinessId = 4,
                    GsobNrOfGuests = 13
                },
                new FbReportGuestSourceOfBusiness
                {
                    FbReportId = 2,
                    GuestSourceOfBusinessId = 1,
                    GsobNrOfGuests = 32
                },
                new FbReportGuestSourceOfBusiness
                {
                    FbReportId = 2,
                    GuestSourceOfBusinessId = 2,
                    GsobNrOfGuests = 3
                },
                new FbReportGuestSourceOfBusiness
                {
                    FbReportId = 3,
                    GuestSourceOfBusinessId = 1,
                    GsobNrOfGuests = 45
                },
                new FbReportGuestSourceOfBusiness
                {
                    FbReportId = 3,
                    GuestSourceOfBusinessId = 5,
                    GsobNrOfGuests = 22
                }
            );
        }
    }
}
