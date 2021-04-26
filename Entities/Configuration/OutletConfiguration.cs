using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;


namespace Entities.Configuration
{
    public class OutletConfiguration : IEntityTypeConfiguration<Outlet>
    {
        public void Configure(EntityTypeBuilder<Outlet> builder)
        {
            builder.HasData
            (
                new Outlet
                {
                    Id = 1,
                    Name = "Catch Beach Club",
                    CompanyId = 1
                },
                new Outlet
                {
                    Id = 2,
                    Name = "Catch Junior",
                    CompanyId = 1,
                },
                new Outlet
                {
                    Id = 3,
                    Name = "Wagyu Steakhouse",
                    CompanyId = 1,
                },
                new Outlet
                {
                    Id = 4,
                    Name = "Palm Seaside",
                    CompanyId = 1,
                },
                new Outlet
                {
                    Id = 5,
                    Name = "Oriental Spoon",
                    CompanyId = 1,
                },
                new Outlet
                {
                    Id = 6,
                    Name = "HQ Beach Lounge",
                    CompanyId = 1,
                },
                new Outlet
                {
                    Id = 7,
                    Name = "Shimmer",
                    CompanyId = 2,
                },
                new Outlet
                {
                    Id = 8,
                    Name = "Bake Laguna",
                    CompanyId = 2,
                },
                new Outlet
                {
                    Id = 9,
                    Name = "Bake BIS",
                    CompanyId = 2,
                },
                new Outlet
                {
                    Id = 10,
                    Name = "Bake Turtle Village",
                    CompanyId = 2,
                },
                new Outlet
                {
                    Id = 11,
                    Name = "Bake Pytong",
                    CompanyId = 2,
                },
                new Outlet
                {
                    Id = 12,
                    Name = "Love Noodles",
                    CompanyId = 2,
                }
            );
        }
    }
}
