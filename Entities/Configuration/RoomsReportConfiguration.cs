using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace Entities.Configuration
{
    class RoomsReportConfiguration : IEntityTypeConfiguration<RoomReport>
    {
        public void Configure(EntityTypeBuilder<RoomReport> builder)
        {
            builder.HasIndex(e => e.LoggerId, "IX_RoomsReports_LoggerId");

            builder.HasIndex(e => e.RoomTypeId, "IX_RoomsReports_RoomTypeId");

            builder.Property(e => e.Date).HasColumnType("date");

            builder.HasOne(d => d.Logger)
                .WithMany(p => p.RoomReports)
                .HasForeignKey(d => d.LoggerId)
                .HasConstraintName("FK_Users_RommsReports");

            builder.HasOne(d => d.RoomType)
                .WithMany(p => p.RoomReports)
                .HasForeignKey(d => d.RoomTypeId)
                .HasConstraintName("FK_RoomTypes_RoomsReports");

            ////Seeding data. Requires seeded data in roomtypes and users/loggers
            builder.HasData
            (
                new RoomReport
                {
                    Id = 1,
                    NewRoomNights = 5,
                    TodaysRevenuePickup = 31200,
                    OtherRevenue = 5000,
                    IsPublicHoliday = false,
                    Notes = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et " +
                                "dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco",
                    Date = DateTime.Now,
                    RoomTypeId = 1,
                    LoggerId = "b0b22e53-3ad2-4a0a-9e58-aa0a70a5a157"
                },
                new RoomReport
                {
                    Id = 2,
                    NewRoomNights = 2,
                    TodaysRevenuePickup = 29300,
                    OtherRevenue = 4000,
                    IsPublicHoliday = false,
                    Notes = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et " +
                            "dolore magna aliqua. Ut enim ad minim veniam",
                    Date = DateTime.Now,
                    RoomTypeId = 2,
                    LoggerId = "b0b22e53-3ad2-4a0a-9e58-aa0a70a5a157"
                },
                new RoomReport
                {
                    Id = 3,
                    NewRoomNights = 4,
                    TodaysRevenuePickup = 39400,
                    OtherRevenue = 6000,
                    IsPublicHoliday = false,
                    Notes = "Lorem ipsum dolor sit amet",
                    Date = DateTime.Now,
                    RoomTypeId = 3,
                    LoggerId = "b0b22e53-3ad2-4a0a-9e58-aa0a70a5a157"
                },
                 new RoomReport
                 {
                     Id = 4,
                     NewRoomNights = 4,
                     TodaysRevenuePickup = 39400,
                     OtherRevenue = 6000,
                     IsPublicHoliday = false,
                     Notes = "Lorem ipsum dolor sit amet",
                     Date = DateTime.Now,
                     RoomTypeId = 4,
                     LoggerId = "b0b22e53-3ad2-4a0a-9e58-aa0a70a5a157"
                 },
                  new RoomReport
                  {
                      Id = 5,
                      NewRoomNights = 4,
                      TodaysRevenuePickup = 39400,
                      OtherRevenue = 6000,
                      IsPublicHoliday = false,
                      Notes = "Lorem ipsum dolor sit amet",
                      Date = DateTime.Now,
                      RoomTypeId = 5,
                      LoggerId = "35947f01-393b-442c-b815-d6d9f7d4b81e"
                  },
                   new RoomReport
                   {
                       Id = 6,
                       NewRoomNights = 4,
                       TodaysRevenuePickup = 39400,
                       OtherRevenue = 6000,
                       IsPublicHoliday = false,
                       Notes = "Lorem ipsum dolor sit amet",
                       Date = DateTime.Now,
                       RoomTypeId = 6,
                       LoggerId = "35947f01-393b-442c-b815-d6d9f7d4b81e"
                   },
                    new RoomReport
                    {
                        Id = 7,
                        NewRoomNights = 4,
                        TodaysRevenuePickup = 39400,
                        OtherRevenue = 6000,
                        IsPublicHoliday = false,
                        Notes = "Lorem ipsum dolor sit amet",
                        Date = DateTime.Now,
                        RoomTypeId = 7,
                        LoggerId = "35947f01-393b-442c-b815-d6d9f7d4b81e"
                    },
                     new RoomReport
                     {
                         Id = 8,
                         NewRoomNights = 4,
                         TodaysRevenuePickup = 39400,
                         OtherRevenue = 6000,
                         IsPublicHoliday = false,
                         Notes = "Lorem ipsum dolor sit amet",
                         Date = DateTime.Now.AddDays(-1),
                         RoomTypeId = 1,
                         LoggerId = "b0b22e53-3ad2-4a0a-9e58-aa0a70a5a157"
                     },
                      new RoomReport
                      {
                          Id = 9,
                          NewRoomNights = 4,
                          TodaysRevenuePickup = 39400,
                          OtherRevenue = 6000,
                          IsPublicHoliday = false,
                          Notes = "Lorem ipsum dolor sit amet",
                          Date = DateTime.Now.AddDays(-1),
                          RoomTypeId = 2,
                          LoggerId = "b0b22e53-3ad2-4a0a-9e58-aa0a70a5a157"
                      },
                       new RoomReport
                       {
                           Id = 10,
                           NewRoomNights = 4,
                           TodaysRevenuePickup = 39400,
                           OtherRevenue = 6000,
                           IsPublicHoliday = false,
                           Notes = "Lorem ipsum dolor sit amet",
                           Date = DateTime.Now.AddDays(-1),
                           RoomTypeId = 3,
                           LoggerId = "b0b22e53-3ad2-4a0a-9e58-aa0a70a5a157"
                       },
                        new RoomReport
                        {
                            Id = 11,
                            NewRoomNights = 4,
                            TodaysRevenuePickup = 39400,
                            OtherRevenue = 6000,
                            IsPublicHoliday = false,
                            Notes = "Lorem ipsum dolor sit amet",
                            Date = DateTime.Now.AddDays(-1),
                            RoomTypeId = 4,
                            LoggerId = "b0b22e53-3ad2-4a0a-9e58-aa0a70a5a157"
                        },
                         new RoomReport
                         {
                             Id = 12,
                             NewRoomNights = 4,
                             TodaysRevenuePickup = 39400,
                             OtherRevenue = 6000,
                             IsPublicHoliday = false,
                             Notes = "Lorem ipsum dolor sit amet",
                             Date = DateTime.Now.AddDays(-1),
                             RoomTypeId = 5,
                             LoggerId = "35947f01-393b-442c-b815-d6d9f7d4b81e"
                         },
                          new RoomReport
                          {
                              Id = 13,
                              NewRoomNights = 4,
                              TodaysRevenuePickup = 39400,
                              OtherRevenue = 6000,
                              IsPublicHoliday = false,
                              Notes = "Lorem ipsum dolor sit amet",
                              Date = DateTime.Now.AddDays(-1),
                              RoomTypeId = 6,
                              LoggerId = "35947f01-393b-442c-b815-d6d9f7d4b81e"
                          },
                           new RoomReport
                           {
                               Id = 14,
                               NewRoomNights = 4,
                               TodaysRevenuePickup = 39400,
                               OtherRevenue = 6000,
                               IsPublicHoliday = false,
                               Notes = "Lorem ipsum dolor sit amet",
                               Date = DateTime.Now.AddDays(-1),
                               RoomTypeId = 7,
                               LoggerId = "35947f01-393b-442c-b815-d6d9f7d4b81e"
                           },
                            new RoomReport
                            {
                                Id = 15,
                                NewRoomNights = 3,
                                TodaysRevenuePickup = 22700,
                                OtherRevenue = 4600,
                                IsPublicHoliday = false,
                                Notes = "Lorem ipsum dolor sit amet",
                                Date = DateTime.Now.AddDays(-2),
                                RoomTypeId = 1,
                                LoggerId = "b0b22e53-3ad2-4a0a-9e58-aa0a70a5a157"
                            },
                             new RoomReport
                             {
                                 Id = 16,
                                 NewRoomNights = 4,
                                 TodaysRevenuePickup = 39400,
                                 OtherRevenue = 6000,
                                 IsPublicHoliday = false,
                                 Notes = "Lorem ipsum dolor sit amet",
                                 Date = DateTime.Now.AddDays(-2),
                                 RoomTypeId = 2,
                                 LoggerId = "b0b22e53-3ad2-4a0a-9e58-aa0a70a5a157"
                             },
                              new RoomReport
                              {
                                  Id = 17,
                                  NewRoomNights = 4,
                                  TodaysRevenuePickup = 39400,
                                  OtherRevenue = 6000,
                                  IsPublicHoliday = false,
                                  Notes = "Lorem ipsum dolor sit amet",
                                  Date = DateTime.Now.AddDays(-2),
                                  RoomTypeId = 3,
                                  LoggerId = "b0b22e53-3ad2-4a0a-9e58-aa0a70a5a157"
                              },
                               new RoomReport
                               {
                                   Id = 18,
                                   NewRoomNights = 4,
                                   TodaysRevenuePickup = 39400,
                                   OtherRevenue = 6000,
                                   IsPublicHoliday = false,
                                   Notes = "Lorem ipsum dolor sit amet",
                                   Date = DateTime.Now.AddDays(-2),
                                   RoomTypeId = 4,
                                   LoggerId = "b0b22e53-3ad2-4a0a-9e58-aa0a70a5a157"
                               },
                                new RoomReport
                                {
                                    Id = 19,
                                    NewRoomNights = 4,
                                    TodaysRevenuePickup = 39400,
                                    OtherRevenue = 6000,
                                    IsPublicHoliday = false,
                                    Notes = "Lorem ipsum dolor sit amet",
                                    Date = DateTime.Now.AddDays(-2),
                                    RoomTypeId = 5,
                                    LoggerId = "35947f01-393b-442c-b815-d6d9f7d4b81e"
                                },
                                 new RoomReport
                                 {
                                     Id = 20,
                                     NewRoomNights = 4,
                                     TodaysRevenuePickup = 39400,
                                     OtherRevenue = 6000,
                                     IsPublicHoliday = false,
                                     Notes = "Lorem ipsum dolor sit amet",
                                     Date = DateTime.Now.AddDays(-2),
                                     RoomTypeId = 6,
                                     LoggerId = "35947f01-393b-442c-b815-d6d9f7d4b81e"
                                 },
                                  new RoomReport
                                  {
                                      Id = 21,
                                      NewRoomNights = 4,
                                      TodaysRevenuePickup = 39400,
                                      OtherRevenue = 6000,
                                      IsPublicHoliday = false,
                                      Notes = "Lorem ipsum dolor sit amet",
                                      Date = DateTime.Now.AddDays(-2),
                                      RoomTypeId = 7,
                                      LoggerId = "35947f01-393b-442c-b815-d6d9f7d4b81e"
                                  }

            );
        }
    }
}
