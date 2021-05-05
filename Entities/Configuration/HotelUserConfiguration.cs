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

            builder.HasData(
               new HotelUser
               {
                   UserId = "b0b22e53-3ad2-4a0a-9e58-aa0a70a5a157",
                   HotelId = 1

               }, new HotelUser
               {
                   UserId = "35947f01-393b-442c-b815-d6d9f7d4b81e",
                   HotelId = 2

               });
        }
    }
}
