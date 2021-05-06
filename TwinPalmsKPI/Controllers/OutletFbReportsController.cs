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

        public OutletsFbReportsController(IRepositoryManager repository, ILoggerManager logger, IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }

        // TODO delete?
        // ************************************** GetOutletFbReports ************************************************
        /// <summary>
        /// Gets a All FbReports by Outlet Id between two Dates
        /// </summary>
        /// <remarks>
        /// Gets all FbReports for one outlet between fromDate and toDate, including toDate
        /// 
        /// For example: to get the reports from only 2021-05-04
        /// 
        ///     fromDate = 2021-05-04
        ///     toDate = 2021-05-04
        ///     
        /// </remarks>     
        [HttpGet("/outlet/fbReports/{outletId}", Name = "OutletFbReportsByIdAndDate")]
        public async Task<IActionResult> GetOutletFbReports(int outletId, DateTime fromDate, DateTime toDate)
        {
            // Needed to include also reports from toDate day
            DateTime toDateCorrected = toDate.AddDays(1);

            var outlet = await _repository.Outlet.GetOutletAsync(outletId, trackChanges: false);

            if (outlet == null)
            {
                _logger.LogInfo($"Outlet with id {outletId} doesn't exist in the database.");
                return NotFound();
            }

            var outletFbReports = await _repository.FbReport.GetAllOutletFbReportsForOneOutlet(outletId, fromDate, toDateCorrected, trackChanges: false);

            if (outletFbReports == null)
            {
                _logger.LogInfo($"No reports for Outlet with id {outletId} found in the database between {fromDate} and {toDateCorrected}.");
                return NotFound();
            }

            var outletFbReportsToReturn = outletFbReports.Select(o => new
            {
                Tables = o.Tables,
                Food = o.Food,
                Beverage = o.Beverage,
                OtherIncome = o.OtherIncome,
                Date = o.Date,
                GuestsFromHotel = o.GuestsFromHotel,
                GuestsFromOutsideHotel = o.GuestsFromOutsideHotel,
                IsPublicHoliday = o.IsPublicHoliday,
                Notes = o.Notes,
                OutletId = o.OutletId,
                UserId = o.UserId,
                LocalEventId = o.LocalEventId,
                GuestSourceOfBusinesses = o.FbReportGuestSourceOfBusinesses.Select(f => f.GuestSourceOfBusiness).ToList(),
                Weathers = o.WeatherFbReports.Select(w => w.Weather).ToList()
            }).ToArray();

            return Ok(outletFbReportsToReturn);
        }

        // ****************************************** GetOutletsFbReports ****************************************************
        /// <summary>
        /// Gets a All FbReports by many Outlet Ids between two Dates
        /// </summary>
        /// <remarks>
        /// Gets all FbReports for many outlets between fromDate and toDate, including toDate
        /// 
        /// For example: to get the reports from only 2021-05-04
        /// 
        ///     fromDate = 2021-05-04
        ///     toDate = 2021-05-04
        ///     
        /// </remarks>     
        [HttpGet("/outlets/fbReports", Name = "OutletsFbReportsByIdAndDate")]
        public async Task<IActionResult> GetOutletsFbReports( [FromQuery] int[] outletIds, DateTime fromDate, DateTime toDate)
        {
            // Needed to include also reports from toDate day
            DateTime toDateCorrected = toDate.AddDays(1);

            StringBuilder sbOutletIds = new StringBuilder();

            int outletIdCounter = 0;

            foreach (var oi in outletIds)
            {
                outletIdCounter++;

                if (await _repository.FbReport.GetFbReportAsync(oi, false) == null)
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

            var outletFbReports = await _repository.FbReport.GetAllOutletFbReportsForOutlets(outletIds, fromDate, toDateCorrected, trackChanges: false);

            if (outletFbReports.Count() == 0)
            {
                _logger.LogInfo($"No reports for Outlet with ids {sbOutletIds.ToString()} found in the database between dates {fromDate} and {toDateCorrected}.");
                return NotFound();
            }

            var outletFbReportsToReturn = outletFbReports.Select(o => new
            {
                Tables = o.Tables,
                Food = o.Food,
                Beverage = o.Beverage,
                OtherIncome = o.OtherIncome,
                Date = o.Date,
                GuestsFromHotel = o.GuestsFromHotel,
                GuestsFromOutsideHotel = o.GuestsFromOutsideHotel,
                IsPublicHoliday = o.IsPublicHoliday,
                Notes = o.Notes,
                OutletId = o.OutletId,
                UserId = o.UserId,
                LocalEventId = o.LocalEventId,
                GuestSourceOfBusinesses = o.FbReportGuestSourceOfBusinesses.Select(f => f.GuestSourceOfBusiness).ToList(),
                Weathers = o.WeatherFbReports.Select(w => w.Weather).ToList()
            }).ToArray();

            return Ok(outletFbReportsToReturn);
        }
    }
}

