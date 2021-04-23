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
                    Name = "Catch Beach Club",
                    CompanyId = 1
                },
                new Outlet
                {
                    Name = "Catch Junior",
                    CompanyId = 1,
                },
                new Outlet
                {
                    Name = "Catch Junior",
                    CompanyId = 1,
                },
                new Outlet
                {
                    Name = "Wagyu Steakhouse",
                    CompanyId = 1,
                },
                new Outlet
                {
                    Name = "Palm Seaside",
                    CompanyId = 1,
                },
                new Outlet
                {
                    Name = "Oriental Spoon",
                    CompanyId = 1,
                },
                new Outlet
                {
                    Name = "HQ Beach Lounge",
                    CompanyId = 1,
                },
                new Outlet
                {
                    Name = "Shimmer",
                    CompanyId = 2,
                },
                new Outlet
                {
                    Name = "Bake Laguna",
                    CompanyId = 2,
                },
                new Outlet
                {
                    Name = "Bake BIS",
                    CompanyId = 2,
                },
                new Outlet
                {
                    Name = "Bake Turtle Village",
                    CompanyId = 2,
                },
                new Outlet
                {
                    Name = "Bake Pytong",
                    CompanyId = 1,
                },
                new Outlet
                {
                    Name = "Love Noodles",
                    CompanyId = 2,
                }
            );
        }
    }
}
