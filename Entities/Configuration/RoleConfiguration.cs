using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Configuration
{
    class RoleConfiguration : IEntityTypeConfiguration<IdentityRole>
    {
        public void Configure(EntityTypeBuilder<IdentityRole> builder)
        {
            builder.HasData(new IdentityRole
            {
                Id = "d83846e6-7a92-41d6-8c6e-9394df0b35f3",
                Name = "SuperAdmin",
                NormalizedName = "SUPERADMIN"
            }, new IdentityRole
            {
                Id = "9c00337f-a26e-4bf4-9602-2996d15beb2d",
                Name = "Admin",
                NormalizedName = "ADMIN"
            }, new IdentityRole
            {
                Id = "32be7cac-c2b5-40af-842b-d7a14891aed7",
                Name = "Basic",
                NormalizedName = "BASIC"
            }
            );
        }
    }
}
