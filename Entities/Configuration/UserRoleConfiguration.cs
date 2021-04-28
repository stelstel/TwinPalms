using Entities.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Entities.Configuration
{
    class UserRoleConfiguration : IEntityTypeConfiguration<UserRole>
    {


        public void Configure(EntityTypeBuilder<UserRole> builder)
        {
            builder.HasData(new UserRole
            {
                UserId = "b0b22e53-3ad2-4a0a-9e58-aa0a70a5a157",
                RoleId = "32be7cac-c2b5-40af-842b-d7a14891aed7"

            }, new UserRole
            {
                UserId = "35947f01-393b-442c-b815-d6d9f7d4b81e",
                RoleId = "32be7cac-c2b5-40af-842b-d7a14891aed7"

            }, new UserRole
            {
                UserId = "7c8a42a1-e82c-4e2a-b944-67aec243d2fb",
                RoleId = "9c00337f-a26e-4bf4-9602-2996d15beb2d"
            },
            new UserRole
            {
                UserId = "7c8a42a1-e82c-4e2a-b944-67aec243d2fb",
                RoleId = "32be7cac-c2b5-40af-842b-d7a14891aed7"
            },
            new UserRole
            {
                UserId = "68a89c2e-ac33-4e56-9b03-a9ef49d28995",
                RoleId = "32be7cac-c2b5-40af-842b-d7a14891aed7"
            },
            new UserRole
            {
                UserId = "68a89c2e-ac33-4e56-9b03-a9ef49d28995",
                RoleId = "9c00337f-a26e-4bf4-9602-2996d15beb2d"
            },
            new UserRole
            {
                UserId = "68a89c2e-ac33-4e56-9b03-a9ef49d28995",
                RoleId = "d83846e6-7a92-41d6-8c6e-9394df0b35f3"
            }
            );

            
        }
    }
}
