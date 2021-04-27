using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace Entities.Configuration
{
    class RoomTypeConfiguration : IEntityTypeConfiguration<RoomType>
    {
        public void Configure(EntityTypeBuilder<RoomType> builder)
        {
            builder.HasIndex(e => e.HotelId, "IX_RoomTypes_HotelId");

            //builder.HasIndex(e => e.Name).IsUnique();

            builder.Property(e => e.Name).IsRequired();

            builder.HasOne(d => d.Hotel)
                .WithMany(p => p.RoomTypes)
                .HasForeignKey(d => d.HotelId)
                .HasConstraintName("FK_Hotels_RoomTypes");

            builder.HasData
            (
                new RoomType { Name = "Palm Rooms", HotelId = 1 },
                new RoomType { Name = "Lagoon Pool Rooms", HotelId = 1 },
                new RoomType { Name = "Duplex Loft", HotelId = 1 },
                new RoomType { Name = "Penthouse Loft", HotelId = 1 },
                new RoomType { Name = "Azure Suites", HotelId = 2 },
                new RoomType { Name = "Grand Azure Suites", HotelId = 2 },
                new RoomType { Name = "PentHouses", HotelId = 2 }
            );
        }
    }
}
