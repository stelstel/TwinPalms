using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;


namespace Entities.Configuration
{
    public class CruiseShipConfiguration : IEntityTypeConfiguration<CruiseShip>
    {
        public void Configure(EntityTypeBuilder<CruiseShip> builder)
        {
            builder.HasData
            (
                new CruiseShip
                {
                    Id = 1,
                    Name = "Olympia",
                    CruiseCompanyId = 1
                },
                new CruiseShip
                {
                    Id = 2,
                    Name = "Chartered",
                    CruiseCompanyId = 1
                }
            );
        }
    }
}
