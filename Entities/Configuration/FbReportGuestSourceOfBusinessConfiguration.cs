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
            
            // TODO seed
        }
    }
}
