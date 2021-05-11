using Entities.Models;
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
    class RoleConfiguration : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.HasMany(e => e.UserRoles)
                .WithOne(e => e.Role)
                .HasForeignKey(ur => ur.RoleId)
                .IsRequired();

            builder.HasData(
                new Role
                {
                    Id = "d83846e6-7a92-41d6-8c6e-9394df0b35f3",
                    Name = "SuperAdmin",
                    NormalizedName = "SUPERADMIN"
                }, new Role
                {
                    Id = "9c00337f-a26e-4bf4-9602-2996d15beb2d",
                    Name = "Admin",
                    NormalizedName = "ADMIN"
                }, new Role
                {
                    Id = "32be7cac-c2b5-40af-842b-d7a14891aed7",
                    Name = "Basic",
                    NormalizedName = "BASIC"
                }
            );
        }
    }
}
