using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;


namespace Entities.Configuration
{
    public class CruiseCompanyConfiguration : IEntityTypeConfiguration<CruiseCompany>
    {
        public void Configure(EntityTypeBuilder<CruiseCompany> builder)
        {
            builder.HasData
            (
                new CruiseCompany
                {
                    Id = 1,
                    Name = "Andaman Cruises",
                    CompanyId = 2
                }
            );
        }
    }
}
