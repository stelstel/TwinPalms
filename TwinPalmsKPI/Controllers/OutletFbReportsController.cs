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

            //var outletFbReportsToReturn = outletFbReports.Select(o => new
            //{
            //    Beverage = o.Beverage,
            //    Date = o.Date,
            //    FbReportGuestSourceOfBusinesses = o (o => o.)

            //    //Date = x.Date,
            //    //NewRoomNights = x.NewRoomNights,
            //    //TodaysRevenuePickup = x.TodaysRevenuePickup,
            //    //OtherRevenue = x.OtherRevenue,
            //    //IsPublicHoliday = x.IsPublicHoliday,
            //    //RoomTypeId = x.RoomTypeId,
            //    //HotelId = x.RoomType.HotelId,
            //    //LocalEvent = x.LocalEventId
            //}).ToArray();


            // return Ok(outletFbReportsToReturn);
            return Ok();
        }
    }
}
