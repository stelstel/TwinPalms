using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;


namespace Entities.Configuration
{
    public class FbReportConfiguration : IEntityTypeConfiguration<FbReport>
    {
        public void Configure(EntityTypeBuilder<FbReport> builder)
        {
            builder.HasData
            (
                new FbReport
                {
                  Id = 1,
                  Tables = 1,
                  Food = 10000,
                  Beverage = 20000,
                  OtherIncome = 5000,
                  GuestsFromHotel = 15,
                  GuestsFromOutsideHotel = 100,
                  IsPublicHoliday = false,
                  Notes = "Lorem Ipsum",
                  Date = DateTime.Now,
                  OutletId = 1,
                  UserId = 1//,
                  //LocalEventId = 1
                }
            );
        }
    }
}
