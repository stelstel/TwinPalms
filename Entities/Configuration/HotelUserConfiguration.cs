using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace Entities.Configuration
{
    class HotelUserConfiguration : IEntityTypeConfiguration<HotelUser>
    {
        public void Configure(EntityTypeBuilder<HotelUser> builder)
        {
            builder.HasKey(e => new { e.UserId, e.HotelId });

            builder.ToTable("HotelUser");

            builder.HasIndex(e => e.HotelId, "IX_HotelUser_HotelId");

            builder.HasOne(d => d.Hotel)
                .WithMany(p => p.HotelUsers)
                .HasForeignKey(d => d.HotelId)
                .HasConstraintName("FK_Hotels_HotelUser");

            builder.HasOne(d => d.User)
                .WithMany(p => p.HotelUsers)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_Users_HotelUser");
        }
    }
}
