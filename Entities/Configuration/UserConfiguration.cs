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
    class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasData(new User
            {
                Id = "68a89c2e-ac33-4e56-9b03-a9ef49d28995",
                FirstName = "Super",
                LastName = "Admin",
                UserName = "superadmin",
                Email = "superadmin@twinpalms",
                NormalizedEmail = "SUPERADMIN@TWINPALMS",
                EmailConfirmed = true,

            }, new User
            {
                Id = "7c8a42a1-e82c-4e2a-b944-67aec243d2fb",
                FirstName = "Admin",
                LastName = "Admin",
                UserName = "admin",
                Email = "admin@twinpalms",
                NormalizedEmail = "ADMIN@TWINPALMS",
                EmailConfirmed = true

            }, new User
            {
                Id = "b0b22e53-3ad2-4a0a-9e58-aa0a70a5a157",
                FirstName = "Basic1",
                LastName = "Basic1",
                UserName = "basic1",
                Email = "basic1@twinpalms",
                NormalizedEmail = "BASIC1@TWINPALMS",
                EmailConfirmed = true

            }, new User
            {
                Id = "35947f01-393b-442c-b815-d6d9f7d4b81e",
                FirstName = "Basic2",
                LastName = "Basic2",
                UserName = "basic2",
                Email = "basic2@twinpalms",
                NormalizedEmail  = "BASIC2@TWINPALMS",
                EmailConfirmed = true
            }); 


        }
    }
}
