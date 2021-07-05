using AutoMapper;
using TwinPalmsKPI.ActionFilters;
using Contracts;
using Entities.DataTransferObjects;
using Entities.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using TwinPalmsKPI.Helpers;

namespace TwinPalmsKPI.Controllers
{
    //[Route("api/[controller]")]
    [Route("api/")]
    [ApiController]
    public class OutletsFbReportsController : ControllerBase
    {
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment env;

        public OutletsFbReportsController(IRepositoryManager repository, ILoggerManager logger, IMapper mapper, IWebHostEnvironment environment)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
            env = environment;
        }

        // ****************************************** GetOutletsFbReports ****************************************************
        /// <summary>
        /// Gets All FbReports by many Outlet Ids between two Dates
        /// </summary>
        /// <remarks>
        /// Gets all FbReports for many outlets between fromDate and toDate, including toDate
        /// 
        /// For example: to get the reports from only 2021-05-04
        /// 
        ///     fromDate = 2021-12-02
        ///     toDate = 2021-12-02
        ///     
        /// </remarks>     
        [HttpGet("/outlets/fbReports", Name = "OutletsFbReportsByIdAndDate")]
        public async Task<IActionResult> GetOutletsFbReports( [FromQuery] int[] outletIds, DateTime fromDate, DateTime toDate)
        {
            // Reports filed before 5am are treated as fbreport for the day before.
            // Request for toDate are reports including that date.
            var fbrStart = fromDate.AddHours(5);
            var fbrEnd = toDate.AddHours(5).AddDays(1);

            StringBuilder sbOutletIds = new StringBuilder();

            int outletIdCounter = 0;

            foreach (var oi in outletIds)
            {
                outletIdCounter++;

                if (await _repository.Outlet.GetOutletAsync(oi, false) == null)
                {
                    _logger.LogInfo($"Outlet with id {oi} doesn't exist in the database.");
                    return NotFound();
                }

                sbOutletIds.Append($"{oi}");

                if (outletIdCounter < outletIds.Count())
                {
                    sbOutletIds.Append($", ");
                }
            }

            var outletFbReports = await _repository.FbReport.GetAllOutletFbReportsForOutlets(outletIds, fbrStart, fbrEnd, trackChanges: false);

            if (outletFbReports.Count() == 0)
            {
                _logger.LogInfo($"No reports for Outlet with ids {sbOutletIds.ToString()} found in the database between dates {fromDate} and {toDate}.");
                return NotFound();
            }

            var outletFbReportsToReturn = outletFbReports.Select(o => new
            {
                Tables = o.Tables,
                Food = o.Food,
                Beverage = o.Beverage,
                OtherIncome = o.OtherIncome,
                Date = o.Date,
                GuestsFromHotelTP = o.GuestsFromHotelTP,
                GuestsFromHotelTM = o.GuestsFromHotelTM,
                GuestsFromOutsideHotel = o.GuestsFromOutsideHotel,
                IsPublicHoliday = o.IsPublicHoliday,
                EventNotes = o.EventNotes,
                GSourceOfBusinessNotes = o.GSourceOfBusinessNotes,
                OutletId = o.OutletId,
                UserId = o.UserId,
                LocalEventId = o.LocalEventId,
                GuestSourceOfBusinesses = o.FbReportGuestSourceOfBusinesses.Select(f => f.GuestSourceOfBusiness).ToList(),
                GsobNrOfGuest = o.FbReportGuestSourceOfBusinesses.Select(f => f.GsobNrOfGuests).ToList(),
                Weathers = o.WeatherFbReports.Select(w => w.Weather).ToList()
            }).ToArray();

            return Ok(outletFbReportsToReturn);
        }

        // ****************************************** GetOutletsOverview ****************************************************
        /// <summary>
        /// Gets YTD, MTD and yesterdays revenue
        /// </summary>
        /// <remarks>
        /// Gets Year to Date, Month to Date and yesterdays revenue based on when the request came
        /// </remarks>     
        [HttpGet("/outlets/overview", Name = "OutletsOverview")]
        public async Task<IActionResult> GetOutletsOverview()
        {
            // Reports filed before 5am are treated as fbreport for the day before.
            // Request for toDate are reports including that date.
            DateTime now = DateTime.UtcNow;
            DateTime today = new DateTime(now.Year, now.Month, now.Day, 0, 0, 0);
            DateTime yesterday = today.AddDays(-1);
            DateTime startOfYear = new DateTime(now.Year, 1, 1, 0, 0, 0);
            DateTime startOfMonth = new DateTime(now.Year, now.Month, 1, 0, 0, 0);

            StringBuilder sbOutletIds = new StringBuilder(); // This is used for error reporting

            int outletIdCounter = 0;

            // Getting outlet ids from DB
            List<int> outletIdsList = new List<int>();

            List<Outlet> outletReports = (List<Outlet>)await _repository.Outlet.GetAllOutletsAsync(trackChanges: false);

            foreach (var or in outletReports)
            {
                outletIdsList.Add(or.Id);
            }

            int[] outletIds = outletIdsList.ToArray();

            // Adding outlet ids to sbOutletIds for error reporting
            foreach (var oi in outletIds)
            {
                outletIdCounter++;

                sbOutletIds.Append($"{oi}");

                if (outletIdCounter < outletIds.Count())
                {
                    sbOutletIds.Append($", ");
                }
            }


            // YTD = Revenue YearToDate?
            List<FbReport> YTDOutletFbReports;

            if (env.IsDevelopment())
            {
                YTDOutletFbReports = (List<FbReport>)await _repository.FbReport.GetAllOutletFbReportsForOutlets(outletIds, startOfYear, new DateTime(now.Year, 12, 31, 23, 23, 59), trackChanges: false); // TODO change back to today
            }
            else
            {
                YTDOutletFbReports = (List<FbReport>)await _repository.FbReport.GetAllOutletFbReportsForOutlets(outletIds, startOfYear, today, trackChanges: false); // TODO change back to today
            }

            if (YTDOutletFbReports.Count() == 0)
            {
                _logger.LogInfo($"No reports for Outlet with ids {sbOutletIds.ToString()} found in the database between dates {startOfYear} and {today}.");
                return NotFound();
            }

            var YTDs = new Dictionary<int, int?>();

            foreach (var yofbr in YTDOutletFbReports)
            {
                if (!YTDs.ContainsKey(yofbr.OutletId))
                {
                    YTDs.Add(yofbr.OutletId, 0);
                }

                YTDs[yofbr.OutletId] += yofbr.Food;
                YTDs[yofbr.OutletId] += yofbr.Beverage;
                YTDs[yofbr.OutletId] += yofbr.OtherIncome;
            }


            // MTD = Revenue MonthToDate?
            var MTDOutletFbReports = await _repository.FbReport.GetAllOutletFbReportsForOutlets(outletIds, startOfMonth, today /*new DateTime(now.Year, 12, 31, 23, 23, 59)*/, trackChanges: false); // TODO change back to today

            if (MTDOutletFbReports.Count() == 0)
            {
                _logger.LogInfo($"No reports for Outlet with ids {sbOutletIds.ToString()} found in the database between dates {startOfMonth} and {today}.");

                if (!env.IsDevelopment())
                {
                    return NotFound();
                }
            }

            var MTDs = new Dictionary<int, int?>();

            foreach (var mofbr in MTDOutletFbReports)
            {
                if (!MTDs.ContainsKey(mofbr.OutletId))
                {
                    MTDs.Add(mofbr.OutletId, 0);
                }

                MTDs[mofbr.OutletId] += mofbr.Food;
                MTDs[mofbr.OutletId] += mofbr.Beverage;
                MTDs[mofbr.OutletId] += mofbr.OtherIncome;
            }


            // Yesterdays revenue
            var YesterdayOutletFbReports = await _repository.FbReport.GetAllOutletFbReportsForOutlets(outletIds, yesterday, today /*new DateTime(now.Year, 12, 31, 23, 23, 59)*/, trackChanges: false); // TODO change back to today

            if (YesterdayOutletFbReports.Count() == 0)
            {
                _logger.LogInfo($"No reports for Outlet with ids {sbOutletIds.ToString()} found in the database between dates {yesterday} and {today}.");

                if (!env.IsDevelopment())
                {
                    return NotFound();
                }
            }

            var yesterdaysRevs = new Dictionary<int, int?>();

            foreach (var ydofbr in YesterdayOutletFbReports)
            {
                if (!yesterdaysRevs.ContainsKey(ydofbr.OutletId))
                {
                    yesterdaysRevs.Add(ydofbr.OutletId, 0);
                }

                yesterdaysRevs[ydofbr.OutletId] += ydofbr.Food;
                yesterdaysRevs[ydofbr.OutletId] += ydofbr.Beverage;
                yesterdaysRevs[ydofbr.OutletId] += ydofbr.OtherIncome;
            }



            //MonthlyRevenues

            //************************************ NYTT FÖRSÖK START ************************************
            //
            // example:
            //      obj = {
            //          outletId1 : {jan: 100, feb: 100, mar: 100, apr: 11}...
            //

            // loopa outlets
            for (int outletCounter = 1; outletCounter <= outletIdCounter; outletCounter++)
            {
                MonthlyRevDto monthlyRev = new MonthlyRevDto()
                {
                    Revenues = new List<int[][]>() 
                };

                int? rev1Month;
                
                // loopa månader{
                for (int monthCounter = 0; monthCounter < 12; monthCounter++)
                {
                    int[] outlId = new int[1];
                    outlId[0] = outletCounter;
                        
                    // Få data från DB
                    var MonthlyRevsFromDB = await _repository.FbReport.GetAllOutletFbReportsForOutlets(
                        outlId,
                        new DateTime(now.Year, (monthCounter + 1), 1, 0, 0, 0).AddHours(5.1),
                        new DateTime(now.Year, (monthCounter + 1), DateTime.DaysInMonth(now.Year, monthCounter + 1), 23, 59, 59).AddHours(5.1),
                        trackChanges: false
                    );

                    rev1Month = 0;
                    monthlyRev.OutletId = outletCounter;
                    
                    // Loop through reports. Räkna ut rev för hela månaden (en for loop till?)
                    foreach (var mr in MonthlyRevsFromDB)
                    {
                        rev1Month += mr.Food;
                        rev1Month += mr.Beverage;
                        rev1Month += mr.OtherIncome;
                    }

                    int[][] mRevTempArray = new int[1][];
                    mRevTempArray[0] = new int[2];

                    mRevTempArray[0][0] = monthCounter;
                    mRevTempArray[0][1] = (int)rev1Month;

                    monthlyRev.Revenues.Add(mRevTempArray);
                }
            }

            //************************************ NYTT FÖRSÖK END ************************************


                List<MonthlyRevenue> monthlyRevenues = new List<MonthlyRevenue>();
            int[,] revs1Outlet1Month = new int[12, 2]; // [x, 0] = outlet id, [x, 1] = revenue

            // [x][0] = month, [x][1] = outlet, [x][2] = revenue that month
            //int[][] revsAllOutlets1Month = new int[outletIdCounter][];

            //for (int i = 0; i < outletIdCounter; i++)
            //{
            //    revsAllOutlets1Month[i] = new int[3];
            //}


            int[] tempOutletId = { 0 };

            for (int outletCounter = 1; outletCounter <= outletIdCounter; outletCounter++)
            {
                for (int monthCounter = 0; monthCounter < 12; monthCounter++)
                {
                    tempOutletId[0] = outletCounter;

                    var MonthlyOutletFbReports = await _repository.FbReport.GetAllOutletFbReportsForOutlets(
                        tempOutletId,
                        new DateTime(now.Year, (monthCounter + 1), 1, 0, 0, 0),
                        new DateTime(now.Year, (monthCounter + 1), DateTime.DaysInMonth(now.Year, monthCounter + 1), 23, 59, 59).AddHours(5.1),
                        trackChanges: false
                    );

                    revs1Outlet1Month[monthCounter, 0] = (int)tempOutletId.GetValue(0);

                    // Adding revenue for all the month
                    foreach (var mor in MonthlyOutletFbReports)
                    {
                        revs1Outlet1Month[monthCounter, 1] += (int)mor.Food;
                        revs1Outlet1Month[monthCounter, 1] += (int)mor.Beverage;
                        revs1Outlet1Month[monthCounter, 1] += (int)mor.OtherIncome;
                    }

                    MonthlyRevenue monthlyRevenue = new MonthlyRevenue();
                    monthlyRevenue.Month = monthCounter + 1;
                    monthlyRevenue.OutletId = outletCounter;
                    monthlyRevenue.Revenue = revs1Outlet1Month[monthCounter, 1];
                    monthlyRevenues.Add(monthlyRevenue);

                    //revsAllOutlets1Month[outletCounter - 1][0] = monthCounter;
                    //revsAllOutlets1Month[outletCounter - 1][1] = revs1Outlet1Month[outletCounter - 1, 0];
                    //revsAllOutlets1Month[outletCounter - 1][2] = revs1Outlet1Month[outletCounter - 1, 1];
                }
            }

            // Adding to dto for return
            List<RevenueOverViewDto> revs = new List<RevenueOverViewDto>();
            List<int> revRevs = new List<int>();

            //foreach (var mr in monthlyRevenues)
            //{
            int revTemp = 0;

            for (int month = 1; month <= 12; month++)
            {
                     
                for (int outlId = 1; outlId <= outletIdCounter; outlId++)
                {
                    var revenue = new RevenueOverViewDto();
                    //revenue.Month = month;
                    //revenue.OutletId = outlId;

                    foreach (var mr in monthlyRevenues)
                    {
                        if (mr.OutletId == outlId && mr.Month == month)
                        {
                            revRevs.Add(mr.Revenue);
                        }

                        //revenue.Revenues = revRevs.ToList<int>();
                        revTemp = 0;
                    }
                
                    revs.Add(revenue);
                }
            }

            return null;
        }

        //        {
        //            Month = mr.Month,
        //            OutletId = mr.OutletId,
        //            Revenue = mr.Revenue
        //        };

        //        revs.Add(revenue);
        //    //}

        //    return Ok(revs);
        //}
    }
}