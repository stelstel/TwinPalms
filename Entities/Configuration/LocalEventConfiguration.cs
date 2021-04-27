using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;


namespace Entities.Configuration
{
    public class LocalEventConfiguration : IEntityTypeConfiguration<LocalEvent>
    {
        public void Configure(EntityTypeBuilder<LocalEvent> builder)
        {
            builder.HasData
            (
                new LocalEvent
                {
                    Id = 1,
                    Event = "Resident DJ"
                },
                new LocalEvent
                {
                    Id = 2,
                    Event = "Guest DJ"
                },
                new LocalEvent
                {
                    Id = 3,
                    Event = "International DJ"
                },
                new LocalEvent
                {
                    Id = 4,
                    Event = "Themed event"
                }
            );
        }
    }
}
