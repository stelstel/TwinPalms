using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace Entities.Configuration
{
    class OutletUserConfiguration : IEntityTypeConfiguration<OutletUser>
    {
        public void Configure(EntityTypeBuilder<OutletUser> builder)
        {
            builder.HasKey(e => new { e.UserId, e.OutletId });

            builder.ToTable("OutletUser");

            builder.HasIndex(e => e.OutletId, "IX_OutletUser_OutletId");

            builder.HasOne(d => d.Outlet)
                .WithMany(p => p.OutletUsers)
                .HasForeignKey(d => d.OutletId)
                .HasConstraintName("FK_Outlets_OutletUser");

            builder.HasOne(d => d.User)
                .WithMany(p => p.OutletUsers)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_Users_OutletUser");
        }
    }
}
