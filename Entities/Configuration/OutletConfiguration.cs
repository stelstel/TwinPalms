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
                    Name = "Catch Junior",
                    CompanyId = 1,
                },
                new Outlet
                {
                    Id = 4,
                    Name = "Wagyu Steakhouse",
                    CompanyId = 1,
                },
                new Outlet
                {
                    Id = 5,
                    Name = "Palm Seaside",
                    CompanyId = 2,
                },
                new Outlet
                {
                    Id = 6,
                    Name = "Oriental Spoon",
                    CompanyId = 2,
                },
                new Outlet
                {
                    Id = 7,
                    Name = "HQ Beach Lounge",
                    CompanyId = 2,
                },
                new Outlet
                {
                    Id = 8,
                    Name = "Shimmer",
                    CompanyId = 3,
                },
                new Outlet
                {
                    Id = 9,
                    Name = "Bake Laguna",
                    CompanyId = 3,
                },
                new Outlet
                {
                    Id = 10,
                    Name = "Bake BIS",
                    CompanyId = 3,
                },
                new Outlet
                {
                    Id = 11,
                    Name = "Bake Turtle Village",
                    CompanyId = 4,
                },
                new Outlet
                {
                    Id = 12,
                    Name = "Bake Pytong",
                    CompanyId = 4,
                },
                new Outlet
                {
                    Id = 13,
                    Name = "Love Noodles",
                    CompanyId = 4,
                }
            );
        }
    }
}
