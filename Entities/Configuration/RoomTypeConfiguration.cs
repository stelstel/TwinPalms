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
                new RoomType { Id = 1, Name = "Palm Rooms", HotelId = 1 },
                new RoomType { Id = 2, Name = "Lagoon Pool Rooms", HotelId = 1 },
                new RoomType { Id = 3, Name = "Duplex Loft", HotelId = 1 },
                new RoomType { Id = 4, Name = "Penthouse Loft", HotelId = 1 },
                new RoomType { Id = 5, Name = "Azure Suites", HotelId = 2 },
                new RoomType { Id = 6, Name = "Grand Azure Suites", HotelId = 2 },
                new RoomType { Id = 7, Name = "PentHouses", HotelId = 2 }
            );
        }
    }
}
