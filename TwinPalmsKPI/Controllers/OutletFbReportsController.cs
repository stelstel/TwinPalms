using AutoMapper;
using Contracts;
using Entities.DataTransferObjects;
using Entities.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwinPalmsKPI.Helpers;

namespace TwinPalmsKPI.Controllers
{
    [Route("api/")]
    [ApiController]
    public class OutletsFbReportsController : ControllerBase
    {
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment env;
        private readonly IConfiguration config;

        public OutletsFbReportsController(IRepositoryManager repository, ILoggerManager logger,
            IMapper mapper, IWebHostEnvironment environment, IConfiguration configuration)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
            env = environment;
            config = configuration;
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
                Notes = o.Notes,
                OutletId = o.OutletId,
                UserId = o.UserId,
                LocalEventId = o.LocalEventId,
                Imagepath = o.ImagePath,
                GuestSourceOfBusinesses = o.FbReportGuestSourceOfBusinesses.Select(f => f.GuestSourceOfBusiness).ToList(),
                GsobNrOfGuest = o.FbReportGuestSourceOfBusinesses.Select(f => f.GsobNrOfGuests).ToList(),
                Weathers = o.WeatherFbReports.Select(w => w.Weather).ToList()
            }).ToArray();

            return Ok(outletFbReportsToReturn);
        }

        // ****************************************** GetOutletsOverview ****************************************************
        /// <summary>
        /// Gets YTD, MTD, yesterdays revenue and monthly overview
        /// </summary>
        /// <remarks>
        /// Gets Year to Date, Month to Date, yesterdays revenue and monthly overview based on when the request came
        /// 
        /// Note that the months in the montly overview (yearlyRev) are numbered 0-11. Thus month 0=January, month 1=february etc.
        /// 
        /// </remarks>     
        [HttpGet("/outlets/overview", Name = "OutletsOverview")]
        public async Task<IActionResult> GetOutletsOverview()
        {
            // Reports filed before 5am are treated as fbreport for the day before.
            // Request for toDate are reports including that date.
            DateTime now = DateTime.UtcNow;
            DateTime today = new DateTime(now.Year, now.Month, now.Day, 0, 0, 0).AddHours(5.1);
            DateTime yesterday = today.AddDays(-1).AddHours(5.1);
            DateTime startOfYear = new DateTime(now.Year, 1, 1, 0, 0, 0).AddHours(5.1);
            DateTime startOfMonth = new DateTime(now.Year, now.Month, 1, 0, 0, 0).AddHours(5.1);
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

            // Deleting older images from DB
            //DeleteImages deleteImages = new DeleteImages(_repository, _logger, env, config); TODO uncomment
            //deleteImages.DelImgs(outletIds);

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

            YTDOutletFbReports = (List<FbReport>)await _repository.FbReport.GetAllOutletFbReportsForOutlets(outletIds, startOfYear, today, trackChanges: false);

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
            YearlyRevDto yearlyRev = new YearlyRevDto()
            {
                MonthlyRevs = new List<MonthlyRevDto>() // Initialize List
            };

            // loop outlets
            for (int outletCounter = 1; outletCounter <= outletIdCounter; outletCounter++)
            {

                MonthlyRevDto monthlyRev = new MonthlyRevDto()
                {
                    Revenues = new List<int[][]>() // Initialize List
                };

                int? rev1Month;
                
                // loop months
                for (int monthCounter = 0; monthCounter < 12; monthCounter++)
                {
                    int[] outlId = new int[1];
                    outlId[0] = outletCounter;

                   // Get data from DB
                   var MonthlyRevsFromDB = await _repository.FbReport.GetAllOutletFbReportsForOutlets(
                       outlId,
                       new DateTime(now.Year, (monthCounter + 1), 1, 0, 0, 0).AddHours(5.1),
                       new DateTime(now.Year, (monthCounter + 1), DateTime.DaysInMonth(now.Year, monthCounter + 1), 23, 59, 59).AddHours(5.1),
                       trackChanges: false
                   );

                    int[][] mRevTempArray = new int[1][];
                    mRevTempArray[0] = new int[2];
                    monthlyRev.OutletId = outletCounter;

                    if (MonthlyRevsFromDB != null)
                    {
                        rev1Month = 0;

                        // Loop through reports
                        foreach (var mr in MonthlyRevsFromDB)
                        {
                            rev1Month += mr.Food;
                            rev1Month += mr.Beverage;
                            rev1Month += mr.OtherIncome;
                        }

                        mRevTempArray[0][0] = monthCounter;
                        mRevTempArray[0][1] = (int)rev1Month;
                    }

                    monthlyRev.Revenues.Add(mRevTempArray);
                }

                yearlyRev.MonthlyRevs.Add(monthlyRev);
            }

            // Adding to dto for return
            var revenueOverview = new RevenueOverviewDto
            {
                YTDs = YTDs,
                MTDs = MTDs,
                YesterdaysRevs = yesterdaysRevs,
                YearlyRev = yearlyRev
            };

            return Ok(revenueOverview);
        }
    }
}