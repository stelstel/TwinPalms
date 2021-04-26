using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;


namespace Entities.Configuration
{
    public class CompanyConfiguration : IEntityTypeConfiguration<Company>
    {
        public void Configure(EntityTypeBuilder<Company> builder)
        {
            builder.HasData
            (
                new Company
                {
                    Id = -1,
                    Name = "TPS"
                },
                new Company
                {
                    Id = -2,
                    Name = "TPMS"
                },
                new Company
                {
                    Id = -3,
                    Name = "TPMA"
                },
                new Company
                {
                    Id = -4,
                    Name = "PPT"
                }
            );
        }
    }
}
