using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Entities.Configuration
{
    class CompanyUserConfiguration : IEntityTypeConfiguration<CompanyUser>
    {
        public void Configure(EntityTypeBuilder<CompanyUser> builder)
        {
            builder.HasKey(e => new { e.UserId, e.CompanyId });

            builder.ToTable("CompanyUser");

            builder.HasIndex(e => e.CompanyId, "IX_CompanyUser_CompanyId");

            builder.HasOne(d => d.Company)
                .WithMany(p => p.CompanyUsers)
                .HasForeignKey(d => d.CompanyId)
                .HasConstraintName("FK_Companies_CompanyUser");

            builder.HasOne(d => d.User)
                .WithMany(p => p.CompanyUsers)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_Users_CompanyUser");

            builder.HasData
            (
                new CompanyUser
                {
                    UserId = "7c8a42a1-e82c-4e2a-b944-67aec243d2fb",
                    CompanyId = 1
                },
                new CompanyUser
                {
                    UserId = "7c8a42a1-e82c-4e2a-b944-67aec243d2fb",
                    CompanyId = 2
                },
                new CompanyUser
                {
                    UserId = "7c8a42a1-e82c-4e2a-b944-67aec243d2fb",
                    CompanyId = 3
                },
                new CompanyUser
                {
                    UserId = "68a89c2e-ac33-4e56-9b03-a9ef49d28995",
                    CompanyId = 1
                },
                new CompanyUser
                {
                    UserId = "68a89c2e-ac33-4e56-9b03-a9ef49d28995",
                    CompanyId = 2
                }
            );
        }
    }
}
