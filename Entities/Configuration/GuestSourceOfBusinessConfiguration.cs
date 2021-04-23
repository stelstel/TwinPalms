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
                    SourceOfBusiness = "Hotel Website"
                },
                new GuestSourceOfBusiness
                {
                    SourceOfBusiness = "Hungry Hub"
                },
                new GuestSourceOfBusiness
                {
                    SourceOfBusiness = "Facebook referral"
                },
                new GuestSourceOfBusiness
                {
                    SourceOfBusiness = "Google search"
                },
                new GuestSourceOfBusiness
                {
                    SourceOfBusiness = "Instagram referral"
                },
                new GuestSourceOfBusiness
                {
                    SourceOfBusiness = "Hotel referral"
                },
                new GuestSourceOfBusiness
                {
                    SourceOfBusiness = "Other Hotel referral"
                },
                new GuestSourceOfBusiness
                {
                    SourceOfBusiness = "Agent referral"
                },
                new GuestSourceOfBusiness
                {
                    SourceOfBusiness = "Walk in"
                },
                new GuestSourceOfBusiness
                {
                    SourceOfBusiness = "Other"
                }
            );
        }

    }
}
