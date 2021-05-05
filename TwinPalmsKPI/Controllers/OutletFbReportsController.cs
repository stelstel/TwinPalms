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

        /// <summary>
        /// Gets a All FbReports by Outlet Id between two Dates
        /// </summary>
        /// <remarks>
        /// Gets all FbReports between fromDate and toDate, not including toDate
        /// 
        /// For example: to get the reports from 2021-05-04
        /// 
        ///     fromDate = 2021-05-04
        ///     toDate = 2021-05-05
        ///     
        /// </remarks>     
        [HttpGet("/outlet/{outletId}/fbReports", Name = "OutletFbReportsByIdAndDate")]
        public async Task<IActionResult> GetOutletFbReport(int outletId, DateTime fromDate, DateTime toDate)
        {
            var outlet = await _repository.Outlet.GetOutletAsync(outletId, trackChanges: false);

            if (outlet == null)
            {
                _logger.LogInfo($"Outlets with id {outletId} doesn't exist in the database.");
                return NotFound();
            }

            var outletFbReports = await _repository.FbReport.GetAllOutletFbReportsForOneOutlet(outletId, fromDate, toDate, trackChanges: false);

            if (outletFbReports == null)
            {
                _logger.LogInfo($"No reports for Outlet with id {outletId} found in the database between {fromDate} and {toDate}.");
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
                GuestSourceOfBusinesses = o.FbReportGuestSourceOfBusinesses.Select(f => f.GuestSourceOfBusinessId).ToList(),
                Weathers = o.WeatherFbReports.Select(w => w.Weather).ToList()
            }).ToArray();

            return Ok(outletFbReportsToReturn);
        }
    }
}

