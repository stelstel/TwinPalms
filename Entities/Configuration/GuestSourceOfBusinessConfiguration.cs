using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace Entities.Configuration
{
    class GuestSourceOfBusinessConfiguration : IEntityTypeConfiguration<GuestSourceOfBusiness>
    {
        public void Configure(EntityTypeBuilder<GuestSourceOfBusiness> builder)
        {
            builder.HasData(
                new GuestSourceOfBusiness
                {
                    Id = 1,
                    SourceOfBusiness = "Hotel Website"
                },
                new GuestSourceOfBusiness
                {
                    Id = 2,
                    SourceOfBusiness = "Hungry Hub"
                },
                new GuestSourceOfBusiness
                {
                    Id = 3,
                    SourceOfBusiness = "Facebook referral"
                },
                new GuestSourceOfBusiness
                {
                    Id = 4,
                    SourceOfBusiness = "Google search"
                },
                new GuestSourceOfBusiness
                {
                    Id = 5,
                    SourceOfBusiness = "Instagram referral"
                },
                new GuestSourceOfBusiness
                {
                    Id = 6,
                    SourceOfBusiness = "Hotel referral"
                },
                new GuestSourceOfBusiness
                {
                    Id = 7,
                    SourceOfBusiness = "Other Hotel referral"
                },
                new GuestSourceOfBusiness
                {
                    Id = 8,
                    SourceOfBusiness = "Agent referral"
                },
                new GuestSourceOfBusiness
                {
                    Id = 9,
                    SourceOfBusiness = "Walk in"
                },
                new GuestSourceOfBusiness
                {
                    Id = 10,
                    SourceOfBusiness = "Other"
                }
            );
        }

    }
}
