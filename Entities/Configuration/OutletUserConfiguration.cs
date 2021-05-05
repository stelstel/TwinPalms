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
            builder.HasData(
                new OutletUser
                {
                    OutletId = 1,
                    UserId = "b0b22e53-3ad2-4a0a-9e58-aa0a70a5a157"
                },
                new OutletUser
                {
                    OutletId = 2,
                    UserId = "b0b22e53-3ad2-4a0a-9e58-aa0a70a5a157"
                },
                new OutletUser
                {
                    OutletId = 3,
                    UserId = "b0b22e53-3ad2-4a0a-9e58-aa0a70a5a157"
                },
                new OutletUser
                {
                    OutletId = 1,
                    UserId = "35947f01-393b-442c-b815-d6d9f7d4b81e"
                },
                new OutletUser
                {
                    OutletId = 4,
                    UserId = "35947f01-393b-442c-b815-d6d9f7d4b81e"
                },
                new OutletUser
                {
                    OutletId = 9,
                    UserId = "35947f01-393b-442c-b815-d6d9f7d4b81e"
                }
                );
            
        }
    }
}
